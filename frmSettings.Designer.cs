namespace PDFPass
{
	partial class frmSettings
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
            lblVersion = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            dlgOpen = new System.Windows.Forms.OpenFileDialog();
            btnOK = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            lblVisitSite = new System.Windows.Forms.LinkLabel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            txtArguments = new System.Windows.Forms.TextBox();
            chkOpen = new System.Windows.Forms.CheckBox();
            chkShowFolder = new System.Windows.Forms.CheckBox();
            chkCloseAfterCompletion = new System.Windows.Forms.CheckBox();
            btnRunBrowse = new System.Windows.Forms.Button();
            txtRun = new System.Windows.Forms.TextBox();
            chkRun = new System.Windows.Forms.CheckBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            txtOwnerPassword = new System.Windows.Forms.TextBox();
            lblOwnerPassword = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            chkDegradedPrinting = new System.Windows.Forms.CheckBox();
            chkAssembly = new System.Windows.Forms.CheckBox();
            chkScreenreaders = new System.Windows.Forms.CheckBox();
            chkForms = new System.Windows.Forms.CheckBox();
            chkNotations = new System.Windows.Forms.CheckBox();
            chkModifying = new System.Windows.Forms.CheckBox();
            chkCopying = new System.Windows.Forms.CheckBox();
            label2 = new System.Windows.Forms.Label();
            chkPrinting = new System.Windows.Forms.CheckBox();
            chkEncryptMetadata = new System.Windows.Forms.CheckBox();
            label3 = new System.Windows.Forms.Label();
            cboEncryptionType = new System.Windows.Forms.ComboBox();
            chkPasswordConfirmation = new System.Windows.Forms.CheckBox();
            linkDonate = new System.Windows.Forms.LinkLabel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblVersion.Location = new System.Drawing.Point(10, 485);
            lblVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new System.Drawing.Size(51, 15);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "Verzia: []";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(10, 502);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(228, 15);
            label1.TabIndex = 1;
            label1.Text = "Copyright 2024. Licencované podľa AGPL.";
            // 
            // dlgOpen
            // 
            dlgOpen.Filter = "Spustiteľné súbory (*.exe, *.bat, *.com)|*.exe;*.bat;*.com|Všetky súbory (*.*)|*.*";
            // 
            // btnOK
            // 
            btnOK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnOK.Location = new System.Drawing.Point(421, 13);
            btnOK.Margin = new System.Windows.Forms.Padding(2);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(85, 37);
            btnOK.TabIndex = 8;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnCancel.Location = new System.Drawing.Point(421, 54);
            btnCancel.Margin = new System.Windows.Forms.Padding(2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(85, 37);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Storno";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblVisitSite
            // 
            lblVisitSite.AutoSize = true;
            lblVisitSite.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblVisitSite.Location = new System.Drawing.Point(237, 501);
            lblVisitSite.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblVisitSite.Name = "lblVisitSite";
            lblVisitSite.Size = new System.Drawing.Size(77, 15);
            lblVisitSite.TabIndex = 14;
            lblVisitSite.TabStop = true;
            lblVisitSite.Text = "Zdrojový kód";
            lblVisitSite.LinkClicked += lblVisitSite_LinkClicked;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtArguments);
            groupBox1.Controls.Add(chkOpen);
            groupBox1.Controls.Add(chkShowFolder);
            groupBox1.Controls.Add(chkCloseAfterCompletion);
            groupBox1.Controls.Add(btnRunBrowse);
            groupBox1.Controls.Add(txtRun);
            groupBox1.Controls.Add(chkRun);
            groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(13, 313);
            groupBox1.Margin = new System.Windows.Forms.Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(2);
            groupBox1.Size = new System.Drawing.Size(457, 170);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = " Po úspešnom zašifrovaní ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            label6.Location = new System.Drawing.Point(49, 144);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(334, 15);
            label6.TabIndex = 27;
            label6.Text = "(Cesta výstupneho súboru je pridaná ako posledný parameter)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(69, 110);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(64, 15);
            label5.TabIndex = 21;
            label5.Text = "Parametre:";
            // 
            // txtArguments
            // 
            txtArguments.Location = new System.Drawing.Point(137, 107);
            txtArguments.Margin = new System.Windows.Forms.Padding(2);
            txtArguments.Name = "txtArguments";
            txtArguments.PlaceholderText = "(parametre programu)";
            txtArguments.Size = new System.Drawing.Size(253, 23);
            txtArguments.TabIndex = 20;
            // 
            // chkOpen
            // 
            chkOpen.AutoSize = true;
            chkOpen.Location = new System.Drawing.Point(238, 28);
            chkOpen.Margin = new System.Windows.Forms.Padding(2);
            chkOpen.Name = "chkOpen";
            chkOpen.Size = new System.Drawing.Size(148, 19);
            chkOpen.TabIndex = 19;
            chkOpen.Text = "Otvoriť výstupný súbor";
            chkOpen.UseVisualStyleBackColor = true;
            // 
            // chkShowFolder
            // 
            chkShowFolder.AutoSize = true;
            chkShowFolder.Location = new System.Drawing.Point(18, 28);
            chkShowFolder.Margin = new System.Windows.Forms.Padding(2);
            chkShowFolder.Name = "chkShowFolder";
            chkShowFolder.Size = new System.Drawing.Size(185, 19);
            chkShowFolder.TabIndex = 18;
            chkShowFolder.Text = "Otvoriť súbor v Priekumníkovi";
            chkShowFolder.UseVisualStyleBackColor = true;
            // 
            // chkCloseAfterCompletion
            // 
            chkCloseAfterCompletion.AutoSize = true;
            chkCloseAfterCompletion.Location = new System.Drawing.Point(18, 53);
            chkCloseAfterCompletion.Margin = new System.Windows.Forms.Padding(2);
            chkCloseAfterCompletion.Name = "chkCloseAfterCompletion";
            chkCloseAfterCompletion.Size = new System.Drawing.Size(115, 19);
            chkCloseAfterCompletion.TabIndex = 17;
            chkCloseAfterCompletion.Text = "Zatvoriť PDFPass";
            chkCloseAfterCompletion.UseVisualStyleBackColor = true;
            // 
            // btnRunBrowse
            // 
            btnRunBrowse.Location = new System.Drawing.Point(398, 76);
            btnRunBrowse.Margin = new System.Windows.Forms.Padding(2);
            btnRunBrowse.Name = "btnRunBrowse";
            btnRunBrowse.Size = new System.Drawing.Size(43, 22);
            btnRunBrowse.TabIndex = 16;
            btnRunBrowse.Text = "...";
            btnRunBrowse.UseVisualStyleBackColor = true;
            btnRunBrowse.Click += btnRunBrowse_Click;
            // 
            // txtRun
            // 
            txtRun.Location = new System.Drawing.Point(137, 75);
            txtRun.Margin = new System.Windows.Forms.Padding(2);
            txtRun.Name = "txtRun";
            txtRun.PlaceholderText = "(cesta k programu)";
            txtRun.Size = new System.Drawing.Size(253, 23);
            txtRun.TabIndex = 15;
            // 
            // chkRun
            // 
            chkRun.AutoSize = true;
            chkRun.Location = new System.Drawing.Point(18, 78);
            chkRun.Margin = new System.Windows.Forms.Padding(2);
            chkRun.Name = "chkRun";
            chkRun.Size = new System.Drawing.Size(115, 19);
            chkRun.TabIndex = 14;
            chkRun.Text = "Spustiť program:";
            chkRun.UseVisualStyleBackColor = true;
            chkRun.CheckedChanged += chkRun_CheckedChanged_1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtOwnerPassword);
            groupBox2.Controls.Add(lblOwnerPassword);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(chkDegradedPrinting);
            groupBox2.Controls.Add(chkAssembly);
            groupBox2.Controls.Add(chkScreenreaders);
            groupBox2.Controls.Add(chkForms);
            groupBox2.Controls.Add(chkNotations);
            groupBox2.Controls.Add(chkModifying);
            groupBox2.Controls.Add(chkCopying);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(chkPrinting);
            groupBox2.Controls.Add(chkEncryptMetadata);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(cboEncryptionType);
            groupBox2.Controls.Add(chkPasswordConfirmation);
            groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.Location = new System.Drawing.Point(13, 12);
            groupBox2.Margin = new System.Windows.Forms.Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(2);
            groupBox2.Size = new System.Drawing.Size(394, 282);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = " Možnosti šifrovania ";
            // 
            // txtOwnerPassword
            // 
            txtOwnerPassword.Location = new System.Drawing.Point(96, 242);
            txtOwnerPassword.Margin = new System.Windows.Forms.Padding(2);
            txtOwnerPassword.MaxLength = 32;
            txtOwnerPassword.Name = "txtOwnerPassword";
            txtOwnerPassword.PlaceholderText = "(trvalé heslo vlastníka)";
            txtOwnerPassword.Size = new System.Drawing.Size(285, 23);
            txtOwnerPassword.TabIndex = 28;
            txtOwnerPassword.TextChanged += txtOwnerPassword_TextChanged;
            // 
            // lblOwnerPassword
            // 
            lblOwnerPassword.AutoSize = true;
            lblOwnerPassword.Location = new System.Drawing.Point(3, 246);
            lblOwnerPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblOwnerPassword.Name = "lblOwnerPassword";
            lblOwnerPassword.Size = new System.Drawing.Size(89, 15);
            lblOwnerPassword.TabIndex = 27;
            lblOwnerPassword.Text = "Heslo vlastníka:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            label4.Location = new System.Drawing.Point(27, 173);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(280, 15);
            label4.TabIndex = 26;
            label4.Text = "Tieto oprávnenia niektoré PDF prehliadače ignorujú.";
            // 
            // chkDegradedPrinting
            // 
            chkDegradedPrinting.AutoSize = true;
            chkDegradedPrinting.Location = new System.Drawing.Point(204, 73);
            chkDegradedPrinting.Margin = new System.Windows.Forms.Padding(2);
            chkDegradedPrinting.Name = "chkDegradedPrinting";
            chkDegradedPrinting.Size = new System.Drawing.Size(157, 19);
            chkDegradedPrinting.TabIndex = 25;
            chkDegradedPrinting.Text = "Povoliť tlač (nízke rozlíš.)";
            chkDegradedPrinting.UseVisualStyleBackColor = true;
            // 
            // chkAssembly
            // 
            chkAssembly.AutoSize = true;
            chkAssembly.Location = new System.Drawing.Point(12, 149);
            chkAssembly.Margin = new System.Windows.Forms.Padding(2);
            chkAssembly.Name = "chkAssembly";
            chkAssembly.Size = new System.Drawing.Size(165, 19);
            chkAssembly.TabIndex = 24;
            chkAssembly.Text = "Povoliť usporiadanie stránok";
            chkAssembly.UseVisualStyleBackColor = true;
            // 
            // chkScreenreaders
            // 
            chkScreenreaders.AutoSize = true;
            chkScreenreaders.Location = new System.Drawing.Point(204, 149);
            chkScreenreaders.Margin = new System.Windows.Forms.Padding(2);
            chkScreenreaders.Name = "chkScreenreaders";
            chkScreenreaders.Size = new System.Drawing.Size(162, 19);
            chkScreenreaders.TabIndex = 23;
            chkScreenreaders.Text = "Povoliť asistenčné technológie";
            chkScreenreaders.UseVisualStyleBackColor = true;
            // 
            // chkForms
            // 
            chkForms.AutoSize = true;
            chkForms.Location = new System.Drawing.Point(204, 123);
            chkForms.Margin = new System.Windows.Forms.Padding(2);
            chkForms.Name = "chkForms";
            chkForms.Size = new System.Drawing.Size(153, 19);
            chkForms.TabIndex = 22;
            chkForms.Text = "Povoliť vyplniť formulár";
            chkForms.UseVisualStyleBackColor = true;
            // 
            // chkNotations
            // 
            chkNotations.AutoSize = true;
            chkNotations.Location = new System.Drawing.Point(204, 98);
            chkNotations.Margin = new System.Windows.Forms.Padding(2);
            chkNotations.Name = "chkNotations";
            chkNotations.Size = new System.Drawing.Size(148, 19);
            chkNotations.TabIndex = 21;
            chkNotations.Text = "Povoliť anotácie";
            chkNotations.UseVisualStyleBackColor = true;
            // 
            // chkModifying
            // 
            chkModifying.AutoSize = true;
            chkModifying.Location = new System.Drawing.Point(12, 98);
            chkModifying.Margin = new System.Windows.Forms.Padding(2);
            chkModifying.Name = "chkModifying";
            chkModifying.Size = new System.Drawing.Size(130, 19);
            chkModifying.TabIndex = 20;
            chkModifying.Text = "Povoliť úpravy dokumentu";
            chkModifying.UseVisualStyleBackColor = true;
            // 
            // chkCopying
            // 
            chkCopying.AutoSize = true;
            chkCopying.Location = new System.Drawing.Point(12, 123);
            chkCopying.Margin = new System.Windows.Forms.Padding(2);
            chkCopying.Name = "chkCopying";
            chkCopying.Size = new System.Drawing.Size(129, 19);
            chkCopying.TabIndex = 19;
            chkCopying.Text = "Povoliť kopírovanie obsahu";
            chkCopying.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 54);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(172, 15);
            label2.TabIndex = 18;
            label2.Text = "Oprávnenia pre výstupný súbor";
            // 
            // chkPrinting
            // 
            chkPrinting.AutoSize = true;
            chkPrinting.Location = new System.Drawing.Point(12, 73);
            chkPrinting.Margin = new System.Windows.Forms.Padding(2);
            chkPrinting.Name = "chkPrinting";
            chkPrinting.Size = new System.Drawing.Size(166, 19);
            chkPrinting.TabIndex = 17;
            chkPrinting.Text = "Povoliť tlač (vysoké rozlíš.)";
            chkPrinting.UseVisualStyleBackColor = true;
            // 
            // chkEncryptMetadata
            // 
            chkEncryptMetadata.AutoSize = true;
            chkEncryptMetadata.Location = new System.Drawing.Point(203, 28);
            chkEncryptMetadata.Margin = new System.Windows.Forms.Padding(2);
            chkEncryptMetadata.Name = "chkEncryptMetadata";
            chkEncryptMetadata.Size = new System.Drawing.Size(132, 19);
            chkEncryptMetadata.TabIndex = 16;
            chkEncryptMetadata.Text = "Zašifrovať metadata";
            chkEncryptMetadata.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(23, 210);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(69, 15);
            label3.TabIndex = 15;
            label3.Text = "Algoritmus:";
            // 
            // cboEncryptionType
            // 
            cboEncryptionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboEncryptionType.FormattingEnabled = true;
            cboEncryptionType.Location = new System.Drawing.Point(96, 206);
            cboEncryptionType.Margin = new System.Windows.Forms.Padding(2);
            cboEncryptionType.Name = "cboEncryptionType";
            cboEncryptionType.Size = new System.Drawing.Size(285, 23);
            cboEncryptionType.TabIndex = 14;
            // 
            // chkPasswordConfirmation
            // 
            chkPasswordConfirmation.AutoSize = true;
            chkPasswordConfirmation.Location = new System.Drawing.Point(12, 28);
            chkPasswordConfirmation.Margin = new System.Windows.Forms.Padding(2);
            chkPasswordConfirmation.Name = "chkPasswordConfirmation";
            chkPasswordConfirmation.Size = new System.Drawing.Size(100, 19);
            chkPasswordConfirmation.TabIndex = 13;
            chkPasswordConfirmation.Text = "Potvrdiť heslo";
            chkPasswordConfirmation.UseVisualStyleBackColor = true;
            // 
            // linkDonate
            // 
            linkDonate.AutoSize = true;
            linkDonate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            linkDonate.LinkColor = System.Drawing.Color.MediumSlateBlue;
            linkDonate.Location = new System.Drawing.Point(83, 526);
            linkDonate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            linkDonate.Name = "linkDonate";
            linkDonate.Size = new System.Drawing.Size(358, 19);
            linkDonate.TabIndex = 17;
            linkDonate.TabStop = true;
            linkDonate.Text = "Autora môžete dobrovoľne podporiť malou sumou!";
            linkDonate.LinkClicked += linkDonate_LinkClicked;
            // 
            // frmSettings
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(512, 555);
            Controls.Add(linkDonate);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(lblVisitSite);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(label1);
            Controls.Add(lblVersion);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSettings";
            Text = "Nastavenia";
            Load += frmSettings_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog dlgOpen;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.LinkLabel lblVisitSite;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkShowFolder;
		private System.Windows.Forms.CheckBox chkCloseAfterCompletion;
		private System.Windows.Forms.Button btnRunBrowse;
		private System.Windows.Forms.TextBox txtRun;
		private System.Windows.Forms.CheckBox chkRun;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkPrinting;
		private System.Windows.Forms.CheckBox chkEncryptMetadata;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboEncryptionType;
		private System.Windows.Forms.CheckBox chkPasswordConfirmation;
		private System.Windows.Forms.CheckBox chkOpen;
		private System.Windows.Forms.CheckBox chkDegradedPrinting;
		private System.Windows.Forms.CheckBox chkAssembly;
		private System.Windows.Forms.CheckBox chkScreenreaders;
		private System.Windows.Forms.CheckBox chkForms;
		private System.Windows.Forms.CheckBox chkNotations;
		private System.Windows.Forms.CheckBox chkModifying;
		private System.Windows.Forms.CheckBox chkCopying;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkDonate;
        private System.Windows.Forms.TextBox txtOwnerPassword;
        private System.Windows.Forms.Label lblOwnerPassword;
    }
}