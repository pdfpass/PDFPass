using System;
using System.Text;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;

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

    public static void WriteEncryptedPdf(string inputFileName, string outputFileName, WriterProperties writerProperties)
    {
        var reader = new PdfReader(inputFileName); // Create a PdfReader with the input file.

        // Setting Owner Password to null generates random password.
        var writer = new PdfWriter(outputFileName, writerProperties); // Set up the output file
        var pdfDocument = new PdfDocument(reader, writer); // Create the new document

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
}