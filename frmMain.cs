using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Navigation;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;

// LOL

namespace PDFPass
{
    public partial class FrmMain : Form
    {
        const int PwLengthMin = 12; // Minimum generated password length
        const int PwLengthMax = 24; // Maximum generated password length

        // List of characters to be used in random passwords
        const string PwChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        public string OwnerPassword = ""; // The owner password, if any.
        public bool EncryptOnStart = false; // Allows encryption via command line without user interaction


        public FrmMain()
        {
            InitializeComponent();
        }

        private static string GetFilenameWithSuffix(string fileName, bool isInputEncrypted)
        {
            var newFileName = isInputEncrypted
                ? $"{Path.GetFileNameWithoutExtension(fileName)}-dešifrovaný.pdf"
                : $"{Path.GetFileNameWithoutExtension(fileName)}-zašifrovaný.pdf";
            return Path.Combine(Path.GetDirectoryName(fileName)!, newFileName);
        }

        private void btnInputBrowse_Click(object sender, EventArgs e)
        {
            var result = dlgOpen.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return;
            }

            txtInputFile.Text = dlgOpen.FileName;

            InitFormControls();
        }


        public void InitFormControls()
        {
            if (!File.Exists(txtInputFile.Text))
            {
                return;
            }

            var isInputEncrypted = PdfUtils.IsPdfReaderPasswordSet(txtInputFile.Text);
            txtOutputFile.Text = GetFilenameWithSuffix(txtInputFile.Text, isInputEncrypted);

            if (isInputEncrypted)
            {
                btnEncrypt.Visible = false;
                btnDecrypt.Visible = !btnEncrypt.Visible;
                btnSettings.Visible = false;
                btnPasswordGenerate.Enabled = false;
                lnkPasswordOwner.Visible = false;
                labelPassword.Text = "Zadať heslo pre odomknutie PDF:";
            }
            else
            {
                btnEncrypt.Visible = true;
                btnDecrypt.Visible = !btnEncrypt.Visible;
                btnSettings.Visible = true;
                btnPasswordGenerate.Enabled = true;
                lnkPasswordOwner.Visible = true;
                labelPassword.Text = "Zadať heslo pre uzamknutie čítania:";
            }
        }

        private void btnOutputBrowse_Click(object sender, EventArgs e)
        {
            var result = dlgSave.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return;
            }

            txtOutputFile.Text = dlgSave.FileName;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Add listener for updated settings
            Settings.notify.Add(SettingsChanged);

            // Load settings from registry
            Settings.load();

