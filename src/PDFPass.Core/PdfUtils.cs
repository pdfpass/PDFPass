using System;
using System.IO;
using System.Text;
using iText.IO.Font;
using iText.Kernel.Exceptions;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PDFPass.Pdf;

public abstract class PdfUtils
{
    // List of characters to be used in random passwords
    private const string PwChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789#@&#@&#@&#@&";

    public static bool IsPdfReaderPasswordSet(string pdfFilePath)
    {
        try
        {
            using var pdfReader = new PdfReader(pdfFilePath, new ReaderProperties().SetPassword(null));
            using var pdfDocument = new PdfDocument(pdfReader);
            return false;
        }
        catch (BadPasswordException)
        {
            return true;
        }
    }

    public static bool IsPdfFile(string pdfFilePath)
    {
        if (!File.Exists(pdfFilePath))
        {
            return false;
        }

        try
        {
            using var fileStream = File.OpenRead(pdfFilePath);
            // PDF files begin with "%PDF-"
            Span<byte> header = stackalloc byte[5];
            var read = fileStream.Read(header);
            if (read < 5)
            {
                return false;
            }

            return header[0] == (byte)'%' && header[1] == (byte)'P' && header[2] == (byte)'D' &&
                   header[3] == (byte)'F' && header[4] == (byte)'-';
        }
        catch
        {
            return false;
        }
    }


    public static bool IsPasswordCorrect(string pdfFilePath, string password)
    {
        try
        {
            var pwdBytes = Encoding.UTF8.GetBytes(password);
            using var pdfReader = new PdfReader(pdfFilePath, new ReaderProperties().SetPassword(pwdBytes));
            using var pdfDocument = new PdfDocument(pdfReader);
            return true;
        }
        catch (BadPasswordException)
        {
            return false;
        }
    }


    public static bool IsPasswordWithFullPermissions(string pdfFilePath, string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var readerProps = new ReaderProperties();
        readerProps.SetPassword(passwordBytes);

        using var pdfReader = new PdfReader(pdfFilePath, readerProps);
        using var pdfDocument = new PdfDocument(pdfReader);
        return pdfReader.IsOpenedWithFullPermission();
    }

    public static void WriteEncryptedPdf(string inputFileName, string outputFileName, WriterProperties writerProperties,
        string watermarkText)
    {
        using var reader = new PdfReader(inputFileName);
        using var writer = new PdfWriter(outputFileName, writerProperties);
        StampPdf(reader, writer, watermarkText);
    }

    public static void WriteDecryptedPdf(string inputFileName, string outputFileName, string readPassword)
    {
        var readerProps = new ReaderProperties();
        var passwordBytes = Encoding.UTF8.GetBytes(readPassword);
        readerProps.SetPassword(passwordBytes);

        using var reader = new PdfReader(inputFileName, readerProps);
        using var writer = new PdfWriter(outputFileName);
        StampPdf(reader, writer);
    }

    public static void WriteFromUnencryptedPdf(string inputFileName, Stream output, WriterProperties writerProperties,
        string? watermarkText)
    {
        using var reader = new PdfReader(inputFileName);
        using var writer = new PdfWriter(output, writerProperties);
        StampPdf(reader, writer, watermarkText, requireEncrypted: false);
    }

    public static void WriteDecryptedPdf(string inputFileName, Stream output, string password)
    {
        using var reader = new PdfReader(inputFileName, new ReaderProperties().SetPassword(Encoding.UTF8.GetBytes(password)));
        using var writer = new PdfWriter(output);
        StampPdf(reader, writer, requireEncrypted: true);
    }

    private static void StampPdf(PdfReader reader, PdfWriter writer, string? watermarkText = null,
        bool? requireEncrypted = null)
    {
        // iText's stamping constructor checks IsOpenedWithFullPermission before writing.
        using var document = new PdfDocument(reader, writer);
        if (requireEncrypted == true && !reader.IsEncrypted())
        {
            throw new PdfInputException("The input PDF is unencrypted. Provide an encrypted PDF to decrypt.");
        }

        if (requireEncrypted == false && reader.IsEncrypted())
        {
            throw new PdfInputException("The input PDF is encrypted. Decrypt it before encrypting or watermarking it.");
        }

        if (!string.IsNullOrEmpty(watermarkText))
        {
            var fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            var font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H,
                PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
            for (var i = 1; i <= document.GetNumberOfPages(); i++)
            {
                AddWatermark(document.GetPage(i), font, watermarkText);
            }
        }
    }

    public static string GenerateRandomPassword(int pwLengthMin, int pwLengthMax)
    {
        // Generate a random password
        var rnd = new Random(); // Random number generator
        var length = rnd.Next(pwLengthMin, pwLengthMax); // Choose password length.
        var result = "";

        // Pick 'length' characters from the allowed characters.
        for (var i = 0; i < length; i++)
        {
            result += PwChars[rnd.Next(0, PwChars.Length - 1)].ToString();
        }

        return result;
    }

    private static void AddWatermark(PdfPage page, PdfFont font, string watermark)
    {
        var pageSize = page.GetPageSizeWithRotation();
        var x = (pageSize.GetLeft() + pageSize.GetRight()) / 2.5f;
        var y = (pageSize.GetTop() + pageSize.GetBottom()) / 2f;
        var text = new Text(watermark);
        text.SetFont(font);
        text.SetFontSize(150);
        text.SetOpacity(0.2f);

        page.SetIgnorePageRotationForContent(true);
        var canvas = new Canvas(page, pageSize);
        canvas.ShowTextAligned(new Paragraph(text), x - 7, y, page.GetDocument().GetPageNumber(page),
            TextAlignment.CENTER, VerticalAlignment.TOP, MathF.PI / 4f);
        canvas.Close();
    }
}

public sealed class PdfInputException(string message) : Exception(message);
