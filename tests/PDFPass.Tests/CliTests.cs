using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iText.IO.Font.Constants;
using iText.Kernel.Exceptions;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Xunit;
using Path = System.IO.Path;

namespace PDFPass.Tests;

public sealed class CliTests : IDisposable
{
    private const string USER_PASSWORD = "test user $' 123";
    private const string OWNER_PASSWORD = "test owner $' 456";
    private const string ENV_USER_PASSWORD = "alternate user 789";
    private const string ENV_OWNER_PASSWORD = "alternate owner 321";
    private const string WRONG_PASSWORD = "definitely wrong 654";
    private const string WATERMARK = "Príliš žltý kôň";
    private const int OPTIONAL_PERMISSIONS = EncryptionConstants.ALLOW_PRINTING |
        EncryptionConstants.ALLOW_MODIFY_CONTENTS | EncryptionConstants.ALLOW_COPY |
        EncryptionConstants.ALLOW_MODIFY_ANNOTATIONS | EncryptionConstants.ALLOW_FILL_IN |
        EncryptionConstants.ALLOW_SCREENREADERS | EncryptionConstants.ALLOW_ASSEMBLY |
        EncryptionConstants.ALLOW_DEGRADED_PRINTING;

    private readonly DirectoryInfo directory = Directory.CreateTempSubdirectory("pdfpassc skúška ");

    [Fact]
    public async Task EncryptAndDecryptPreserveContentAndSetRequestedProtection()
    {
        var input = CreatePdf();
        var encrypted = FilePath("zašifrovaný dokument.pdf");
        var decrypted = FilePath("odšifrovaný dokument.pdf");

        AssertSuccess(await RunAsync("encrypt", "-i", input, "-o", encrypted,
            "--user-password", USER_PASSWORD, "--owner-password", OWNER_PASSWORD), encrypted);
        AssertProtectedPdf(encrypted, OWNER_PASSWORD, fullPermissions: true);
        AssertProtectedPdf(encrypted, USER_PASSWORD, fullPermissions: false);
        AssertBadPassword(encrypted, null);

        AssertSuccess(await RunAsync("decrypt", "--input", encrypted, "--output", decrypted,
            "--password", OWNER_PASSWORD), decrypted);
        AssertPlainPdf(decrypted);
        AssertPlainPdf(input);
    }

    [Theory]
    [InlineData("--user-password", true)]
    [InlineData("--owner-password", false)]
    public async Task EitherSinglePasswordCanDecryptAsOwner(string passwordFlag, bool requiresOpenPassword)
    {
        var input = CreatePdf();
        var encrypted = FilePath("single-password.pdf");
        var decrypted = FilePath("single-password-plain.pdf");

        AssertSuccess(await RunAsync("encrypt", "-i", input, "-o", encrypted, passwordFlag, OWNER_PASSWORD), encrypted);
        AssertProtectedPdf(encrypted, OWNER_PASSWORD, fullPermissions: true);
        if (requiresOpenPassword)
        {
            AssertBadPassword(encrypted, null);
        }
        else
        {
            AssertProtectedPdf(encrypted, null, fullPermissions: false);
        }

        AssertSuccess(await RunAsync("decrypt", "-i", encrypted, "-o", decrypted, "--password", OWNER_PASSWORD),
            decrypted);
        AssertPlainPdf(decrypted);
    }

    [Fact]
    public async Task WrongPasswordLimitedPasswordAndPlainInputFailDecryption()
    {
        var plain = CreatePdf();
        var protectedPdf = CreateProtectedPdf(USER_PASSWORD);
        var output = FilePath("failed-decryption.pdf");

        AssertError(await RunAsync("decrypt", "-i", protectedPdf, "-o", output, "--password", WRONG_PASSWORD), 1,
            "password");
        AssertError(await RunAsync("decrypt", "-i", protectedPdf, "-o", output, "--password", USER_PASSWORD), 1,
            "owner");
        AssertError(await RunAsync("decrypt", "-i", plain, "-o", output, "--password", OWNER_PASSWORD), 1,
            "encrypt");
        Assert.False(File.Exists(output));
    }

