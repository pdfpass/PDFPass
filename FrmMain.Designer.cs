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
	        components = new System.ComponentModel.Container();
	        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
	        groupBox1 = new System.Windows.Forms.GroupBox();
	        label4 = new System.Windows.Forms.Label();
	        btnInputBrowse = new System.Windows.Forms.Button();
	        txtInputFile = new System.Windows.Forms.TextBox();
	        groupBox2 = new System.Windows.Forms.GroupBox();
	        label2 = new System.Windows.Forms.Label();
	        btnOutputBrowse = new System.Windows.Forms.Button();
	        txtOutputFile = new System.Windows.Forms.TextBox();
	        groupBox3 = new System.Windows.Forms.GroupBox();
	        btnChangePassword = new System.Windows.Forms.Button();
	        lblOwnerPasswordSet = new System.Windows.Forms.Label();
	        lblCopied = new System.Windows.Forms.Label();
	        labelPassword = new System.Windows.Forms.Label();
	        btnPasswordGenerate = new System.Windows.Forms.Button();
	        txtPassword = new System.Windows.Forms.TextBox();
	        lblPasswordLength = new System.Windows.Forms.Label();
	        btnPaste = new System.Windows.Forms.Button();
	        btnCopy = new System.Windows.Forms.Button();
	        btnDecrypt = new System.Windows.Forms.Button();
	        btnEncrypt = new System.Windows.Forms.Button();
	        btnClose = new System.Windows.Forms.Button();
	        dlgOpen = new System.Windows.Forms.OpenFileDialog();
	        dlgSave = new System.Windows.Forms.SaveFileDialog();
	        btnSettings = new System.Windows.Forms.Button();
	        lblVersion = new System.Windows.Forms.Label();
	        gbWatermark = new System.Windows.Forms.GroupBox();
	        label1 = new System.Windows.Forms.Label();
	        cmbWatermark = new System.Windows.Forms.ComboBox();
	        cbWatermark = new System.Windows.Forms.CheckBox();
	        btnPasteTooltip = new System.Windows.Forms.ToolTip(components);
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
	        groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        groupBox1.Location = new System.Drawing.Point(10, 12);
	        groupBox1.Margin = new System.Windows.Forms.Padding(2);
	        groupBox1.Name = "groupBox1";
	        groupBox1.Padding = new System.Windows.Forms.Padding(2);
	        groupBox1.Size = new System.Drawing.Size(542, 109);
	        groupBox1.TabIndex = 8;
	        groupBox1.TabStop = false;
	        groupBox1.Text = "Vstupný súbor";
	        // 
	        // label4
	        // 
	        label4.AutoSize = true;
	        label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        label4.Location = new System.Drawing.Point(23, 26);
	        label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        label4.Name = "label4";
	        label4.Size = new System.Drawing.Size(219, 21);
	        label4.TabIndex = 11;
	        label4.Text = "Vybrať súbor pre zašifrovanie:";
	        // 
	        // btnInputBrowse
	        // 
	        btnInputBrowse.Image = ((System.Drawing.Image)resources.GetObject("btnInputBrowse.Image"));
	        btnInputBrowse.Location = new System.Drawing.Point(485, 47);
	        btnInputBrowse.Margin = new System.Windows.Forms.Padding(2);
	        btnInputBrowse.Name = "btnInputBrowse";
	        btnInputBrowse.Size = new System.Drawing.Size(38, 38);
	        btnInputBrowse.TabIndex = 10;
	        btnInputBrowse.UseVisualStyleBackColor = true;
	        btnInputBrowse.Click += btnInputBrowse_Click;
	        // 
	        // txtInputFile
	        // 
	        txtInputFile.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        txtInputFile.Location = new System.Drawing.Point(24, 52);
	        txtInputFile.Margin = new System.Windows.Forms.Padding(2);
	        txtInputFile.Name = "txtInputFile";
	        txtInputFile.Size = new System.Drawing.Size(448, 27);
	        txtInputFile.TabIndex = 8;
	        // 
	        // groupBox2
	        // 
	        groupBox2.Controls.Add(label2);
	        groupBox2.Controls.Add(btnOutputBrowse);
	        groupBox2.Controls.Add(txtOutputFile);
	        groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        groupBox2.Location = new System.Drawing.Point(10, 127);
	        groupBox2.Margin = new System.Windows.Forms.Padding(2);
	        groupBox2.Name = "groupBox2";
	        groupBox2.Padding = new System.Windows.Forms.Padding(2);
	        groupBox2.Size = new System.Drawing.Size(542, 109);
	        groupBox2.TabIndex = 12;
	        groupBox2.TabStop = false;
	        groupBox2.Text = "Výstupný súbor";
	        // 
	        // label2
	        // 
	        label2.AutoSize = true;
	        label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        label2.Location = new System.Drawing.Point(23, 28);
	        label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        label2.Name = "label2";
	        label2.Size = new System.Drawing.Size(255, 21);
	        label2.TabIndex = 11;
	        label2.Text = "Vybrať cestu pre zašifrovaný súbor:";
	        // 
	        // btnOutputBrowse
	        // 
	        btnOutputBrowse.Image = ((System.Drawing.Image)resources.GetObject("btnOutputBrowse.Image"));
	        btnOutputBrowse.Location = new System.Drawing.Point(485, 48);
	        btnOutputBrowse.Margin = new System.Windows.Forms.Padding(2);
	        btnOutputBrowse.Name = "btnOutputBrowse";
	        btnOutputBrowse.Size = new System.Drawing.Size(38, 38);
	        btnOutputBrowse.TabIndex = 10;
	        btnOutputBrowse.UseVisualStyleBackColor = true;
	        btnOutputBrowse.Click += btnOutputBrowse_Click;
	        // 
	        // txtOutputFile
	        // 
	        txtOutputFile.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        txtOutputFile.Location = new System.Drawing.Point(24, 54);
	        txtOutputFile.Margin = new System.Windows.Forms.Padding(2);
	        txtOutputFile.Name = "txtOutputFile";
	        txtOutputFile.Size = new System.Drawing.Size(448, 27);
	        txtOutputFile.TabIndex = 8;
	        // 
	        // groupBox3
	        // 
	        groupBox3.Controls.Add(btnChangePassword);
	        groupBox3.Controls.Add(lblOwnerPasswordSet);
	        groupBox3.Controls.Add(lblCopied);
	        groupBox3.Controls.Add(labelPassword);
	        groupBox3.Controls.Add(btnPasswordGenerate);
	        groupBox3.Controls.Add(txtPassword);
	        groupBox3.Controls.Add(lblPasswordLength);
	        groupBox3.Controls.Add(btnPaste);
	        groupBox3.Controls.Add(btnCopy);
	        groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        groupBox3.Location = new System.Drawing.Point(12, 247);
	        groupBox3.Margin = new System.Windows.Forms.Padding(2);
	        groupBox3.Name = "groupBox3";
	        groupBox3.Padding = new System.Windows.Forms.Padding(2);
	        groupBox3.Size = new System.Drawing.Size(541, 147);
	        groupBox3.TabIndex = 13;
	        groupBox3.TabStop = false;
	        groupBox3.Text = "Heslá";
	        // 
	        // btnChangePassword
	        // 
	        btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        btnChangePassword.Image = ((System.Drawing.Image)resources.GetObject("btnChangePassword.Image"));
	        btnChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        btnChangePassword.Location = new System.Drawing.Point(21, 102);
	        btnChangePassword.Margin = new System.Windows.Forms.Padding(2);
	        btnChangePassword.Name = "btnChangePassword";
	        btnChangePassword.Size = new System.Drawing.Size(79, 32);
	        btnChangePassword.TabIndex = 17;
	        btnChangePassword.Text = "Zmeniť";
	        btnChangePassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
	        btnChangePassword.UseVisualStyleBackColor = true;
	        btnChangePassword.Click += btnChangePassword_Click;
	        // 
	        // lblOwnerPasswordSet
	        // 
	        lblOwnerPasswordSet.AutoSize = true;
	        lblOwnerPasswordSet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        lblOwnerPasswordSet.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)0)), ((int)((byte)192)), ((int)((byte)192)));
	        lblOwnerPasswordSet.Location = new System.Drawing.Point(112, 112);
	        lblOwnerPasswordSet.Name = "lblOwnerPasswordSet";
	        lblOwnerPasswordSet.Size = new System.Drawing.Size(153, 15);
	        lblOwnerPasswordSet.TabIndex = 16;
	        lblOwnerPasswordSet.Text = "Heslo vlastníka nastavené.";
	        lblOwnerPasswordSet.Visible = false;
	        // 
	        // lblCopied
	        // 
	        lblCopied.AutoSize = true;
	        lblCopied.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
	        lblCopied.ForeColor = System.Drawing.Color.Green;
	        lblCopied.Location = new System.Drawing.Point(24, 86);
	        lblCopied.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        lblCopied.Name = "lblCopied";
	        lblCopied.Size = new System.Drawing.Size(140, 13);
	        lblCopied.TabIndex = 13;
	        lblCopied.Text = "Skopírované do schránky.";
	        lblCopied.Visible = false;
	        // 
	        // labelPassword
	        // 
	        labelPassword.AutoSize = true;
	        labelPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        labelPassword.Location = new System.Drawing.Point(21, 27);
	        labelPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        labelPassword.Name = "labelPassword";
	        labelPassword.Size = new System.Drawing.Size(236, 21);
	        labelPassword.TabIndex = 11;
	        labelPassword.Text = "Heslo pre uzamknutie čítania 🔒";
	        // 
	        // btnPasswordGenerate
	        // 
	        btnPasswordGenerate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        btnPasswordGenerate.Image = ((System.Drawing.Image)resources.GetObject("btnPasswordGenerate.Image"));
	        btnPasswordGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        btnPasswordGenerate.Location = new System.Drawing.Point(344, 52);
	        btnPasswordGenerate.Margin = new System.Windows.Forms.Padding(2);
	        btnPasswordGenerate.Name = "btnPasswordGenerate";
	        btnPasswordGenerate.Size = new System.Drawing.Size(92, 32);
	        btnPasswordGenerate.TabIndex = 10;
	        btnPasswordGenerate.Text = "Generuj";
	        btnPasswordGenerate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
	        btnPasswordGenerate.UseVisualStyleBackColor = true;
	        btnPasswordGenerate.Click += btnPasswordGenerate_Click;
	        // 
	        // txtPassword
	        // 
	        txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        txtPassword.Location = new System.Drawing.Point(24, 52);
	        txtPassword.Margin = new System.Windows.Forms.Padding(2);
	        txtPassword.Name = "txtPassword";
	        txtPassword.PlaceholderText = "(zadať heslo)";
	        txtPassword.Size = new System.Drawing.Size(316, 29);
	        txtPassword.TabIndex = 8;
	        txtPassword.TextChanged += txtPassword_TextChanged;
	        txtPassword.KeyDown += txtPassword_KeyDown;
	        // 
	        // lblPasswordLength
	        // 
	        lblPasswordLength.AutoSize = true;
	        lblPasswordLength.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        lblPasswordLength.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)255)), ((int)((byte)128)), ((int)((byte)0)));
	        lblPasswordLength.Location = new System.Drawing.Point(24, 86);
	        lblPasswordLength.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        lblPasswordLength.Name = "lblPasswordLength";
	        lblPasswordLength.Size = new System.Drawing.Size(350, 15);
	        lblPasswordLength.TabIndex = 14;
	        lblPasswordLength.Text = "Heslá dlhšie ako 32 znakov budú skratené podľa špecifikácie PDF.";
	        lblPasswordLength.Visible = false;
	        // 
	        // btnPaste
	        // 
	        btnPaste.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        btnPaste.Image = ((System.Drawing.Image)resources.GetObject("btnPaste.Image"));
	        btnPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        btnPaste.Location = new System.Drawing.Point(440, 52);
	        btnPaste.Margin = new System.Windows.Forms.Padding(2);
	        btnPaste.Name = "btnPaste";
	        btnPaste.Size = new System.Drawing.Size(81, 32);
	        btnPaste.TabIndex = 18;
	        btnPaste.Text = "Prilepiť";
	        btnPaste.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
	        btnPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
	        btnPaste.UseVisualStyleBackColor = true;
	        btnPaste.Click += btnPaste_Click;
	        btnPaste.MouseHover += btnPaste_MouseHover;
	        // 
	        // btnCopy
	        // 
	        btnCopy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        btnCopy.Image = ((System.Drawing.Image)resources.GetObject("btnCopy.Image"));
	        btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        btnCopy.Location = new System.Drawing.Point(440, 52);
	        btnCopy.Margin = new System.Windows.Forms.Padding(2);
	        btnCopy.Name = "btnCopy";
	        btnCopy.Size = new System.Drawing.Size(91, 32);
	        btnCopy.TabIndex = 12;
	        btnCopy.Text = "Kopírovať";
	        btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
	        btnCopy.UseVisualStyleBackColor = true;
	        btnCopy.Click += btnCopy_Click;
	        // 
	        // btnDecrypt
	        // 
	        btnDecrypt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        btnDecrypt.Image = ((System.Drawing.Image)resources.GetObject("btnDecrypt.Image"));
	        btnDecrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        btnDecrypt.Location = new System.Drawing.Point(429, 473);
	        btnDecrypt.Margin = new System.Windows.Forms.Padding(2);
	        btnDecrypt.Name = "btnDecrypt";
	        btnDecrypt.Size = new System.Drawing.Size(118, 37);
	        btnDecrypt.TabIndex = 17;
	        btnDecrypt.Text = "Odhesluj";
	        btnDecrypt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
	        btnDecrypt.UseVisualStyleBackColor = true;
	        btnDecrypt.Click += BtnDecryptClick;
	        // 
	        // btnEncrypt
	        // 
	        btnEncrypt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        btnEncrypt.Image = ((System.Drawing.Image)resources.GetObject("btnEncrypt.Image"));
	        btnEncrypt.Location = new System.Drawing.Point(429, 473);
	        btnEncrypt.Margin = new System.Windows.Forms.Padding(2);
	        btnEncrypt.Name = "btnEncrypt";
	        btnEncrypt.Size = new System.Drawing.Size(118, 37);
	        btnEncrypt.TabIndex = 14;
	        btnEncrypt.Text = "Zahesluj";
	        btnEncrypt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
	        btnEncrypt.UseVisualStyleBackColor = true;
	        btnEncrypt.Click += BtnEncryptClick;
	        // 
	        // btnClose
	        // 
	        btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
	        btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        btnClose.Image = ((System.Drawing.Image)resources.GetObject("btnClose.Image"));
	        btnClose.Location = new System.Drawing.Point(322, 473);
	        btnClose.Margin = new System.Windows.Forms.Padding(2);
	        btnClose.Name = "btnClose";
	        btnClose.Size = new System.Drawing.Size(103, 37);
	        btnClose.TabIndex = 15;
	        btnClose.Text = "Zatvoriť";
	        btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
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
	        btnSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        btnSettings.Image = ((System.Drawing.Image)resources.GetObject("btnSettings.Image"));
	        btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
	        btnSettings.Location = new System.Drawing.Point(12, 473);
	        btnSettings.Margin = new System.Windows.Forms.Padding(2);
	        btnSettings.Name = "btnSettings";
	        btnSettings.Size = new System.Drawing.Size(124, 37);
	        btnSettings.TabIndex = 16;
	        btnSettings.Text = "Nastavenia";
	        btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
	        btnSettings.UseVisualStyleBackColor = true;
	        btnSettings.Click += btnSettings_Click;
	        // 
	        // lblVersion
	        // 
	        lblVersion.AutoSize = true;
	        lblVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        lblVersion.Location = new System.Drawing.Point(463, 2);
	        lblVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
	        lblVersion.Name = "lblVersion";
	        lblVersion.Size = new System.Drawing.Size(51, 15);
	        lblVersion.TabIndex = 18;
	        lblVersion.Text = "Verzia: []";
	        // 
	        // gbWatermark
	        // 
	        gbWatermark.Controls.Add(label1);
	        gbWatermark.Controls.Add(cmbWatermark);
	        gbWatermark.Controls.Add(cbWatermark);
	        gbWatermark.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        gbWatermark.Location = new System.Drawing.Point(10, 411);
	        gbWatermark.Name = "gbWatermark";
	        gbWatermark.Size = new System.Drawing.Size(542, 54);
	        gbWatermark.TabIndex = 19;
	        gbWatermark.TabStop = false;
	        gbWatermark.Text = "Vodotlač";
	        // 
	        // label1
	        // 
	        label1.AutoSize = true;
	        label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        label1.Location = new System.Drawing.Point(235, 21);
	        label1.Name = "label1";
	        label1.Size = new System.Drawing.Size(39, 21);
	        label1.TabIndex = 2;
	        label1.Text = "Text:";
	        // 
	        // cmbWatermark
	        // 
	        cmbWatermark.Enabled = false;
	        cmbWatermark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
	        cmbWatermark.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        cmbWatermark.FormattingEnabled = true;
	        cmbWatermark.Items.AddRange(new object[] { "Vzor", "Kópia", "Dôverné", "Návrh" });
	        cmbWatermark.Location = new System.Drawing.Point(280, 17);
	        cmbWatermark.Name = "cmbWatermark";
	        cmbWatermark.Size = new System.Drawing.Size(250, 29);
	        cmbWatermark.TabIndex = 1;
	        // 
	        // cbWatermark
	        // 
	        cbWatermark.AutoSize = true;
	        cbWatermark.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        cbWatermark.Location = new System.Drawing.Point(24, 21);
	        cbWatermark.Name = "cbWatermark";
	        cbWatermark.Size = new System.Drawing.Size(162, 25);
	        cbWatermark.TabIndex = 0;
	        cbWatermark.Text = "Použiť vodotlač 💧";
	        cbWatermark.UseVisualStyleBackColor = true;
	        cbWatermark.CheckedChanged += cbWatermark_CheckedChanged;
	        // 
	        // FrmMain
	        // 
	        AcceptButton = btnEncrypt;
	        AllowDrop = true;
	        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
	        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	        BackColor = System.Drawing.SystemColors.Control;
	        CancelButton = btnClose;
	        ClientSize = new System.Drawing.Size(561, 521);
	        Controls.Add(gbWatermark);
	        Controls.Add(lblVersion);
	        Controls.Add(btnSettings);
	        Controls.Add(btnClose);
	        Controls.Add(btnEncrypt);
	        Controls.Add(btnDecrypt);
	        Controls.Add(groupBox3);
	        Controls.Add(groupBox2);
	        Controls.Add(groupBox1);
	        Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
	        ForeColor = System.Drawing.SystemColors.ControlText;
	        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
	        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
	        Margin = new System.Windows.Forms.Padding(2);
	        MaximizeBox = false;
	        MinimizeBox = false;
	        Text = "PDFPass -  Nástroj  s otvoreným kódom pre správu PDF hesiel";
	        Load += frmMain_Load;
	        DragDrop += FrmMain_DragDrop;
	        DragEnter += FrmMain_DragEnter;
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
        private System.Windows.Forms.Button btnEncrypt;
        public System.Windows.Forms.Label lblOwnerPasswordSet;
        private System.Windows.Forms.Button btnDecrypt;
        private Label lblVersion;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.GroupBox gbWatermark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbWatermark;
        private System.Windows.Forms.CheckBox cbWatermark;
        private System.Windows.Forms.Button btnPaste;
        private ToolTip btnPasteTooltip;
    }
}

