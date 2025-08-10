using System;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using PDFPass.Resources;

namespace PDFPass
{
    public class PdfFileProcessor
    {
        public ProcessResult Encrypt(string inputFile, string outputFile, string userPassword, string ownerPassword, bool applyWatermark, string watermarkText)
        {
            // Clean up text
            inputFile = inputFile.Trim();
            outputFile = outputFile.Trim();

            // Ensure input and output are not the same.
            if (string.Equals(inputFile, outputFile, StringComparison.CurrentCultureIgnoreCase))
            {
                return new ProcessResult(false, Strings.SourceAndDestinationSame);
            }

            // Ensure input file exists.
            if (!File.Exists(inputFile))
            {
                return new ProcessResult(false, Strings.SourceFileDoesNotExist);
            }

            // Verify password if at least 1 pwd
            if (string.IsNullOrWhiteSpace(userPassword) && string.IsNullOrWhiteSpace(ownerPassword))
            {
                return new ProcessResult(false, Strings.NoPasswordEntered);
            }

            try
            {
                // Encryption properties:
                var encryptionProperties = (int)Settings.encryption_type;

                // If specified, disable encrypting metadata.
                if (!Settings.encrypt_metadata)
                {
                    encryptionProperties |= EncryptionConstants.DO_NOT_ENCRYPT_METADATA;
                }

                // Set document options
                var documentOptions = 0;
                if (Settings.allow_printing) documentOptions |= EncryptionConstants.ALLOW_PRINTING;
                if (Settings.allow_degraded_printing) documentOptions |= EncryptionConstants.ALLOW_DEGRADED_PRINTING;
                if (Settings.allow_modifying) documentOptions |= EncryptionConstants.ALLOW_MODIFY_CONTENTS;
                if (Settings.allow_modifying_annotations) documentOptions |= EncryptionConstants.ALLOW_MODIFY_ANNOTATIONS;
                if (Settings.allow_copying) documentOptions |= EncryptionConstants.ALLOW_COPY;
                if (Settings.allow_form_fill) documentOptions |= EncryptionConstants.ALLOW_FILL_IN;
                if (Settings.allow_assembly) documentOptions |= EncryptionConstants.ALLOW_ASSEMBLY;
                if (Settings.allow_screenreaders) documentOptions |= EncryptionConstants.ALLOW_SCREENREADERS;

                var writerProperties = new WriterProperties(); // Set properties of output
                var userPasswordBytes = Encoding.UTF8.GetBytes(userPassword);
                var ownerPasswordBytes = Encoding.UTF8.GetBytes(ownerPassword);

                writerProperties.SetStandardEncryption(userPasswordBytes,
                    string.IsNullOrEmpty(ownerPassword) ? userPasswordBytes : ownerPasswordBytes,
                    documentOptions,
                    encryptionProperties); // Enable encryption

                var waterMark = applyWatermark ? watermarkText : string.Empty;

                PdfUtils.WriteEncryptedPdf(inputFile, outputFile, writerProperties, waterMark);
            }
            catch (Exception ex)
            {
                return new ProcessResult(false, $@"{Strings.UnknownError}{ex.Message}");
            }

            return new ProcessResult(true);
        }

        public ProcessResult Decrypt(string inputFile, string outputFile, string password)
        {
            // Clean up text
            inputFile = inputFile.Trim();
            outputFile = outputFile.Trim();

            // Ensure input and output are not the same.
            if (string.Equals(inputFile, outputFile, StringComparison.CurrentCultureIgnoreCase))
            {
                return new ProcessResult(false, Strings.SourceAndDestinationSame);
            }

            // Ensure input file exists.
            if (!File.Exists(inputFile))
            {
                return new ProcessResult(false, Strings.SourceFileDoesNotExist);
            }

            // Verify password:
            if (password == string.Empty)
            {
                return new ProcessResult(false, Strings.NoPasswordEntered);
            }

            if (!PdfUtils.IsPasswordCorrect(inputFile, password))
            {
                return new ProcessResult(false, Strings.IncorrectPassword);
            }

            if (PdfUtils.IsPasswordWithFullPermissions(inputFile, password))
            {
                // Write the un-protected PDF
                try
                {
                    PdfUtils.WriteDecryptedPdf(inputFile, outputFile, password);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    return new ProcessResult(false, Strings.UnknownErrorShort);
                }
            }
            else
            {
                return new ProcessResult(false, Strings.OwnerPasswordRequired);
            }

            return new ProcessResult(true);
        }
    }
}
