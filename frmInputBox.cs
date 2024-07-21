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
        }

        private void frmInputBox_Load(object sender, EventArgs e)
        {


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Result = txtInput.Text;
            PwdChanged = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Prompt = "";
            PwdChanged = true;
            this.Close();
        }

        private void frmInputBox_Shown(object sender, EventArgs e)
        {
            txtInput.Focus();
            txtInput.Text = ""; // Clear input on load.
            lblPrompt.Text = Prompt;
            Text = Title;
            txtInput.PasswordChar = (Password) ? '*' : (char)0;
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close the app
            PwdChanged = false;
            Close();
        }
    }
}
