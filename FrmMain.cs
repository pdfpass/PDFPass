using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using PDFPass.Resources;
using static System.Environment;
using static System.String;
using Application = System.Windows.Forms.Application;
using Clipboard = System.Windows.Forms.Clipboard;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

namespace PDFPass
{
    public partial class FrmMain : Form
    {
        private const int PwLengthMin = 12; // Minimum generated password length
        private const int PwLengthMax = 24; // Maximum generated password length

        public string OwnerPassword = ""; // The owner password, if any.
        public bool EncryptOnStart = false; // Allows encryption via command line without user interaction


        public FrmMain()
        {
            InitializeComponent();

            // Update UI with localized text
            UpdateUiText();

            // Subscribe to language change events
            LocalizationManager.LanguageChanged += (sender, e) => UpdateUiText();
        }

        private void UpdateUiText()
        {
            // Update form title
            this.Text = Strings.ApplicationTitle;

            // Update group boxes
            groupBox1.Text = Strings.InputFile;
            groupBox2.Text = Strings.OutputFile;
            groupBox3.Text = Strings.Passwords;
            gbWatermark.Text = Strings.Watermark;

            // Update labels
            label1.Text = Strings.Text;
            label2.Text = Strings.SelectPathForEncryptedFile;
            label4.Text = Strings.SelectFileForEncryption;
            labelPassword.Text = IsInputEncrypted() ? Strings.PasswordForUnlocking : Strings.PasswordForLocking;
            lblCopied.Text = Strings.CopiedToClipboard;
            lblOwnerPasswordSet.Text = IsNullOrEmpty(OwnerPassword)
                ? Strings.OwnerPasswordEmpty
                : Strings.OwnerPasswordSet;
            lblPasswordLength.Text = Strings.PasswordLengthWarning;
            // Show program version
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            lblVersion.Text = $"{Strings.Version}{Join(".", version.Split('.').Take(3))}";


            // Update buttons
            btnChangePassword.Text = Strings.Change;
            btnClose.Text = Strings.Close;
            btnCopy.Text = Strings.Copy;
            btnDecrypt.Text = Strings.Decrypt;
            btnEncrypt.Text = Strings.Encrypt;
            btnPasswordGenerate.Text = Strings.Generate;
            btnPaste.Text = Strings.Paste;
            btnSettings.Text = Strings.Settings;

            // Update checkbox
            cbWatermark.Text = Strings.UseWatermark;

            // Update placeholders
            txtPassword.PlaceholderText = Strings.EnterPassword;

            // Update combobox items - only if not already populated
            if (cmbWatermark.Items.Count == 0 || cmbWatermark.Items[0]?.ToString() != Strings.Sample)
            {
                cmbWatermark.Items.Clear();
                cmbWatermark.Items.Add(Strings.Sample);
                cmbWatermark.Items.Add(Strings.WCopy);
                cmbWatermark.Items.Add(Strings.Confidential);
                cmbWatermark.Items.Add(Strings.Draft);
                if (cmbWatermark.SelectedIndex < 0 && cmbWatermark.Items.Count > 0)
                {
                    cmbWatermark.SelectedIndex = 0;
                }
            }

            // Update file dialogs
            var fileFilter = $"{Strings.PDFFiles}|*.pdf|{Strings.AllFiles}|*.*";
            dlgOpen.Filter = fileFilter;
            dlgSave.Filter = fileFilter;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Add listener for updated settings
            Settings.Notify.Add(SettingsChanged);

            // Load settings from registry
            Settings.Load();

            if (Settings.always_default_owner_password)
            {
                OwnerPassword = Settings.owner_password;
            }

            var fileStatus = getFileStatus(txtInputFile.Text);
            if (fileStatus is FileStatus.Ok or FileStatus.Empty)
            {
                UpdateView();
            }
            else
            {
                if (fileStatus == FileStatus.NotPdf) processInputFileStatus(fileStatus);
            }

            // If immediate run is enabled, click Run button (see command line options)
            if (EncryptOnStart)
            {
                // Click the Encrypt button immediately.
                BtnEncryptClick(sender, e);
            }
        }


