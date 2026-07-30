using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PDFPass.MVP;
using PDFPass.Resources;
using static System.Environment;
using static System.String;
using Clipboard = System.Windows.Forms.Clipboard;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

namespace PDFPass
{
    public partial class FrmMain : Form, IMainView
    {
        private MainPresenter? _presenter;

        public FrmMain()
        {
            InitializeComponent();

            // Update UI with localized text
            UpdateUiText();

            // Subscribe to language change events
            LocalizationManager.LanguageChanged += (sender, e) => UpdateUiText();
        }

        public void SetPresenter(MainPresenter presenter)
        {
            _presenter = presenter;
        }

        #region Properties

        public string InputFile
        {
            get => txtInputFile.Text;
            set => txtInputFile.Text = value;
        }

        public string OutputFile
        {
            get => txtOutputFile.Text;
            set => txtOutputFile.Text = value;
        }

        public string UserPassword
        {
            get => txtPassword.Text;
            set => txtPassword.Text = value;
        }

        public string? OwnerPassword { get; set; }

        public bool WatermarkEnabled
        {
            get => cbWatermark.Checked;
            set => cbWatermark.Checked = value;
        }

        public string WatermarkText
        {
            get => cmbWatermark.Text;
            set => cmbWatermark.Text = value;
        }

        public bool EncryptOnStart { get; set; }

        #endregion

        #region Events

        public event EventHandler? EncryptClick;
        public event EventHandler? DecryptClick;
        public event EventHandler? SettingsClick;
        public event EventHandler? ChangeOwnerPasswordClick;
        public event EventHandler? GeneratePasswordClick;
        public event EventHandler? InputFileChanged;
        public event EventHandler? CloseClick;

        #endregion

        #region Methods

        public void ShowError(string message)
        {
            MessageBox.Show(message, Strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool ShowWarning(string message)
        {
            return MessageBox.Show(message, Strings.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                   DialogResult.Yes;
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, Strings.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdateView(bool isInputEncrypted)
        {
            btnPaste.Visible = IsNullOrEmpty(txtPassword.Text);
            btnCopy.Visible = !btnPaste.Visible;
            btnPaste.Enabled = !IsNullOrWhiteSpace(Clipboard.GetText());

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

        public void CloseForm()
        {
            Close();
        }

        public bool ConfirmOverwrite()
        {
            return MessageBox.Show(this, Strings.ConfirmOverwriteFile, Strings.OverwriteFile,
                MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public string? PromptForPassword(string title, string prompt)
        {
            var input = new FrmInputBox
            {
                Title = title,
                Prompt = prompt,
                Password = true
            };
            input.ShowDialog();
            return input.PwdChanged ? input.Result : null;
        }

        #endregion

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
            lblCopied.Text = Strings.CopiedToClipboard;
            lblPasswordLength.Text = Strings.PasswordLengthWarning;
            // Show program version
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            lblVersion.Text = $"{Strings.Version}{Join(".", (version ?? "1.0.0").Split('.').Take(3))}";


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

            var fileFilter = $"{Strings.PDFFiles}|*.pdf|{Strings.AllFiles}|*.*";
            dlgOpen.Filter = fileFilter;
            dlgSave.Filter = fileFilter;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (_presenter == null)
            {
                throw new InvalidOperationException("Presenter is not set.");
            }
        }

        private void btnInputBrowse_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() != DialogResult.OK) return;
            InputFile = dlgOpen.FileName;
            InputFileChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnOutputBrowse_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                OutputFile = dlgSave.FileName;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnPasswordGenerate_Click(object sender, EventArgs e)
        {
            GeneratePasswordClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!IsNullOrEmpty(txtPassword.Text))
            {
                Clipboard.SetText(txtPassword.Text);
                lblCopied.Visible = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblCopied.Visible = false;
            lblPasswordLength.Visible = txtPassword.Text.Length > 32;
            InputFileChanged?.Invoke(this, EventArgs.Empty);
        }

        private void BtnEncryptClick(object sender, EventArgs e)
        {
            EncryptClick?.Invoke(this, EventArgs.Empty);
        }

        private void BtnDecryptClick(object sender, EventArgs e)
        {
            DecryptClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangeOwnerPasswordClick?.Invoke(this, EventArgs.Empty);
        }

        private void cbWatermark_CheckedChanged(object sender, EventArgs e)
        {
            cmbWatermark.Enabled = cbWatermark.Checked;
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtPassword.Text = Clipboard.GetText();
        }

        private void btnPaste_MouseHover(object sender, EventArgs e)
        {
            // Keep the Paste button state in sync with the clipboard content when hovering.
            btnPaste.Enabled = !IsNullOrWhiteSpace(Clipboard.GetText());
            // Provide a simple tooltip hint.
            btnPasteTooltip.SetToolTip(btnPaste, btnPaste.Enabled ? btnPaste.Text : "Clipboard is empty");
        }

        private void FrmMain_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = e.Data.GetData(DataFormats.FileDrop);
                var files = data as string[];
                if (files != null && files.Length > 0)
                {
                    InputFile = files[0];
                    InputFileChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void FrmMain_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = e.Data.GetData(DataFormats.FileDrop);
                var files = data as string[] ?? Array.Empty<string>();
                if (files.Length == 1 && Path.GetExtension(files[0]).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
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

        private void txtInputFile_TextChanged(object sender, EventArgs e)
        {
            InputFileChanged?.Invoke(this, EventArgs.Empty);
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the visible primary action and suppress the default beep.
                if (btnEncrypt.Visible && btnEncrypt.Enabled)
                {
                    BtnEncryptClick(btnEncrypt, EventArgs.Empty);
                }
                else if (btnDecrypt.Visible && btnDecrypt.Enabled)
                {
                    BtnDecryptClick(btnDecrypt, EventArgs.Empty);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            btnSettings_Click(null!, EventArgs.Empty);
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            var tooltip = Empty;
            var availableLanguages = LanguageHelper.AvailableLanguages;

            tooltip = availableLanguages.Keys.Select(key => LocalizationManager.ResourceManager.GetString("SetLanguage",
                    new CultureInfo(key)))
                .Aggregate(tooltip,
                    (current,
                        value) => current + value + NewLine);

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