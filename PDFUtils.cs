using System;
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
            var pdfReader = new PdfReader(pdfFilePath, new ReaderProperties().SetPassword(null));
            var pdfDocument = new PdfDocument(pdfReader);
            pdfDocument.Close();
            return false;
        }
        catch (BadPasswordException)
        {
            return true;
        }
    }

    public static bool IsPdfFile(string pdfFilePath)
    {
        try
        {
            var pdfReader = new PdfReader(pdfFilePath, new ReaderProperties().SetPassword(null));
            var pdfDocument = new PdfDocument(pdfReader);
            pdfDocument.Close();
            return false;
        }
        catch (BadPasswordException)
        {
            return true;
        }
    }


    public static bool IsPasswordCorrect(string pdfFilePath, string password)
    {
        try
        {
            var pwdBytes = Encoding.ASCII.GetBytes(password);
            var pdfReader = new PdfReader(pdfFilePath, new ReaderProperties().SetPassword(pwdBytes));
            var pdfDocument = new PdfDocument(pdfReader);
            pdfDocument.Close();
            return true;
        }
        catch (BadPasswordException)
        {
            return false;
        }
    }


    public static bool IsPasswordWithFullPermissions(string pdfFilePath, string password)
    {
        var passwordBytes = Encoding.ASCII.GetBytes(password); // Convert to bytes
        var readerProps = new ReaderProperties();
        readerProps.SetPassword(passwordBytes);

        var pdfReader = new PdfReader(pdfFilePath, readerProps);
        var pdfDocument = new PdfDocument(pdfReader);
        var isOpenedWithFullPermission = pdfReader.IsOpenedWithFullPermission();
        pdfDocument.Close();
        return isOpenedWithFullPermission;
    }

    public static void WriteEncryptedPdf(string inputFileName, string outputFileName, WriterProperties writerProperties,
        string watermarkText)
    {
        var reader = new PdfReader(inputFileName); // Create a PdfReader with the input file.
        var writer = new PdfWriter(outputFileName, writerProperties); // Set up the output file
        var pdfDocument = new PdfDocument(reader, writer); // Create the new document
        var document = new Document(pdfDocument);


        if (!string.IsNullOrEmpty(watermarkText))
        {
            var paragraph = createWatermarkParagraph(watermarkText);
            var transparentGraphicState = new PdfExtGState().SetFillOpacity(0.2f);
            for (var i = 1; i <= document.GetPdfDocument().GetNumberOfPages(); i++)
            {
                addWatermarkToExistingPage(document, i, paragraph, transparentGraphicState, 0f);
            }
        }

        document.Close();
        pdfDocument.Close(); // Close the output document.
    }

    public static void WriteDecryptedPdf(string inputFileName, string outputFileName, string readPassword)
    {
        var readerProps = new ReaderProperties();
        var passwordBytes = Encoding.ASCII.GetBytes(readPassword); // Convert to bytes
        readerProps.SetPassword(passwordBytes);


        var pdfReader = new PdfReader(inputFileName, readerProps);
        var pdfDocument = new PdfDocument(pdfReader, new PdfWriter(outputFileName));
        // Close the PDF documents
        pdfDocument.Close();
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