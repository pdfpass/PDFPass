using System;
using System.Windows.Forms;
using PDFPass.Resources;

namespace PDFPass
{
    public partial class FrmInputBox : Form
    {
        public string Result; // The result of the input box action.
        public string Prompt; // Prompt to be displayed.
        public string Title; // Title of box
        public bool Password; // Is the input a password?

        public bool PwdChanged;

        public FrmInputBox()
        {
            InitializeComponent();

            // Update UI text from resources
            UpdateUIText();

            // Subscribe to language change events
            LocalizationManager.LanguageChanged += (sender, e) => UpdateUIText();
        }

        private void UpdateUIText()
        {
            // Update buttons
            btnOK.Text = Strings.Confirm;
            btnCancel.Text = Strings.Cancel;
            btnClose.Text = Strings.Close;
            btnDefaultOwnerPassword.Text = "🢤"; // Special character - don't localize

            // Update placeholders
            txtInput.PlaceholderText = Strings.EnterPassword;

            // Update tooltips
            toolTipDefaultOwnerPassword.SetToolTip(btnDefaultOwnerPassword, Strings.FillWithDefaultPassword);

            // Note: Don't update Title and Prompt here as they are set dynamically
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Result = txtInput.Text;
            PwdChanged = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = string.Empty;
            PwdChanged = true;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            PwdChanged = false;
            Close();
        }

        private void frmInputBox_Shown(object sender, EventArgs e)
        {
            txtInput.Focus();
            txtInput.Text = ""; // Clear input on load.
            lblPrompt.Text = Prompt; // Set dynamic prompt
            Text = Title; // Set dynamic title
            txtInput.PasswordChar = (Password) ? '*' : (char)0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtInput.Text = Settings.owner_password;
        }
    }
}