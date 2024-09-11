using System;
using System.Windows.Forms;

namespace PDFPass
{
    public partial class FrmInputBox : Form
    {
        public string Result;   // The result of the input box action.
        public string Prompt;   // Prompt to be displayed.
        public string Title;    // Title of box
        public bool Password;   // Is the input a password?

        public bool PwdChanged;
        public FrmInputBox()
        {
            InitializeComponent();
            toolTipDefaultOwnerPassword.SetToolTip(btnDefaultOwnerPassword, "Vyplniť predvoleným heslom z Nastavení");
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
            lblPrompt.Text = Prompt;
            Text = Title;
            txtInput.PasswordChar = (Password) ? '*' : (char)0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtInput.Text = Settings.owner_password;
        }
    }
}