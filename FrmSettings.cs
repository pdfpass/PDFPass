using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PDFPass
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            // Load encryption types: (Thanks https://stackoverflow.com/a/11745699/1502289)
            var encryptionTypes = new Dictionary<int, string>
            {
                { (int)Settings.EncryptionType.AES_256, "AES-256 (Adobe Reader 8+) [odporúčané]" },
                { (int)Settings.EncryptionType.AES_128, "AES-128 (Adobe Reader 7+" },
                { (int)Settings.EncryptionType.RC4_128, "RC4-128 (Adobe Reader 6+)" },
                // { (int)Settings.EncryptionType.RC4_40, "RC4-40 (Adobe Reader 3+) [neodporúčané]" }
            };

            // Attach datasource to combo box.
            cboEncryptionType.DataSource = new BindingSource(encryptionTypes, null);
            cboEncryptionType.DisplayMember = "Value";
            cboEncryptionType.ValueMember = "Key";

            // Load settings:
            Settings.Load(); // Load settings from registry.

            // Populate settings to form controls
            chkRun.Checked = Settings.run_after;
            txtRun.Text = Settings.run_after_file;
            txtArguments.Text = Settings.run_after_arguments;
            chkPasswordConfirmation.Checked = Settings.password_confirm;
            chkCloseAfterCompletion.Checked = Settings.close_after;
            chkShowFolder.Checked = Settings.show_folder_after;
            chkOpen.Checked = Settings.open_after;

            // Encryption options:
            cboEncryptionType.SelectedIndex =
                cboEncryptionType.FindStringExact(encryptionTypes[(int)Settings.encryption_type]);
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
            // Save settings

            // Validate RUN file
            if (chkRun.Checked)
            {
                if (!File.Exists(txtRun.Text))
                {
                    MessageBox.Show("Súbor pre spustenie neexistuje.");
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
      
        private void chkRun_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void linkDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openWebSite("https://www.paypal.com/donate/?hosted_button_id=5G336LA7YBMXQ");
        }

        private static void openWebSite(string urlToOpen)
        {
            // Method 1: Process.Start (All .NET platforms)
            var startInfo = new ProcessStartInfo(urlToOpen)
            {
                UseShellExecute = true // Essential for opening in default browser
            };
            Process.Start(startInfo);
        }

        private void txtOwnerPassword_TextChanged(object sender, EventArgs e)
        {
        }
    }
}