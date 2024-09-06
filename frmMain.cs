using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using Application = System.Windows.Forms.Application;
using Clipboard = System.Windows.Forms.Clipboard;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

// LOL

namespace PDFPass
{
    public partial class FrmMain : Form
    {
        const int PwLengthMin = 12; // Minimum generated password length
        const int PwLengthMax = 24; // Maximum generated password length

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


        private void InitFormControls()
        {
            if (!File.Exists(txtInputFile.Text))
            {
                return;
            }

            OwnerPassword = Settings.owner_password;
            var isInputEncrypted = PdfUtils.IsPdfReaderPasswordSet(txtInputFile.Text);
            txtOutputFile.Text = GetFilenameWithSuffix(txtInputFile.Text, isInputEncrypted);

            labelPassword.Text =
                isInputEncrypted ? "Zadať heslo pre odomknutie PDF:" : "Zadať heslo pre uzamknutie čítania:";
            btnEncrypt.Visible = !isInputEncrypted;
            btnDecrypt.Visible = isInputEncrypted;
            btnSettings.Visible = !isInputEncrypted;
            btnPasswordGenerate.Enabled = !isInputEncrypted;
            btnChangePassword.Visible = !isInputEncrypted;
            lblOwnerPasswordSet.Visible = !isInputEncrypted && !string.IsNullOrEmpty(OwnerPassword);
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
            Settings.Notify.Add(SettingsChanged);

            // Load settings from registry
            Settings.Load();

            InitFormControls();

            // If immediate run is enabled, click Run button (see command line options)
            if (EncryptOnStart)
            {
                // Click the Encrypt button immediately.
                BtnEncryptClick(sender, e);
            }

            // Show program version
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            lblVersion.Text = "Verzia: " + string.Join(".", version.Split('.').Take(3));
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
            // Set password
            txtPassword.Text = PdfUtils.GenerateRandomPassword(PwLengthMin, PwLengthMax);

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

            // Verify password if at least 1 pwd
            if (string.IsNullOrWhiteSpace(txtPassword.Text) && string.IsNullOrWhiteSpace(OwnerPassword))
            {
                MessageBox.Show("Nebolo zadané žiadne heslo! (potrebné minimálne 1)", "Chyba", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            // Warning about missing reading pwd
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                var dialogResult = MessageBox.Show("Nebolo zadané heslo pre uzamknutie čitania! Pokračovať?",
                    "Upozornenie", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.No)
                {
                    txtPassword.Focus();
                    return;
                }
            }


            // Confirm password:
            if (Settings.password_confirm)
            {
                var input = new FrmInputBox();
                input.Prompt = "Zadajte heslo uzamknutia čítania pre potvrdenie.";
                input.Title = "Potvrdenie hesla uzamknutia čítania";
                input.Password = true;
                input.ShowDialog(); // Modal, blocking call

                if (!input.PwdChanged)
                {
                    return;
                }

                // If password doesn't match, stop.
                if (input.Result != txtPassword.Text)
                {
                    MessageBox.Show("Hesla sa nezhodujú. Zopakujte.", "Chyba", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (OwnerPassword != "")
                {
                    input.Prompt = "Heslo vlastníka bolo nastavené. Potvrdťe prosím heslo opät.";
                    input.Title = "Potvrdenie hesla vlastníka";
                    input.Password = true;
                    input.ShowDialog();
                    if (!input.PwdChanged)
                    {
                        return;
                    }

                    if (input.Result != OwnerPassword)
                    {
                        MessageBox.Show("Heslo vlastníka nie je rovnaké. Prosím, zopakujte.", "Chyba",
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
                    string.IsNullOrEmpty(OwnerPassword) ? null : Encoding.ASCII.GetBytes(OwnerPassword),
                    documentOptions,
                    encryptionProperties); // Enable encryption
                PdfUtils.WriteEncryptedPdf(txtInputFile.Text, txtOutputFile.Text, writerProperties);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neznáma chyba počas spracovania súboru: " + ex.Message, "Chyba", MessageBoxButtons.OK,
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
            // Calculate the center position
            var posX = this.Location.X + (this.Width - settings.Width) / 2;
            var posY = this.Location.Y + (this.Height - settings.Height) / 2;

            // Ensure the position is not negative
            posX = Math.Max(0, posX);
            posY = Math.Max(0, posY);

            // Set the start position manually and set the location to the calculated position
            settings.StartPosition = FormStartPosition.Manual;
            settings.Location = new Point(posX, posY);
            settings.ShowDialog();
            InitFormControls();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            var input = new FrmInputBox();
            input.Title = "Nastaviť heslo vlastníka";
            input.Prompt =
                "Zadajte heslo vlastníka.\r\n(Heslo vlastníka obmedzí manipuláciu s obsahom PDF)\r\n\r\nStlačte STORNO, ak chcete ZRUŠIŤ heslo vlastníka";
            input.Password = true;
            input.ShowDialog();

            if (input.PwdChanged)
            {
                OwnerPassword = input.Result;
            }

            if (!string.IsNullOrEmpty(OwnerPassword))
            {
                lblOwnerPasswordSet.Text = "Heslo vlastníka nastavené.";
                lblOwnerPasswordSet.ForeColor = Color.FromArgb(0, 192, 192);
            }
            else
            {
                lblOwnerPasswordSet.Text = "Heslo vlastníka prázdné.";
                lblOwnerPasswordSet.ForeColor = Color.FromArgb(192, 0, 0);
            }
        }
    }
}