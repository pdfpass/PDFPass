using System.Text;
using System.Windows.Forms;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace PDFPass;

public abstract class PdfUtils
{
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

    public static void EncryptPdf(string inputFileName, string outputFileName, WriterProperties writerProperties)
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
}