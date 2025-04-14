using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PDFPass.Resources;

namespace PDFPass
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();

            // Update UI text from resources
            UpdateUiText();

            // Initialize language selection
            LanguageHelper.InitializeLanguageComboBox(comboBoxLanguage, Settings.language);

            // Subscribe to language change events
            LocalizationManager.LanguageChanged += (sender, e) => UpdateUiText();
        }

        private void UpdateUiText()
        {
            // Update form title
            this.Text = Strings.SettingsTitle;

            // Update labels
            label1.Text = Strings.Copyright;
            label2.Text = Strings.PermissionsForOutputFile;
            label3.Text = Strings.Algorithm;
            label4.Text = Strings.PermissionsIgnoredWarning;
            label5.Text = Strings.Parameters;
            label6.Text = Strings.OutputFilePathAdded;
            lblOwnerPassword.Text = Strings.OwnerPassword;

            // Update buttons
            btnOK.Text = Strings.Confirm;
            btnCancel.Text = Strings.Cancel;
            // ReSharper disable once LocalizableElement
            btnRunBrowse.Text = "..."; // This is not a text to localize

            // Update group boxes
            groupBox1.Text = Strings.AfterSuccessfulEncryption;
            groupBox2.Text = Strings.EncryptionOptions;
            groupBoxLanguage.Text = Strings.LanguageTitle;

            // Update checkboxes
            chkOpen.Text = Strings.OpenOutputFile;
            chkShowFolder.Text = Strings.OpenFileInExplorer;
            chkCloseAfterCompletion.Text = Strings.ClosePDFPass;
            chkRun.Text = Strings.RunProgram;
            chkAlwaysDefaultOwnerPassword.Text = Strings.SetAutomatically;
            chkDegradedPrinting.Text = Strings.AllowPrintingLowRes;
            chkAssembly.Text = Strings.AllowPageArrangement;
            chkScreenreaders.Text = Strings.AllowAccessTechnologies;
            chkForms.Text = Strings.AllowFillForm;
            chkNotations.Text = Strings.AllowAddAnnotations;
            chkModifying.Text = Strings.AllowDocumentModification;
            chkCopying.Text = Strings.AllowContentCopy;
            chkPrinting.Text = Strings.AllowPrintingHighRes;
            chkEncryptMetadata.Text = Strings.EncryptMetadata;
            chkPasswordConfirmation.Text = Strings.ConfirmPassword;

            // Update placeholders and tooltips
            txtArguments.PlaceholderText = Strings.ParametersPlaceholder;
            txtRun.PlaceholderText = Strings.ProgramPathPlaceholder;
            txtOwnerPassword.PlaceholderText = Strings.PermanentOwnerPassword;

            // Update link label
            linkDonate.Text = Strings.SupportDeveloper;

            // Update dialog filter
            dlgOpen.Filter = new StringBuilder().Append(Strings.ExecutableFiles)
                .Append("|*.exe;*.bat;*.com|")
                .Append(Strings.AllFiles)
                .Append("|*.*")
                .ToString();

            // Load encryption types with localized descriptions
            var encryptionTypes = new Dictionary<int, string>
            {
                { (int)Settings.EncryptionType.AES_256, Strings.AES256Recommended },
                { (int)Settings.EncryptionType.AES_128, Strings.AES128 },
                { (int)Settings.EncryptionType.RC4_128, Strings.RC4128 }
            };

            // Attach datasource to combo box.
            cboEncryptionType.DataSource = new BindingSource(encryptionTypes, null);
            cboEncryptionType.DisplayMember = "Value";
            cboEncryptionType.ValueMember = "Key";

            //Set combo to value from Settings (Registry)
            cboEncryptionType.SelectedIndex =
                cboEncryptionType.FindStringExact(encryptionTypes[(int)Settings.encryption_type]);
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            // Load settings from registry.
            Settings.Load();

            // Populate settings to form controls
            chkRun.Checked = Settings.run_after;
            txtRun.Text = Settings.run_after_file;
            txtArguments.Text = Settings.run_after_arguments;
            chkPasswordConfirmation.Checked = Settings.password_confirm;
            chkCloseAfterCompletion.Checked = Settings.close_after;
            chkShowFolder.Checked = Settings.show_folder_after;
            chkOpen.Checked = Settings.open_after;

            // Encryption options:
            chkEncryptMetadata.Checked = Settings.encrypt_metadata;
            chkPrinting.Checked = Settings.allow_printing;
            chkDegradedPrinting.Checked = Settings.allow_degraded_printing;
            chkModifying.Checked = Settings.allow_modifying;
            chkNotations.Checked = Settings.allow_modifying_annotations;
            chkCopying.Checked = Settings.allow_copying;
            chkForms.Checked = Settings.allow_form_fill;
            chkAssembly.Checked = Settings.allow_assembly;
            chkScreenreaders.Checked = Settings.allow_screenreaders;
            txtOwnerPassword.Text = Settings.owner_password;
            chkAlwaysDefaultOwnerPassword.Checked = Settings.always_default_owner_password;
        }

        private void btnRunBrowse_Click(object sender, EventArgs e)
        {
            var result = dlgOpen.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return;
            }

            txtRun.Text = dlgOpen.FileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Validate RUN file
            if (chkRun.Checked)
            {
                if (!File.Exists(txtRun.Text))
                {
                    MessageBox.Show(Strings.RunFileNotExists, Strings.ErrorTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRun.Focus();
                    txtRun.SelectAll();
                    return;
                }
            }

            // Save settings
            Settings.run_after = chkRun.Checked;
            Settings.run_after_file = txtRun.Text;
            Settings.run_after_arguments = txtArguments.Text;
            Settings.password_confirm = chkPasswordConfirmation.Checked;
            Settings.close_after = chkCloseAfterCompletion.Checked;
            Settings.show_folder_after = chkShowFolder.Checked;
            Settings.open_after = chkOpen.Checked;

            // Encryption options
            if (cboEncryptionType.SelectedValue != null)
                Settings.encryption_type = (Settings.EncryptionType)cboEncryptionType.SelectedValue;
            Settings.encrypt_metadata = chkEncryptMetadata.Checked;
            Settings.allow_printing = chkPrinting.Checked;
            Settings.allow_degraded_printing = chkDegradedPrinting.Checked;
            Settings.allow_modifying = chkModifying.Checked;
            Settings.allow_modifying_annotations = chkNotations.Checked;
            Settings.allow_copying = chkCopying.Checked;
            Settings.allow_form_fill = chkForms.Checked;
            Settings.allow_assembly = chkAssembly.Checked;
            Settings.allow_screenreaders = chkScreenreaders.Checked;
            Settings.owner_password = txtOwnerPassword.Text;
            Settings.always_default_owner_password = chkAlwaysDefaultOwnerPassword.Checked;

            Settings.Save();

            // Close settings window
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWebSite("https://www.paypal.com/donate/?hosted_button_id=5G336LA7YBMXQ");
        }

        private static void OpenWebSite(string urlToOpen)
        {
            var startInfo = new ProcessStartInfo(urlToOpen)
            {
                UseShellExecute = true // Essential for opening in default browser
            };
            Process.Start(startInfo);
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save language setting
            var selectedLanguage = LanguageHelper.GetSelectedLanguage(comboBoxLanguage);
            if (selectedLanguage != Settings.language)
            {
                // Language changed, apply it
                Settings.language = selectedLanguage;
                LanguageHelper.ApplyLanguageChange(selectedLanguage);

                // // Show a message that changes will fully apply after restart
                // MessageBox.Show(
                //     Strings.LanguageChangeRestartRequired,
                //     Strings.Information,
                //     MessageBoxButtons.OK,
                //     MessageBoxIcon.Information);
            }

            Settings.Save();
        }
    }
}