            // If immediate run is enabled, click Run button (see command line options)
            if (EncryptOnStart)
            {
                // Click the Encrypt button immediately.
                BtnEncryptClick(sender, e);
            }
        }

        private static void SettingsChanged()
        {
            // This function is executed when settings change.
            Console.WriteLine("Notifikácia zmeny nastavenia.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close the app
            Close();
        }

        private void btnPasswordGenerate_Click(object sender, EventArgs e)
        {
            // Generate a random password
            var rnd = new Random(); // Random number generator
            var length = rnd.Next(PwLengthMin, PwLengthMax); // Choose password length.
            var result = "";

            // Pick 'length' characters from the allowed characters.
            for (var i = 0; i < length; i++)
            {
                result += PwChars[rnd.Next(0, PwChars.Length - 1)].ToString();
            }

            // Set password
            txtPassword.Text = result;

            // Copy to clipboard
            btnCopy_Click(sender, e);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            // Copy password to clipboard.
            txtPassword.Focus();
            txtPassword.SelectAll();

            if (string.IsNullOrEmpty(txtPassword.Text)) return;

            Clipboard.SetText(txtPassword.Text);
            // Show Copied label
            lblCopied.Visible = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Hide Copied label
            lblCopied.Visible = false;

            // Show Password Length warning if exceeding 32 chars.
            lblPasswordLength.Visible = txtPassword.Text.Length > 32;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // Hide Copied label
            lblCopied.Visible = false;
        }

        private void BtnEncryptClick(object sender, EventArgs e)
        {
            // Clean up text
            txtInputFile.Text = txtInputFile.Text.Trim();
            txtOutputFile.Text = txtOutputFile.Text.Trim();

            // Ensure input and output are not the same.
            if (string.Equals(txtInputFile.Text, txtOutputFile.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show("Zdrojový a výstupný súbor nemožu byt rovnaké alebo prázdne.", "Chybný zdroj/výstup",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOutputFile.Focus();
                txtOutputFile.SelectAll();
                return;
            }

            // Ensure input file exists.
            if (!File.Exists(txtInputFile.Text))
            {
                MessageBox.Show("Zdrojový súbor neexistuje.");
                txtInputFile.Focus();
                txtInputFile.SelectAll();
                return;
            }

            // If output file exists, prompt to overwrite.
            if (File.Exists(txtOutputFile.Text))
            {
                if (MessageBox.Show(this, "Výstupny súbor už existuje. Želate si prepísať súbor?", "Prepísať súbor?",
                        MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    txtOutputFile.Focus();
                    txtOutputFile.SelectAll();
                    return;
                }
            }

            // Verify password:
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Nebolo zadané heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            // Confirm password:
            if (Settings.password_confirm)
            {
                var input = new FrmInputBox();
                input.prompt = "Zadajte heslo pre potvrdenie.";
                input.title = "Potvrdenie hesla";
                input.password = true;
                input.ShowDialog(); // Modal, blocking call

                if (input.cancelled)
                {
                    return;
                }

                // If password doesn't match, stop.
                if (input.result != txtPassword.Text)
                {
                    MessageBox.Show("Hesla sa nezhodujú. Zopakujte.", "Chyba", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (OwnerPassword != "")
                {
                    input.prompt = "Heslo pre editovanie bolo nastavené. Potvrdťe prosím heslo opät.";
                    input.title = "Potvrdenie hesla pre editovanie";
                    input.password = true;
                    input.ShowDialog();
                    if (input.cancelled)
                    {
                        return;
                    }

                    if (input.result != OwnerPassword)
                    {
                        MessageBox.Show("Heslo pre editovanie nie je rovnaké. Prosím, zopakujte.", "Chyba",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // See https://itextpdf.com/en/resources/faq/technical-support/itext-7/how-protect-already-existing-pdf-password
            try
            {
                // Set mouse cursor to wait.
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                // Encryption properties:
                var encryptionProperties = (int)Settings.encryption_type;

                // If specified, disable encrypting metadata.
                if (!Settings.encrypt_metadata)
                {
                    encryptionProperties |= EncryptionConstants.DO_NOT_ENCRYPT_METADATA;
                }

                // Set document options
                var documentOptions = 0;
                if (Settings.allow_printing)
                {
                    documentOptions |= EncryptionConstants.ALLOW_PRINTING;
                }

                if (Settings.allow_degraded_printing)
                {
                    documentOptions |= EncryptionConstants.ALLOW_DEGRADED_PRINTING;
                }

                if (Settings.allow_modifying)
                {
                    documentOptions |= EncryptionConstants.ALLOW_MODIFY_CONTENTS;
                }

                if (Settings.allow_modifying_annotations)
                {
                    documentOptions |= EncryptionConstants.ALLOW_MODIFY_ANNOTATIONS;
                }

                if (Settings.allow_copying)
                {
                    documentOptions |= EncryptionConstants.ALLOW_COPY;
                }

                if (Settings.allow_form_fill)
                {
                    documentOptions |= EncryptionConstants.ALLOW_FILL_IN;
                }

                if (Settings.allow_assembly)
                {
                    documentOptions |= EncryptionConstants.ALLOW_ASSEMBLY;
                }

                if (Settings.allow_screenreaders)
                {
                    documentOptions |= EncryptionConstants.ALLOW_SCREENREADERS;
                }

                var writerProperties = new WriterProperties(); // Set properties of output
                writerProperties.SetStandardEncryption(Encoding.ASCII.GetBytes(txtPassword.Text),
                    OwnerPassword == "" ? null : Encoding.ASCII.GetBytes(OwnerPassword), documentOptions,
                    encryptionProperties); // Enable encryption
                PdfUtils.EncryptPdf(txtInputFile.Text, txtOutputFile.Text, writerProperties);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba počas spracovania súboru: " + ex.Message, "Chyba", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                return;
            }

            ExecuteAfterSteps();
        }

        private void ExecuteAfterSteps()
        {
            // If launching a program:
            if (Settings.run_after)
            {
                // Attempt to run the program, passing the newly encrypted filename.
                try
                {
                    Process.Start(Settings.run_after_file, Settings.run_after_arguments + " " + txtOutputFile.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie je možné spustiť príkaz. Chyba: " + ex.Message, "Chyba", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            // If opening the output file:
            if (Settings.open_after)
            {
                // Attempt to launch the default app for the file.
                var psi = new ProcessStartInfo
                {
                    FileName = txtOutputFile.Text,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }

            // If launching Explorer:
            if (Settings.show_folder_after)
            {
                // Attempt to launch the folder with the file highlighted.
                var argument = "/select, \"" + txtOutputFile.Text + "\"";

                Process.Start("explorer.exe", argument);
            }

            // If closing after encryption
            if (Settings.close_after)
            {
                Close();
            }

            Cursor.Current = Cursors.Default; // Return to default cursor.
        }


        private void BtnDecryptClick(object sender, EventArgs e)
        {
            // Clean up text
            txtInputFile.Text = txtInputFile.Text.Trim();
            txtOutputFile.Text = txtOutputFile.Text.Trim();

            // Ensure input and output are not the same.
            if (string.Equals(txtInputFile.Text, txtOutputFile.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show("Zdrojový a výstupný súbor nemôžu byť rovnaké alebo prázdne.", "Chybný zdroj/výstup",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOutputFile.Focus();
                txtOutputFile.SelectAll();
                return;
            }

            // Ensure input file exists.
            if (!File.Exists(txtInputFile.Text))
            {
                MessageBox.Show("Zdrojový súbor neexistuje.");
                txtInputFile.Focus();
                txtInputFile.SelectAll();
                return;
            }


            // Verify password:
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Nebolo zadané heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            if (!PdfUtils.IsPasswordCorrect(txtInputFile.Text, txtPassword.Text))
            {
                MessageBox.Show("Nesprávné heslo. Skúste opäť.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (PdfUtils.IsPasswordWithFullPermissions(txtInputFile.Text, txtPassword.Text))
            {
                // If output file exists, prompt to overwrite.
                if (File.Exists(txtOutputFile.Text))
                {
                    if (MessageBox.Show(this, "Výstupny súbor už existuje. Želáte si prepísať súbor?",
                            "Prepísať súbor?",
                            MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        txtOutputFile.Focus();
                        txtOutputFile.SelectAll();
                        return;
                    }
                }

                // Write the un-protected PDF
                try
                {
                    PdfUtils.WriteDecryptedPdf(txtInputFile.Text, txtOutputFile.Text, txtPassword.Text);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    MessageBox.Show("Neznáma chyba!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("PDF je chránené aj heslom vlastníka. Potrebné zadať toto heslo.", "Informácia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ExecuteAfterSteps();
        }


        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settings = new frmSettings();
            settings.ShowDialog();
        }

        private void lnkPasswordOwner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var input = new FrmInputBox();
            input.title = "Nastaviť heslo pre editovanie";
            input.prompt =
                "Zadajte heslo pre editovanie.\r\n(Heslo pre editovanie umožní plnú kontrolu nad obsahom súboru PDF.)\r\nAk nebude heslo zadané, bude vygenerované náhodné heslo.\r\nStlačte Storno ak chcete anulovať heslo pre editovanie";
            input.password = true;
            input.ShowDialog();
            OwnerPassword = input.cancelled ? "" : input.result;
            lblOwnerPasswordSet.Visible = (OwnerPassword != "");
        }
    }
}