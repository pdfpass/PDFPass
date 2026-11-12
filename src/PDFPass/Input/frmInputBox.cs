using System;
using System.Drawing;
using System.Windows.Forms;
using PDFPass.Localization;
using PDFPass.Preferences;
using PDFPass.Resources;

namespace PDFPass.Input
{
    public partial class FrmInputBox : Form
    {
        public string Result = string.Empty; // The result of the input box action.
        public string? Prompt; // Prompt to be displayed.
        public string? Title; // Title of box
        public bool Password; // Is the input a password?

        public bool PwdChanged;

        public FrmInputBox()
        {
            InitializeComponent();
            ApplyColors();

            // Update UI text from resources
            UpdateUiText();

            // Subscribe to language change events
            LocalizationManager.LanguageChanged += (_, _) => UpdateUiText();
        }

        private void ApplyColors()
        {
            var foreground = ColorTranslator.FromHtml("#15130f");
            BackColor = ColorTranslator.FromHtml("#f5f4f0");
            ForeColor = foreground;
            lblPrompt.BackColor = BackColor;
            lblPrompt.ForeColor = foreground;
            txtInput.BackColor = Color.White;
            txtInput.ForeColor = foreground;
            txtInput.BorderStyle = BorderStyle.FixedSingle;

            var focusLine = new Label
            {
                BackColor = ColorTranslator.FromHtml("#f2b829"),
                AutoSize = false,
                Height = 2,
                Visible = false
            };
            txtInput.Parent!.Controls.Add(focusLine);

            void UpdateFocusLine()
            {
                focusLine.SetBounds(txtInput.Left, txtInput.Bottom - focusLine.Height, txtInput.Width,
                    focusLine.Height);
                focusLine.Visible = txtInput.Focused;
                focusLine.BringToFront();
            }

            txtInput.GotFocus += (_, _) => UpdateFocusLine();
            txtInput.LostFocus += (_, _) => UpdateFocusLine();
            txtInput.LocationChanged += (_, _) => UpdateFocusLine();
            txtInput.SizeChanged += (_, _) => UpdateFocusLine();

            foreach (var button in new[] { btnOK, btnCancel, btnClose, btnDefaultOwnerPassword })
            {
                button.UseVisualStyleBackColor = false;
                button.BackColor = Color.White;
                button.ForeColor = foreground;
            }

            btnOK.BackColor = ColorTranslator.FromHtml("#f2b829");
        }

        private void UpdateUiText()
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