using System;
using System.IO;
using System.Text;
using iText.IO.Font.Constants;
using iText.Kernel.Exceptions;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PDFPass;

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
        using var pdfDocument = new PdfDocument(reader, writer);
        using var document = new Document(pdfDocument);


        if (!string.IsNullOrEmpty(watermarkText))
        {
            var paragraph = createWatermarkParagraph(watermarkText);
            var transparentGraphicState = new PdfExtGState().SetFillOpacity(0.2f);
            for (var i = 1; i <= document.GetPdfDocument().GetNumberOfPages(); i++)
            {
                addWatermarkToExistingPage(document, i, paragraph, transparentGraphicState, 0f);
            }
        }

        // using statements ensure proper disposal/close
    }

    public static void WriteDecryptedPdf(string inputFileName, string outputFileName, string readPassword)
    {
        var readerProps = new ReaderProperties();
        var passwordBytes = Encoding.UTF8.GetBytes(readPassword);
        readerProps.SetPassword(passwordBytes);

        using var pdfReader = new PdfReader(inputFileName, readerProps);
        using var pdfDocument = new PdfDocument(pdfReader, new PdfWriter(outputFileName));
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

    private static void addWatermarkToExistingPage(Document document, int pageIndex,
        Paragraph paragraph, PdfExtGState graphicState, float verticalOffset)
    {
        var pdfDocument = document.GetPdfDocument();
        var pdfPage = pdfDocument.GetPage(pageIndex);
        var pageSize = (PageSize)pdfPage.GetPageSizeWithRotation();
        var x = (pageSize.GetLeft() + pageSize.GetRight()) / 2.5f;
        var y = (pageSize.GetTop() + pageSize.GetBottom()) / 2f;

        var over = new PdfCanvas(pdfDocument.GetPage(pageIndex));
        over.SaveState();
        over.SetExtGState(graphicState);
        var xOffset = 14 / 2;
        var rotationInRadians = (float)(Double.Pi / 180 * 45f);

        document.ShowTextAligned(paragraph, x - xOffset, y + verticalOffset,
            pageIndex, TextAlignment.CENTER, VerticalAlignment.TOP, rotationInRadians);
        document.Flush();
        over.RestoreState();
        over.Release();
    }

    private static Paragraph createWatermarkParagraph(String watermark)
    {
        var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        var text = new Text(watermark);
        text.SetFont(font);
        text.SetFontSize(150);
        text.SetOpacity(0.2f);

        return new Paragraph(text);
    }
}