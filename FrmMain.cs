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
using PDFPass.MVP;

namespace PDFPass
{
    public partial class FrmMain : Form, IMainView
    {
        private MainPresenter _presenter;

        public FrmMain()
        {
            InitializeComponent();
            new MainPresenter(this, new MainModel());
            UpdateUiText();
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

        public string OwnerPassword { get; set; }

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
        public event EventHandler EncryptClick;
        public event EventHandler DecryptClick;
        public event EventHandler SettingsClick;
        public event EventHandler ChangeOwnerPasswordClick;
        public event EventHandler GeneratePasswordClick;
        public event EventHandler InputFileChanged;
        public event EventHandler OutputFileChanged;
        public event EventHandler CloseClick;
        #endregion

        #region Methods
        public void ShowError(string message)
        {
            MessageBox.Show(message, Strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool ShowWarning(string message)
        {
            return MessageBox.Show(message, Strings.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
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

        public string PromptForPassword(string title, string prompt)
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
            this.Text = Strings.ApplicationTitle;
            groupBox1.Text = Strings.InputFile;
            groupBox2.Text = Strings.OutputFile;
            groupBox3.Text = Strings.Passwords;
            gbWatermark.Text = Strings.Watermark;
            label1.Text = Strings.Text;
            label2.Text = Strings.SelectPathForEncryptedFile;
            label4.Text = Strings.SelectFileForEncryption;
            lblCopied.Text = Strings.CopiedToClipboard;
            lblPasswordLength.Text = Strings.PasswordLengthWarning;
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            lblVersion.Text = $"{Strings.Version}{Join(".", version.Split('.').Take(3))}";
            btnChangePassword.Text = Strings.Change;
            btnClose.Text = Strings.Close;
            btnCopy.Text = Strings.Copy;
            btnDecrypt.Text = Strings.Decrypt;
            btnEncrypt.Text = Strings.Encrypt;
            btnPasswordGenerate.Text = Strings.Generate;
            btnPaste.Text = Strings.Paste;
            btnSettings.Text = Strings.Settings;
            cbWatermark.Text = Strings.UseWatermark;
            txtPassword.PlaceholderText = Strings.EnterPassword;
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
            _presenter.OnFormLoad();
        }

        private void btnInputBrowse_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                InputFile = dlgOpen.FileName;
                InputFileChanged?.Invoke(this, EventArgs.Empty);
            }
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

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    InputFile = files[0];
                    InputFileChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1 && Path.GetExtension(files[0]).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
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
    }
}