    [Fact]
    public async Task PasswordFlagsOverrideTheCorrespondingEnvironmentVariables()
    {
        var input = CreatePdf();
        var envOutput = FilePath("encrypted-with-env.pdf");
        var flagOutput = FilePath("encrypted-with-flags.pdf");
        var envDecrypted = FilePath("decrypted-with-env.pdf");
        var flagDecrypted = FilePath("decrypted-with-flag.pdf");
        var encryptionEnvironment = new Dictionary<string, string>
        {
            ["PDFPASSC_USER_PASSWORD"] = ENV_USER_PASSWORD,
            ["PDFPASSC_OWNER_PASSWORD"] = ENV_OWNER_PASSWORD
        };

        AssertSuccess(await RunAsync(encryptionEnvironment, "encrypt", "-i", input, "-o", envOutput), envOutput);
        AssertProtectedPdf(envOutput, ENV_USER_PASSWORD, fullPermissions: false);
        AssertProtectedPdf(envOutput, ENV_OWNER_PASSWORD, fullPermissions: true);

        AssertSuccess(await RunAsync(encryptionEnvironment, "encrypt", "-i", input, "-o", flagOutput,
            "--user-password", USER_PASSWORD, "--owner-password", OWNER_PASSWORD), flagOutput);
        AssertProtectedPdf(flagOutput, USER_PASSWORD, fullPermissions: false);
        AssertProtectedPdf(flagOutput, OWNER_PASSWORD, fullPermissions: true);
        AssertBadPassword(flagOutput, ENV_USER_PASSWORD);
        AssertBadPassword(flagOutput, ENV_OWNER_PASSWORD);

        var decryptionEnvironment = new Dictionary<string, string> { ["PDFPASSC_PASSWORD"] = ENV_OWNER_PASSWORD };
        AssertSuccess(await RunAsync(decryptionEnvironment, "decrypt", "-i", envOutput, "-o", envDecrypted),
            envDecrypted);
        AssertSuccess(await RunAsync(decryptionEnvironment, "decrypt", "-i", flagOutput, "-o", flagDecrypted,
            "--password", OWNER_PASSWORD), flagDecrypted);
        AssertPlainPdf(envDecrypted);
        AssertPlainPdf(flagDecrypted);
    }

    [Theory]
    [InlineData("encrypt", "--user-password", "PDFPASSC_USER_PASSWORD", "")]
    [InlineData("encrypt", "--owner-password", "PDFPASSC_OWNER_PASSWORD", "  ")]
    [InlineData("decrypt", "--password", "PDFPASSC_PASSWORD", "  ")]
    public async Task BlankPasswordFlagsAndEnvironmentValuesAreInvalid(string command, string option,
        string variable, string blank)
    {
        var input = CreatePdf();
        var output = FilePath("blank-password.pdf");
        var fallback = new Dictionary<string, string> { [variable] = ENV_OWNER_PASSWORD };
        var blankEnvironment = new Dictionary<string, string> { [variable] = blank };

        AssertError(await RunAsync(fallback, command, "-i", input, "-o", output, option, blank), 2, "password");
        AssertError(await RunAsync(blankEnvironment, command, "-i", input, "-o", output), 2, "password");
        Assert.False(File.Exists(output));
    }