        private void UpdateView()
        {
            btnPaste.Visible = IsNullOrEmpty(txtPassword.Text);
            btnCopy.Visible = !btnPaste.Visible;
            btnPaste.Enabled = !IsNullOrWhiteSpace(Clipboard.GetText());

            var fileName = txtInputFile.Text;
            if (!File.Exists(fileName))
            {
                return;
            }

            try
            {
                PdfUtils.IsPdfFile(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(Strings.FileNotPdfOrDamaged, Strings.ErrorTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInputFile.Text = Empty;
                return;
            }

            var isInputEncrypted = PdfUtils.IsPdfReaderPasswordSet(fileName);

            if (IsNullOrEmpty(txtOutputFile.Text))
            {
                txtOutputFile.Text = GetFilenameWithSuffix(fileName, isInputEncrypted);
            }

            labelPassword.Text = isInputEncrypted ? Strings.PasswordForUnlocking : Strings.PasswordForLocking;
            btnEncrypt.Visible = !isInputEncrypted;
            btnDecrypt.Visible = isInputEncrypted;
            btnSettings.Visible = !isInputEncrypted;
            btnPasswordGenerate.Enabled = !isInputEncrypted;
            btnChangePassword.Enabled = !isInputEncrypted;
            lblOwnerPasswordSet.Visible = !isInputEncrypted;
            gbWatermark.Visible = !isInputEncrypted;
            Height = isInputEncrypted ? 500 : 560;
            lblOwnerPasswordSet.ForeColor = IsNullOrEmpty(OwnerPassword)
                ? Color.FromArgb(255, 153, 0)
                : Color.FromArgb(0, 192, 192);
            lblOwnerPasswordSet.Text = IsNullOrEmpty(OwnerPassword)
                ? Strings.OwnerPasswordEmpty
                : Strings.OwnerPasswordSet;
            if (isInputEncrypted)
            {
                btnClose.Location = new Point(291, 413);
                btnDecrypt.Location = new Point(409, 413);
            }
            else
            {
                btnClose.Location = new Point(291, 473);
            }
        }

        private static FileStatus getFileStatus(string fileName)
        {
            if (IsNullOrWhiteSpace(fileName))
            {
                return FileStatus.Empty;
            }

            if (!File.Exists(fileName))
            {
                return FileStatus.Notexists;
            }

            try
            {
                PdfUtils.IsPdfFile(fileName);
            }
            catch (Exception e)
            {
                return FileStatus.NotPdf;
            }

            return FileStatus.Ok;
        }

        private void processInputFileStatus(FileStatus fileStatus)
        {
            var messageText = Empty;
            switch (fileStatus)
            {
                case FileStatus.NotPdf:
                    messageText = Strings.FileNotPdfOrDamaged;
                    txtInputFile.Text = Empty;
                    break;
                case FileStatus.Notexists:
                    messageText = Strings.FileNotExist;
                    txtInputFile.Text = Empty;
                    break;
                case FileStatus.Ok:
                    break;
                default:
                    messageText = Strings.Warning;
                    break;
            }

            UpdateView();
            MessageBox.Show(messageText, Strings.ErrorTitle,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool IsInputEncrypted()
        {
            if (IsNullOrEmpty(txtInputFile.Text) || !File.Exists(txtInputFile.Text))
                return false;

            return PdfUtils.IsPdfReaderPasswordSet(txtInputFile.Text);
        }

        private void btnInputBrowse_Click(object sender, EventArgs e)
        {
            var result = dlgOpen.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return;
            }

            var fileStatus = getFileStatus(dlgOpen.FileName);
            if (fileStatus == FileStatus.Ok)
            {
                txtInputFile.Text = dlgOpen.FileName;
                setOutputFilename(txtInputFile.Text, IsInputEncrypted());
                UpdateView();
            }
            else
            {
                processInputFileStatus(fileStatus);
            }
        }

        private void setOutputFilename(string inputFileName, bool isInputEncrypted)
        {
            txtOutputFile.Text = GetFilenameWithSuffix(inputFileName, isInputEncrypted);
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

        private static void SettingsChanged()
        {
            // This function is executed when settings change.
            Console.WriteLine(Strings.SettingsChanged);
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

            if (IsNullOrEmpty(txtPassword.Text)) return;

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
            UpdateView();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // Hide Copied label
            lblCopied.Visible = false;
            UpdateView();
        }

        private void BtnEncryptClick(object sender, EventArgs e)
        {
            // If output file exists, prompt to overwrite.
            if (File.Exists(txtOutputFile.Text))
            {
                if (MessageBox.Show(this, Strings.ConfirmOverwriteFile, Strings.OverwriteFile,
                        MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    txtOutputFile.Focus();
                    txtOutputFile.SelectAll();
                    return;
                }
            }

            // Warning about missing reading pwd
            if (IsNullOrWhiteSpace(txtPassword.Text))
            {
                var dialogResult = MessageBox.Show(Strings.NoReadingPasswordWarning,
                    Strings.Warning, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    txtPassword.Focus();
                    return;
                }
            }

            // Confirm password:
            if (Settings.password_confirm)
            {
                var input = new FrmInputBox();
                input.Prompt = Strings.ConfirmReadingPassword;
                input.Title = Strings.ConfirmReadingPasswordTitle;
                input.Password = true;
                input.ShowDialog(); // Modal, blocking call

                if (!input.PwdChanged)
                {
                    return;
                }

                // If password doesn't match, stop.
                if (input.Result != txtPassword.Text)
                {
                    MessageBox.Show(Strings.PasswordsMismatch, Strings.ErrorTitle, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (OwnerPassword != "")
                {
                    input.Prompt = Strings.OwnerPasswordSetConfirm;
                    input.Title = Strings.ConfirmOwnerPasswordTitle;
                    input.Password = true;
                    input.ShowDialog();
                    if (!input.PwdChanged)
                    {
                        return;
                    }

                    if (input.Result != OwnerPassword)
                    {
                        MessageBox.Show(Strings.OwnerPasswordMismatch, Strings.ErrorTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            var processor = new PdfFileProcessor();
            var result = processor.Encrypt(txtInputFile.Text, txtOutputFile.Text, txtPassword.Text, OwnerPassword, cbWatermark.Checked, cmbWatermark.Text);

            Cursor.Current = Cursors.Default;

            if (result.Success)
            {
                ExecuteAfterSteps();
            }
            else
            {
                MessageBox.Show(result.Message, Strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetFilenameWithSuffix(string fileName, bool isInputEncrypted)
        {
            var newFileName = isInputEncrypted
                ? $"{Path.GetFileNameWithoutExtension(fileName).Replace(Strings.Encrypted, "")}{Strings.Decrypted}.pdf"
                : $"{Path.GetFileNameWithoutExtension(fileName)}-{Strings.Encrypted}.pdf";
            return Path.Combine(Path.GetDirectoryName(fileName)!, newFileName);
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
                    MessageBox.Show($@"{Strings.CannotRunCommand}{ex.Message}", Strings.ErrorTitle,
                        MessageBoxButtons.OK,
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
            // If output file exists, prompt to overwrite.
            if (File.Exists(txtOutputFile.Text))
            {
                if (MessageBox.Show(this, Strings.ConfirmOverwriteFile, Strings.OverwriteFile,
                        MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    txtOutputFile.Focus();
                    txtOutputFile.SelectAll();
                    return;
                }
            }

            var processor = new PdfFileProcessor();
            var result = processor.Decrypt(txtInputFile.Text, txtOutputFile.Text, txtPassword.Text);

            if (result.Success)
            {
                ExecuteAfterSteps();
            }
            else
            {
                var icon = result.Message == Strings.OwnerPasswordRequired ? MessageBoxIcon.Warning : MessageBoxIcon.Error;
                MessageBox.Show(result.Message, Strings.ErrorTitle, MessageBoxButtons.OK, icon);
            }
        }


        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settings = new FrmSettings();
            // Calculate the center position
            var posX = Location.X + (Width - settings.Width) / 2;
            var posY = Location.Y + (Height - settings.Height) / 2;

            // Ensure the position is not negative
            posX = Math.Max(0, posX);
            posY = Math.Max(0, posY);

            // Set the start position manually and set the location to the calculated position
            settings.StartPosition = FormStartPosition.Manual;
            settings.Location = new Point(posX, posY);
            settings.ShowDialog();
            UpdateView();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            var input = new FrmInputBox();
            input.Title = Strings.SetOwnerPassword;
            input.Prompt = Strings.EnterOwnerPasswordPrompt;
            input.Password = true;
            input.ShowDialog();


            if (!input.PwdChanged) return;

            OwnerPassword = input.Result;
            UpdateView();
        }

        private void cbWatermark_CheckedChanged(object sender, EventArgs e)
        {
            cmbWatermark.Enabled = cbWatermark.Checked;
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtPassword.Text = Clipboard.GetText();
        }

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null && !e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var files = (string[])e.Data?.GetData(DataFormats.FileDrop);
            var filename = files?[0];

            if (filename == null) return;

            txtInputFile.Text = filename;

            setOutputFilename(txtInputFile.Text, IsInputEncrypted());
            UpdateView();
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the dropped data contains a file
            if (e.Data == null) return;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Check if the dropped file is a PDF
                if (files is { Length: 1 } &&
                    Path.GetExtension(files[0])!.Equals(".pdf", StringComparison.CurrentCultureIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy; // Allow dropping the file
                }
                else
                {
                    e.Effect = DragDropEffects.None; // Don't allow dropping other files or multiple files
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void btnPaste_MouseHover(object sender, EventArgs e)
        {
            btnPasteTooltip.SetToolTip(btnPaste,
                btnPaste.Enabled ? $"{Strings.ClipboardValuePrefix}{Clipboard.GetText()}'" : Empty);
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            btnSettings_Click(null, null);
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            var tooltip = Empty;
            var availableLanguages = LanguageHelper.AvailableLanguages;

            foreach (var key in availableLanguages.Keys)
            {
                var value = LocalizationManager.ResourceManager.GetString("SetLanguage", new CultureInfo(key));
                tooltip = tooltip + value + NewLine;
            }

            languageToolTip.SetToolTip(pbLanguage, tooltip);
        }
    }

    internal enum FileStatus
    {
        Notexists,
        NotPdf,
        Empty,
        Ok
    }
}