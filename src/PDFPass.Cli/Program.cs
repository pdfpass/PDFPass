using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommandLine;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using PDFPass.Pdf;

namespace PDFPass.Cli;

internal static class Program
{
    private const int OPERATION_FAILED = 1;
    private const int INVALID_ARGUMENTS = 2;

    private const string HELP = """
        Usage:
          pdfpassc encrypt -i <input> -o <output> [--user-password <password>] [--owner-password <password>] [--watermark <text>] [--force]
          pdfpassc decrypt -i <input> -o <output> [--password <owner-password>] [--force]
          pdfpassc watermark -i <input> -o <output> --text <text> [--force]
          pdfpassc --help | --version

        -i and -o can also be written as --input and --output.
        Password fallbacks: PDFPASSC_USER_PASSWORD, PDFPASSC_OWNER_PASSWORD, PDFPASSC_PASSWORD.
        Encrypt requires at least one password. Decrypt requires the owner password.
        Password flags may expose values in shell history and process listings.
        Use --force to replace an existing destination.
        """;

    private static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        if (args.Length != 0 && (args[0] == "--help" ||
            (args[0] is "encrypt" or "decrypt" or "watermark" &&
             args.Skip(1).TakeWhile(argument => argument != "--").Contains("--help"))))
        {
            return ShowHelp();
        }

        // The parser's default diagnostics can include unrecognized arguments verbatim.
        using var parser = new Parser(settings => settings.HelpWriter = null);
        return parser.ParseArguments<EncryptOptions, DecryptOptions, WatermarkOptions>(args)
            .MapResult<EncryptOptions, DecryptOptions, WatermarkOptions, int>(Encrypt, Decrypt, Watermark, HandleParseErrors);
    }

    private static int HandleParseErrors(IEnumerable<Error> errors)
    {
        if (errors.Any(error => error.Tag is ErrorType.HelpRequestedError or ErrorType.HelpVerbRequestedError))
        {
            return ShowHelp();
        }

        if (errors.Any(error => error.Tag == ErrorType.VersionRequestedError))
        {
            Console.WriteLine(typeof(Program).Assembly.GetName().Version);
            return 0;
        }

        return Error("Invalid command or options. Run pdfpassc --help for usage.", INVALID_ARGUMENTS);
    }

    private static int ShowHelp()
    {
        Console.WriteLine(HELP);
        return 0;
    }

    private static int Encrypt(EncryptOptions options)
    {
        var user = options.UserPassword ?? Environment.GetEnvironmentVariable("PDFPASSC_USER_PASSWORD");
        var owner = options.OwnerPassword ?? Environment.GetEnvironmentVariable("PDFPASSC_OWNER_PASSWORD");
        if ((user is null && owner is null) || (user is not null && string.IsNullOrWhiteSpace(user)) ||
            (owner is not null && string.IsNullOrWhiteSpace(owner)))
        {
            return Error("Supply at least one password and ensure every supplied password contains a non-whitespace character.",
                INVALID_ARGUMENTS);
        }

        if (options.Watermark is not null && string.IsNullOrWhiteSpace(options.Watermark))
        {
            return Error("--watermark requires non-blank text.", INVALID_ARGUMENTS);
        }

        var writer = new WriterProperties().SetStandardEncryption(
            user is null ? null : Encoding.UTF8.GetBytes(user), Encoding.UTF8.GetBytes((owner ?? user)!),
            0, EncryptionConstants.ENCRYPTION_AES_256);
        return WriteOutput(options, (input, output) => PdfUtils.WriteFromUnencryptedPdf(input, output, writer, options.Watermark));
    }

    private static int Decrypt(DecryptOptions options)
    {
        var password = options.Password ?? Environment.GetEnvironmentVariable("PDFPASSC_PASSWORD");
        if (string.IsNullOrWhiteSpace(password))
        {
            return Error("Supply a non-blank owner password via --password or PDFPASSC_PASSWORD.", INVALID_ARGUMENTS);
        }

        return WriteOutput(options, (input, output) => PdfUtils.WriteDecryptedPdf(input, output, password));
    }

    private static int Watermark(WatermarkOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Text))
        {
            return Error("--text requires non-blank watermark text.", INVALID_ARGUMENTS);
        }

        return WriteOutput(options, (input, output) =>
            PdfUtils.WriteFromUnencryptedPdf(input, output, new WriterProperties(), options.Text));
    }

    private static int WriteOutput(FileOptions options, Action<string, Stream> writePdf)
    {
        string input;
        string output;
        try
        {
            input = Path.GetFullPath(options.Input);
            output = Path.GetFullPath(options.Output);
        }
        catch (Exception ex) when (ex is ArgumentException or NotSupportedException)
        {
            return Error("Provide valid Windows paths for --input and --output.", INVALID_ARGUMENTS);
        }
        catch (PathTooLongException)
        {
            return Error("The input or output path is too long.");
        }

        if (string.Equals(ComparablePath(input), ComparablePath(output), StringComparison.OrdinalIgnoreCase))
        {
            return Error("Input and output must be different paths.", INVALID_ARGUMENTS);
        }

        if (!File.Exists(input))
        {
            return Error("The input file does not exist or cannot be accessed. Check --input.");
        }

        var directory = Path.GetDirectoryName(output);
        if (!Directory.Exists(directory))
        {
            return Error("The output directory does not exist or cannot be accessed. Check --output.");
        }

        if (!options.Force && File.Exists(output))
        {
            return Error("The output file already exists. Choose another path or use --force.");
        }

        try
        {
            var tempPath = Path.Combine(directory, $".pdfpassc-{Guid.NewGuid():N}.tmp");
            var temp = new FileStream(tempPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            try
            {
                using (temp)
                {
                    writePdf(input, temp);
                }

                File.Move(tempPath, output, options.Force);
                tempPath = null;
            }
            finally
            {
                if (tempPath is not null)
                {
                    File.Delete(tempPath);
                }
            }
        }
        catch (BadPasswordException)
        {
            return Error(options is DecryptOptions
                ? "The PDF requires its correct owner password. Check --password or PDFPASSC_PASSWORD."
                : "The input PDF is encrypted. Decrypt it before encrypting or watermarking it.");
        }
        catch (PdfInputException ex)
        {
            return Error(ex.Message);
        }
        catch (IOException) when (!options.Force && File.Exists(output))
        {
            return Error("The output file already exists. Choose another path or use --force.");
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return Error("Could not read the input or write the output. Check file access, locks, and free space.");
        }
        catch (Exception)
        {
            return Error("Could not process the PDF. Check that the input is a valid PDF and can be opened.");
        }

        Console.WriteLine(output);
        return 0;
    }

    private static string ComparablePath(string path)
    {
        if (path.StartsWith(@"\\?\UNC\", StringComparison.OrdinalIgnoreCase))
        {
            return @"\\" + path[8..];
        }

        if (path.Length >= 7 && path[5] == ':' && path[6] == '\\' &&
            (path.StartsWith(@"\\?\", StringComparison.Ordinal) || path.StartsWith(@"\\.\", StringComparison.Ordinal)))
        {
            return path[4..];
        }

        return path;
    }

    private static int Error(string message, int exitCode = OPERATION_FAILED)
    {
        Console.Error.WriteLine(message);
        return exitCode;
    }
}