    [Fact]
    public async Task HelpVersionAndParsingErrorsUseExpectedStreamsWithoutExposingPasswords()
    {
        var help = await RunAsync("--help");
        Assert.Equal(0, help.ExitCode);
        Assert.Contains("encrypt", help.Stdout, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("decrypt", help.Stdout, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("watermark", help.Stdout, StringComparison.OrdinalIgnoreCase);
        Assert.Empty(help.Stderr);

        var commandHelp = await RunAsync("encrypt", "--owner-password", OWNER_PASSWORD, "--help");
        Assert.Equal(0, commandHelp.ExitCode);
        Assert.Contains("--owner-password", commandHelp.Stdout);
        Assert.Empty(commandHelp.Stderr);

        var version = await RunAsync("--version");
        var versionInfo = FileVersionInfo.GetVersionInfo(GetExecutablePath());
        var expected = new Version(versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart,
            versionInfo.FilePrivatePart);
        Assert.Equal(0, version.ExitCode);
        Assert.Equal(expected, Version.Parse(version.Stdout.Trim()));
        Assert.Empty(version.Stderr);

        AssertError(await RunAsync("encrypt", "--owner-password", OWNER_PASSWORD, "--user-password", USER_PASSWORD), 2,
            "--help");
        AssertError(await RunAsync("encrypt", "-i", "input.pdf", "-o", "output.pdf"), 2, "password");
        AssertError(await RunAsync("decrypt", "-i", "input.pdf", "-o", "output.pdf"), 2, "password");
        AssertError(await RunAsync("encrypt", "--unknown", WRONG_PASSWORD), 2, "--help");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task WatermarkIsDiagonalOnAllPagesAndPreservesOriginalContent(bool encrypt)
    {
        var input = CreatePdf();
        var output = FilePath("dôverný dokument.pdf");

        var result = encrypt
            ? await RunAsync("encrypt", "-i", input, "-o", output, "--owner-password", OWNER_PASSWORD,
                "--watermark", WATERMARK)
            : await RunAsync("watermark", "-i", input, "-o", output, "--text", WATERMARK);
        AssertSuccess(result, output);

        using var reader = OpenReader(output, encrypt ? OWNER_PASSWORD : null);
        using var pdf = new PdfDocument(reader);
        Assert.Equal(encrypt, reader.IsEncrypted());
        AssertPdfContent(pdf, WATERMARK);
        for (var pageNumber = 1; pageNumber <= pdf.GetNumberOfPages(); pageNumber++)
        {
            var page = pdf.GetPage(pageNumber);
            var watermark = new WatermarkListener();
            new PdfCanvasProcessor(watermark).ProcessPageContent(page);
            var baseline = Assert.Single(watermark.Baselines);
            var start = baseline.GetStartPoint();
            var end = baseline.GetEndPoint();
            var dx = Math.Abs(end.Get(Vector.I1) - start.Get(Vector.I1));
            var dy = Math.Abs(end.Get(Vector.I2) - start.Get(Vector.I2));
            Assert.True(dx > 0 && Math.Abs(dx - dy) < 1, $"Unexpected baseline on page {pageNumber}: {dx}, {dy}.");
            var size = page.GetPageSize();
            Assert.InRange((start.Get(Vector.I1) + end.Get(Vector.I1)) / 2, size.GetLeft(), size.GetRight());
            Assert.InRange((start.Get(Vector.I2) + end.Get(Vector.I2)) / 2, size.GetBottom(), size.GetTop());
        }

        AssertPlainPdf(input);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task WatermarkAndEncryptRejectEncryptedInputEvenWhenItOpensWithoutPassword(bool hasUserPassword)
    {
        var input = CreateProtectedPdf(hasUserPassword ? USER_PASSWORD : null);
        var output = FilePath("unexpected-output.pdf");

        AssertError(await RunAsync("watermark", "-i", input, "-o", output, "--text", WATERMARK), 1, "encrypt");
        AssertError(await RunAsync("encrypt", "-i", input, "-o", output, "--owner-password", OWNER_PASSWORD), 1,
            "encrypt");
        Assert.False(File.Exists(output));
    }

    [Fact]
    public async Task EmptyOrWhitespaceWatermarkTextIsInvalid()
    {
        var input = CreatePdf();
        var output = FilePath("blank-watermark.pdf");

        AssertError(await RunAsync("watermark", "-i", input, "-o", output, "--text", ""), 2, "text");
        AssertError(await RunAsync("encrypt", "-i", input, "-o", output, "--owner-password", OWNER_PASSWORD,
            "--watermark", "  "), 2, "watermark");
        Assert.False(File.Exists(output));
    }

    [Fact]
    public async Task MissingInputAndOutputDirectoryAreRejected()
    {
        var input = CreatePdf();
        var output = FilePath("output.pdf");
        var missingDirectory = FilePath("missing-directory");

        AssertError(await RunAsync("watermark", "-i", "missing.pdf", "-o", output, "--text", WATERMARK), 1, "input");
        AssertError(await RunAsync("watermark", "-i", input, "-o", Path.Combine(missingDirectory, "output.pdf"),
            "--text", WATERMARK), 1, "directory");
        Assert.False(File.Exists(output));
        Assert.False(Directory.Exists(missingDirectory));
    }

    [Fact]
    public async Task EquivalentWindowsPathsAreRejectedEvenWithForce()
    {
        var input = CreatePdf();
        var original = File.ReadAllBytes(input);
        var extendedPath = input.StartsWith(@"\\", StringComparison.Ordinal)
            ? @"\\?\UNC\" + input[2..]
            : @"\\?\" + input;

        foreach (var alias in new[] { @".\SOURCE DOCUMENT.PDF", input + ".", extendedPath })
        {
            AssertError(await RunAsync("watermark", "-i", input, "-o", alias, "--text", WATERMARK, "--force"), 2,
                "different");
            Assert.Equal(original, File.ReadAllBytes(input));
        }
    }

    [Theory]
    [InlineData("encrypt", "--owner-password", OWNER_PASSWORD)]
    [InlineData("decrypt", "--password", OWNER_PASSWORD)]
    [InlineData("watermark", "--text", WATERMARK)]
    public async Task ExistingOutputRequiresForce(string command, string option, string value)
    {
        var input = command == "decrypt" ? CreateProtectedPdf(USER_PASSWORD) : CreatePdf();
        var output = FilePath("existing.pdf");
        byte[] previous = [1, 2, 3];
        File.WriteAllBytes(output, previous);

        AssertError(await RunAsync(command, "-i", input, "-o", output, option, value), 1, "--force");
        Assert.Equal(previous, File.ReadAllBytes(output));
        AssertSuccess(await RunAsync(command, "-i", input, "-o", output, option, value, "--force"), output);

        if (command == "encrypt")
        {
            AssertProtectedPdf(output, OWNER_PASSWORD, fullPermissions: true);
        }
        else
        {
            AssertPlainPdf(output, command == "watermark" ? WATERMARK : null);
        }
    }

    [Fact]
    public async Task MalformedPdfLeavesExistingDestinationAndNoTemporaryOutput()
    {
        var malformed = FilePath("malformed.pdf");
        var output = FilePath("existing.pdf");
        File.WriteAllText(malformed, "%PDF-1.7\n1 0 obj\n<< /Type /Catalog >>\nendobj\n%%EOF\n");
        byte[] previous = [4, 5, 6];
        File.WriteAllBytes(output, previous);
        var filesBefore = Directory.GetFileSystemEntries(directory.FullName).Order().ToArray();

        AssertError(await RunAsync("encrypt", "-i", malformed, "-o", output, "--owner-password", OWNER_PASSWORD,
            "--force"), 1);
        Assert.Equal(previous, File.ReadAllBytes(output));
        Assert.Equal(filesBefore, Directory.GetFileSystemEntries(directory.FullName).Order().ToArray());
    }

    public void Dispose() => directory.Delete(recursive: true);

    private string FilePath(string name) => Path.Combine(directory.FullName, name);

    private string CreatePdf(string name = "source document.pdf", WriterProperties? writerProperties = null)
    {
        var path = FilePath(name);
        using var writer = new PdfWriter(path, writerProperties ?? new WriterProperties());
        using var pdf = new PdfDocument(writer);
        var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        for (var pageNumber = 1; pageNumber <= 2; pageNumber++)
        {
            var page = pdf.AddNewPage(PageSize.A4);
            if (pageNumber == 2)
            {
                page.SetRotation(90);
            }

            var canvas = new PdfCanvas(page);
            canvas.BeginText().SetFontAndSize(font, 12).MoveText(50, 100)
                .ShowText($"Original content on page {pageNumber}").EndText();
            canvas.Release();
        }

        return path;
    }

    private string CreateProtectedPdf(string? userPassword)
    {
        var writerProperties = new WriterProperties().SetStandardEncryption(
            userPassword is null ? null : Encoding.UTF8.GetBytes(userPassword), Encoding.UTF8.GetBytes(OWNER_PASSWORD),
            0, EncryptionConstants.ENCRYPTION_AES_256);
        return CreatePdf("protected.pdf", writerProperties);
    }

    private static PdfReader OpenReader(string path, string? password) => new(path,
        new ReaderProperties().SetPassword(password is null ? null : Encoding.UTF8.GetBytes(password)));

    private static void AssertPlainPdf(string path, string? watermark = null)
    {
        using var reader = OpenReader(path, null);
        using var pdf = new PdfDocument(reader);
        Assert.False(reader.IsEncrypted());
        AssertPdfContent(pdf, watermark);
    }

    private static void AssertProtectedPdf(string path, string? password, bool fullPermissions)
    {
        using var reader = OpenReader(path, password);
        using var pdf = new PdfDocument(reader);
        Assert.True(reader.IsEncrypted());
        Assert.Equal(fullPermissions, reader.IsOpenedWithFullPermission());
        Assert.Equal(EncryptionConstants.ENCRYPTION_AES_256, reader.GetCryptoMode());
        Assert.Equal(0, reader.GetPermissions() & OPTIONAL_PERMISSIONS);
        AssertPdfContent(pdf);
    }

    private static void AssertBadPassword(string path, string? password)
    {
        Assert.Throws<BadPasswordException>(() =>
        {
            using var reader = OpenReader(path, password);
            using var pdf = new PdfDocument(reader);
        });
    }

    private static void AssertPdfContent(PdfDocument pdf, string? watermark = null)
    {
        Assert.Equal(2, pdf.GetNumberOfPages());
        for (var pageNumber = 1; pageNumber <= 2; pageNumber++)
        {
            var page = pdf.GetPage(pageNumber);
            Assert.Equal(pageNumber == 2 ? 90 : 0, page.GetRotation());
            var text = PdfTextExtractor.GetTextFromPage(page);
            Assert.Contains($"Original content on page {pageNumber}", text);
            if (watermark is not null)
            {
                Assert.Contains(watermark, text);
            }
        }
    }

    private static void AssertSuccess(CliResult result, string expectedPath)
    {
        Assert.True(result.ExitCode == 0, $"Exit code {result.ExitCode}: {result.Stderr}");
        Assert.Empty(result.Stderr);
        Assert.Equal(expectedPath, result.Stdout.TrimEnd('\r', '\n'));
    }

    private static void AssertError(CliResult result, int expectedExitCode, string? expectedMessage = null)
    {
        Assert.Equal(expectedExitCode, result.ExitCode);
        Assert.Empty(result.Stdout);
        Assert.False(string.IsNullOrWhiteSpace(result.Stderr));
        if (expectedMessage is not null)
        {
            Assert.Contains(expectedMessage, result.Stderr, StringComparison.OrdinalIgnoreCase);
        }
    }

    private Task<CliResult> RunAsync(params string[] arguments) => RunAsync(null, arguments);

    private async Task<CliResult> RunAsync(IReadOnlyDictionary<string, string>? environment, params string[] arguments)
    {
        var binary = GetExecutablePath();
        Assert.True(File.Exists(binary), $"CLI executable missing at {binary}. Build the solution or set PDFPASSC_EXE.");
        var startInfo = new ProcessStartInfo(binary)
        {
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8,
            WorkingDirectory = directory.FullName
        };
        startInfo.Environment.Remove("PDFPASSC_USER_PASSWORD");
        startInfo.Environment.Remove("PDFPASSC_OWNER_PASSWORD");
        startInfo.Environment.Remove("PDFPASSC_PASSWORD");
        if (environment is not null)
        {
            foreach (var (key, value) in environment)
            {
                startInfo.Environment[key] = value;
            }
        }

        foreach (var argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        using var process = Process.Start(startInfo) ?? throw new InvalidOperationException("Could not start pdfpassc.");
        process.StandardInput.Close();
        var stdout = process.StandardOutput.ReadToEndAsync();
        var stderr = process.StandardError.ReadToEndAsync();
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        try
        {
            await process.WaitForExitAsync(timeout.Token);
        }
        catch (OperationCanceledException)
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);
            }

            await process.WaitForExitAsync();
            throw new TimeoutException("pdfpassc did not exit within 30 seconds.");
        }

        var result = new CliResult(process.ExitCode, await stdout, await stderr);
        foreach (var password in new[] { USER_PASSWORD, OWNER_PASSWORD, ENV_USER_PASSWORD, ENV_OWNER_PASSWORD,
                     WRONG_PASSWORD })
        {
            Assert.DoesNotContain(password, result.Stdout);
            Assert.DoesNotContain(password, result.Stderr);
        }

        return result;
    }

    private static string GetExecutablePath()
    {
        var publishedPath = Environment.GetEnvironmentVariable("PDFPASSC_EXE");
        if (!string.IsNullOrEmpty(publishedPath))
        {
            return Path.GetFullPath(publishedPath);
        }

        for (var current = new DirectoryInfo(AppContext.BaseDirectory); current is not null; current = current.Parent)
        {
            if (File.Exists(Path.Combine(current.FullName, "PDFPass.sln")))
            {
                var configuration = typeof(CliTests).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>();
                return Path.Combine(current.FullName, "src", "PDFPass.Cli", "bin", configuration!.Configuration,
                    "net10.0-windows", "pdfpassc.exe");
            }
        }

        throw new InvalidOperationException("Could not find PDFPass.sln. Set PDFPASSC_EXE to the CLI executable.");
    }

    private sealed record CliResult(int ExitCode, string Stdout, string Stderr);

    private sealed class WatermarkListener : IEventListener
    {
        public List<LineSegment> Baselines { get; } = [];

        public void EventOccurred(IEventData data, EventType type)
        {
            if (data is TextRenderInfo text && text.GetText() == WATERMARK)
            {
                Baselines.Add(text.GetBaseline());
            }
        }

        public ICollection<EventType> GetSupportedEvents() => [EventType.RENDER_TEXT];
    }
}
