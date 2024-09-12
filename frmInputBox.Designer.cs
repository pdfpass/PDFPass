namespace PDFPass
{
	partial class FrmInputBox
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
            btnOK = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            txtInput = new System.Windows.Forms.TextBox();
            lblPrompt = new System.Windows.Forms.TextBox();
            btnClose = new System.Windows.Forms.Button();
            btnDefaultOwnerPassword = new System.Windows.Forms.Button();
            toolTipDefaultOwnerPassword = new System.Windows.Forms.ToolTip(components);
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(394, 7);
            btnOK.Margin = new System.Windows.Forms.Padding(2);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(68, 32);
            btnOK.TabIndex = 0;
            btnOK.Text = "Potvrdiť";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(394, 43);
            btnCancel.Margin = new System.Windows.Forms.Padding(2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(68, 32);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Stornovať";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtInput
            // 
            txtInput.Location = new System.Drawing.Point(13, 102);
            txtInput.Margin = new System.Windows.Forms.Padding(2);
            txtInput.Name = "txtInput";
            txtInput.PlaceholderText = "(zadať heslo)";
            txtInput.Size = new System.Drawing.Size(321, 23);
            txtInput.TabIndex = 3;
            // 
            // lblPrompt
            // 
            lblPrompt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lblPrompt.Location = new System.Drawing.Point(13, 12);
            lblPrompt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lblPrompt.Multiline = true;
            lblPrompt.Name = "lblPrompt";
            lblPrompt.ReadOnly = true;
            lblPrompt.Size = new System.Drawing.Size(364, 80);
            lblPrompt.TabIndex = 4;
            lblPrompt.Text = "prompt";
            // 
            // btnClose
            // 
            btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnClose.Location = new System.Drawing.Point(394, 96);
            btnClose.Margin = new System.Windows.Forms.Padding(2);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(68, 32);
            btnClose.TabIndex = 5;
            btnClose.Text = "Zatvoriť";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnDefaultOwnerPassword
            // 
            btnDefaultOwnerPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnDefaultOwnerPassword.Location = new System.Drawing.Point(340, 98);
            btnDefaultOwnerPassword.Name = "btnDefaultOwnerPassword";
            btnDefaultOwnerPassword.Size = new System.Drawing.Size(37, 28);
            btnDefaultOwnerPassword.TabIndex = 6;
            btnDefaultOwnerPassword.Tag = "";
            btnDefaultOwnerPassword.Text = "🢤";
            btnDefaultOwnerPassword.UseVisualStyleBackColor = true;
            btnDefaultOwnerPassword.Click += button1_Click;
            // 
            // FrmInputBox
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(471, 130);
            Controls.Add(btnDefaultOwnerPassword);
            Controls.Add(btnClose);
            Controls.Add(lblPrompt);
            Controls.Add(txtInput);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmInputBox";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Title";
            Shown += frmInputBox_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox lblPrompt;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDefaultOwnerPassword;
        private System.Windows.Forms.ToolTip toolTipDefaultOwnerPassword;
    }
}