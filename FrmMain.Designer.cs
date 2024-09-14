using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PDFPass
{
	partial class FrmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            groupBox1 = new GroupBox();
            label4 = new Label();
            btnInputBrowse = new Button();
            txtInputFile = new TextBox();
            groupBox2 = new GroupBox();
            label2 = new Label();
            btnOutputBrowse = new Button();
            txtOutputFile = new TextBox();
            groupBox3 = new GroupBox();
            btnChangePassword = new Button();
            lblOwnerPasswordSet = new Label();
            lblCopied = new Label();
            btnCopy = new Button();
            labelPassword = new Label();
            btnPasswordGenerate = new Button();
            txtPassword = new TextBox();
            lblPasswordLength = new Label();
            btnDecrypt = new Button();
            btnEncrypt = new Button();
            btnClose = new Button();
            dlgOpen = new OpenFileDialog();
            dlgSave = new SaveFileDialog();
            btnSettings = new Button();
            lblVersion = new Label();
            gbWatermark = new GroupBox();
            label1 = new Label();
            cmbWatermark = new ComboBox();
            cbWatermark = new CheckBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            gbWatermark.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(btnInputBrowse);
            groupBox1.Controls.Add(txtInputFile);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(10, 12);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(542, 109);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Vstupný súbor";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(23, 26);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(219, 21);
            label4.TabIndex = 11;
            label4.Text = "Vybrať súbor pre zašifrovanie:";
            // 
            // btnInputBrowse
            // 
            btnInputBrowse.Location = new Point(488, 54);
            btnInputBrowse.Margin = new Padding(2);
            btnInputBrowse.Name = "btnInputBrowse";
            btnInputBrowse.Size = new Size(42, 24);
            btnInputBrowse.TabIndex = 10;
            btnInputBrowse.Text = "...";
            btnInputBrowse.UseVisualStyleBackColor = true;
            btnInputBrowse.Click += btnInputBrowse_Click;
            // 
            // txtInputFile
            // 
            txtInputFile.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInputFile.Location = new Point(24, 52);
            txtInputFile.Margin = new Padding(2);
            txtInputFile.Name = "txtInputFile";
            txtInputFile.Size = new Size(448, 27);
            txtInputFile.TabIndex = 8;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(btnOutputBrowse);
            groupBox2.Controls.Add(txtOutputFile);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(10, 127);
            groupBox2.Margin = new Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2);
            groupBox2.Size = new Size(542, 109);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Výstupný súbor";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(23, 28);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(255, 21);
            label2.TabIndex = 11;
            label2.Text = "Vybrať cestu pre zašifrovaný súbor:";
            // 
            // btnOutputBrowse
            // 
            btnOutputBrowse.Location = new Point(488, 56);
            btnOutputBrowse.Margin = new Padding(2);
            btnOutputBrowse.Name = "btnOutputBrowse";
            btnOutputBrowse.Size = new Size(42, 24);
            btnOutputBrowse.TabIndex = 10;
            btnOutputBrowse.Text = "...";
            btnOutputBrowse.UseVisualStyleBackColor = true;
            btnOutputBrowse.Click += btnOutputBrowse_Click;
            // 
            // txtOutputFile
            // 
            txtOutputFile.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOutputFile.Location = new Point(24, 54);
            txtOutputFile.Margin = new Padding(2);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(448, 27);
            txtOutputFile.TabIndex = 8;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnChangePassword);
            groupBox3.Controls.Add(lblOwnerPasswordSet);
            groupBox3.Controls.Add(lblCopied);
            groupBox3.Controls.Add(btnCopy);
            groupBox3.Controls.Add(labelPassword);
            groupBox3.Controls.Add(btnPasswordGenerate);
            groupBox3.Controls.Add(txtPassword);
            groupBox3.Controls.Add(lblPasswordLength);
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(12, 247);
            groupBox3.Margin = new Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2);
            groupBox3.Size = new Size(541, 147);
            groupBox3.TabIndex = 13;
            groupBox3.TabStop = false;
            groupBox3.Text = "Heslá";
            // 
            // btnChangePassword
            // 
            btnChangePassword.Image = (Image)resources.GetObject("btnChangePassword.Image");
            btnChangePassword.ImageAlign = ContentAlignment.MiddleLeft;
            btnChangePassword.Location = new Point(21, 102);
            btnChangePassword.Margin = new Padding(2);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(71, 32);
            btnChangePassword.TabIndex = 17;
            btnChangePassword.Text = "Zmeniť";
            btnChangePassword.TextAlign = ContentAlignment.MiddleRight;
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // lblOwnerPasswordSet
            // 
            lblOwnerPasswordSet.AutoSize = true;
            lblOwnerPasswordSet.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOwnerPasswordSet.ForeColor = Color.FromArgb(0, 192, 192);
            lblOwnerPasswordSet.Location = new Point(97, 111);
            lblOwnerPasswordSet.Name = "lblOwnerPasswordSet";
            lblOwnerPasswordSet.Size = new Size(153, 15);
            lblOwnerPasswordSet.TabIndex = 16;
            lblOwnerPasswordSet.Text = "Heslo vlastníka nastavené.";
            lblOwnerPasswordSet.Visible = false;
            // 
            // lblCopied
            // 
            lblCopied.AutoSize = true;
            lblCopied.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblCopied.ForeColor = Color.Green;
            lblCopied.Location = new Point(24, 86);
            lblCopied.Margin = new Padding(2, 0, 2, 0);
            lblCopied.Name = "lblCopied";
            lblCopied.Size = new Size(140, 13);
            lblCopied.TabIndex = 13;
            lblCopied.Text = "Skopírované do schránky.";
            lblCopied.Visible = false;
            // 
            // btnCopy
            // 
            btnCopy.Image = (Image)resources.GetObject("btnCopy.Image");
            btnCopy.ImageAlign = ContentAlignment.MiddleLeft;
            btnCopy.Location = new Point(458, 52);
            btnCopy.Margin = new Padding(2);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(78, 32);
            btnCopy.TabIndex = 12;
            btnCopy.Text = "Skopíruj";
            btnCopy.TextAlign = ContentAlignment.MiddleRight;
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPassword.Location = new Point(21, 27);
            labelPassword.Margin = new Padding(2, 0, 2, 0);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(236, 21);
            labelPassword.TabIndex = 11;
            labelPassword.Text = "Heslo pre uzamknutie čítania 📖";
            // 
            // btnPasswordGenerate
            // 
            btnPasswordGenerate.Image = (Image)resources.GetObject("btnPasswordGenerate.Image");
            btnPasswordGenerate.ImageAlign = ContentAlignment.MiddleLeft;
            btnPasswordGenerate.Location = new Point(380, 52);
            btnPasswordGenerate.Margin = new Padding(2);
            btnPasswordGenerate.Name = "btnPasswordGenerate";
            btnPasswordGenerate.Size = new Size(74, 32);
            btnPasswordGenerate.TabIndex = 10;
            btnPasswordGenerate.Text = "Generuj";
            btnPasswordGenerate.TextAlign = ContentAlignment.MiddleRight;
            btnPasswordGenerate.UseVisualStyleBackColor = true;
            btnPasswordGenerate.Click += btnPasswordGenerate_Click;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(24, 52);
            txtPassword.Margin = new Padding(2);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "(zadať heslo)";
            txtPassword.Size = new Size(350, 29);
            txtPassword.TabIndex = 8;
            txtPassword.TextChanged += txtPassword_TextChanged;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // lblPasswordLength
            // 
            lblPasswordLength.AutoSize = true;
            lblPasswordLength.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPasswordLength.ForeColor = Color.FromArgb(255, 128, 0);
            lblPasswordLength.Location = new Point(24, 86);
            lblPasswordLength.Margin = new Padding(2, 0, 2, 0);
            lblPasswordLength.Name = "lblPasswordLength";
            lblPasswordLength.Size = new Size(350, 15);
            lblPasswordLength.TabIndex = 14;
            lblPasswordLength.Text = "Heslá dlhšie ako 32 znakov budú skratené podľa špecifikácie PDF.";
            lblPasswordLength.Visible = false;
            // 
            // btnDecrypt
            // 
            btnDecrypt.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDecrypt.Image = (Image)resources.GetObject("btnDecrypt.Image");
            btnDecrypt.ImageAlign = ContentAlignment.MiddleLeft;
            btnDecrypt.Location = new Point(429, 472);
            btnDecrypt.Margin = new Padding(2);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(111, 37);
            btnDecrypt.TabIndex = 17;
            btnDecrypt.Text = "Odhesluj";
            btnDecrypt.TextAlign = ContentAlignment.MiddleRight;
            btnDecrypt.UseVisualStyleBackColor = true;
            btnDecrypt.Click += BtnDecryptClick;
            // 
            // btnEncrypt
            // 
            btnEncrypt.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEncrypt.Image = (Image)resources.GetObject("btnEncrypt.Image");
            btnEncrypt.ImageAlign = ContentAlignment.MiddleLeft;
            btnEncrypt.Location = new Point(429, 473);
            btnEncrypt.Margin = new Padding(2);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(119, 37);
            btnEncrypt.TabIndex = 14;
            btnEncrypt.Text = "Zahesluj  ";
            btnEncrypt.TextAlign = ContentAlignment.MiddleRight;
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += BtnEncryptClick;
            // 
            // btnClose
            // 
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.ImageAlign = ContentAlignment.MiddleLeft;
            btnClose.Location = new Point(330, 473);
            btnClose.Margin = new Padding(2);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(95, 37);
            btnClose.TabIndex = 15;
            btnClose.Text = "Zatvoriť";
            btnClose.TextAlign = ContentAlignment.MiddleRight;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // dlgOpen
            // 
            dlgOpen.Filter = "PDF súbory|*.pdf|Všetky súbory|*.*";
            // 
            // dlgSave
            // 
            dlgSave.Filter = "PDF súbory|*.pdf|Všetky súbory|*.*";
            dlgSave.OverwritePrompt = false;
            // 
            // btnSettings
            // 
            btnSettings.AutoEllipsis = true;
            btnSettings.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSettings.Image = (Image)resources.GetObject("btnSettings.Image");
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(13, 473);
            btnSettings.Margin = new Padding(2);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(118, 37);
            btnSettings.TabIndex = 16;
            btnSettings.Text = "Nastavenia";
            btnSettings.TextAlign = ContentAlignment.MiddleRight;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVersion.Location = new Point(463, 2);
            lblVersion.Margin = new Padding(2, 0, 2, 0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(51, 15);
            lblVersion.TabIndex = 18;
            lblVersion.Text = "Verzia: []";
            // 
            // gbWatermark
            // 
            gbWatermark.Controls.Add(label1);
            gbWatermark.Controls.Add(cmbWatermark);
            gbWatermark.Controls.Add(cbWatermark);
            gbWatermark.Location = new Point(10, 411);
            gbWatermark.Name = "gbWatermark";
            gbWatermark.Size = new Size(542, 51);
            gbWatermark.TabIndex = 19;
            gbWatermark.TabStop = false;
            gbWatermark.Text = "Vodotlač";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(218, 23);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 2;
            label1.Text = "Text:";
            // 
            // cmbWatermark
            // 
            cmbWatermark.Enabled = false;
            cmbWatermark.FormattingEnabled = true;
            cmbWatermark.Items.AddRange(new object[] { "Vzor", "Kópia", "Dôverné", "Návrh" });
            cmbWatermark.Location = new Point(255, 19);
            cmbWatermark.Name = "cmbWatermark";
            cmbWatermark.Size = new Size(275, 23);
            cmbWatermark.TabIndex = 1;
            // 
            // cbWatermark
            // 
            cbWatermark.AutoSize = true;
            cbWatermark.Location = new Point(24, 22);
            cbWatermark.Name = "cbWatermark";
            cbWatermark.Size = new Size(126, 19);
            cbWatermark.TabIndex = 0;
            cbWatermark.Text = "Použiťť vodotlač";
            cbWatermark.UseVisualStyleBackColor = true;
            cbWatermark.CheckedChanged += cbWatermark_CheckedChanged;
            // 
            // FrmMain
            // 
            AcceptButton = btnEncrypt;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            CancelButton = btnClose;
            ClientSize = new Size(561, 517);
            Controls.Add(gbWatermark);
            Controls.Add(lblVersion);
            Controls.Add(btnSettings);
            Controls.Add(btnClose);
            Controls.Add(btnEncrypt);
            Controls.Add(btnDecrypt);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMain";
            Text = "PDFPass -  Nástroj  s otvoreným kódom pre správu PDF hesiel";
            Load += frmMain_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            gbWatermark.ResumeLayout(false);
            gbWatermark.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnInputBrowse;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOutputBrowse;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.Button btnPasswordGenerate;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.OpenFileDialog dlgOpen;
		private System.Windows.Forms.SaveFileDialog dlgSave;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label lblCopied;
		private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Label lblPasswordLength;
        public System.Windows.Forms.TextBox txtInputFile;
        public System.Windows.Forms.TextBox txtOutputFile;
        public System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.Button btnEncrypt;
        public System.Windows.Forms.Label lblOwnerPasswordSet;
        public Button btnDecrypt;
        private Label lblVersion;
        private Button btnChangePassword;
        private GroupBox gbWatermark;
        private Label label1;
        private ComboBox cmbWatermark;
        private CheckBox cbWatermark;
    }
}

