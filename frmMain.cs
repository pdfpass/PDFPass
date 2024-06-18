using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iText.Kernel.Pdf;

// LOL

namespace PDFPass
{
	

	public partial class FrmMain : Form
	{
		const int PW_LENGTH_MIN = 12;	// Minimum generated password length
		const int PW_LENGTH_MAX = 24;   // Maximum generated password length
		const string PW_CHARS = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789"; // List of characters to be used in random passwords

		public string OwnerPassword = "";  // The owner password, if any.
		public bool EncryptOnStart = false;	// Allows encryption via command line without user interaction


		public FrmMain()
		{
			InitializeComponent();
		}

		private string GetFilenameWithSuffix(string file, string suffix = "-zašifrovaný")
		{
			var newFileName = $"{Path.GetFileNameWithoutExtension(file)}{suffix}.pdf";
			return Path.Combine(Path.GetDirectoryName(file)!, newFileName);
		}

		private void btnInputBrowse_Click(object sender, EventArgs e)
		{
			var result = dlgOpen.ShowDialog();
			if (result == DialogResult.Cancel) { return; }

			txtInputFile.Text = dlgOpen.FileName;
			txtOutputFile.Text = GetFilenameWithSuffix(dlgOpen.FileName);
		}

		private void btnOutputBrowse_Click(object sender, EventArgs e)
		{
			var result = dlgSave.ShowDialog();
			if (result == DialogResult.Cancel) { return; }

			txtOutputFile.Text = dlgSave.FileName;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			// Add listener for updated settings
			Settings.notify.Add(settingsChanged);

			// Load settings from registry
			Settings.load();		
			
			// If immediate run is enabled, click Run button (see command line options)
			if (EncryptOnStart)
            {
				// Click the Encrypt button immediately.
				btnEncrypt_Click(sender, e);
            }
		}

		private void settingsChanged()
		{
			// This function is executed when settings change.
			Console.WriteLine("Notifikacia zmeny nastavenia.");
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			// Close the app
			Close();
		}

		private void btnPasswordGenerate_Click(object sender, EventArgs e)
		{
			// Generate a random password
			var rnd = new Random();	// Random number generator
			var length = rnd.Next(PW_LENGTH_MIN, PW_LENGTH_MAX);	// Choose password length.
			var result = "";

			// Pick 'length' characters from the allowed characters.
			for(var i = 0; i<length; i++)
			{
				result += PW_CHARS[rnd.Next(0, PW_CHARS.Length - 1)].ToString();
			}

			// Set password
			txtPassword.Text = result;

			// Copy to clipboard
			btnCopy_Click(sender, e);
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			// Copy password to clipboard.
			txtPassword.Focus();
			txtPassword.SelectAll();

			if (string.IsNullOrEmpty(txtPassword.Text)) return;
			
			Clipboard.SetText(txtPassword.Text);
			// Show Copied label
			lblCopied.Visible = true;


		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{
			// Hide Copied label
			lblCopied.Visible = false;

			// Show Password Length warning if exceeding 32 chars.
			lblPasswordLength.Visible = txtPassword.Text.Length > 32;

		}

		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			// Hide Copied label
			lblCopied.Visible = false;

		}

		private void btnEncrypt_Click(object sender, EventArgs e)
		{
			// Clean up text
			txtInputFile.Text = txtInputFile.Text.Trim();
			txtOutputFile.Text = txtOutputFile.Text.Trim();

			// Ensure input and output are not the same.
			if (string.Equals(txtInputFile.Text, txtOutputFile.Text, StringComparison.CurrentCultureIgnoreCase))
			{
				MessageBox.Show("Zdrojový a výstupný súbor nemožu byt rovnaké alebo prázdne.", "Chybný zdroj/výstup", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtOutputFile.Focus();
				txtOutputFile.SelectAll();
				return;
			}

			// Ensure input file exists.
			if (!File.Exists(txtInputFile.Text))
			{
				MessageBox.Show("Zdrojový súbor neexistuje.");
				txtInputFile.Focus();
				txtInputFile.SelectAll();
				return;
			}

			// If output file exists, prompt to overwrite.
			if (File.Exists(txtOutputFile.Text))
			{
				if(MessageBox.Show(this, "Výstupny súbor už existuje. Želate si prepísať súbor?","Prepísať súbor?", MessageBoxButtons.YesNo) != DialogResult.Yes)
				{
					txtOutputFile.Focus();
					txtOutputFile.SelectAll();
					return;
				}
			}

			// Verify password:
			if (txtPassword.Text == "")
			{
				MessageBox.Show("Nebolo zadané heslo.");
				txtPassword.Focus();
				return;
			}
			
			// Confirm password:
			if (Settings.password_confirm)
			{
				var input = new FrmInputBox();
				input.prompt = "Zadajte heslo pre potvrdenie.";
				input.title = "Potvrdenie hesla";
				input.password = true;
				input.ShowDialog();	// Modal, blocking call

				if (input.cancelled) { return;  }

				// If password doesn't match, stop.
				if (input.result != txtPassword.Text)
				{
					MessageBox.Show("Hesla sa nezhodujú. Zopakujte.");
					return;
				}

				if (OwnerPassword != "")
                {
					input.prompt = "Heslo pre editovanie bolo nastavené. Potvrdťe prosím heslo opät.";
					input.title = "Potvrdenie hesla pre editovanie";
					input.password = true;
					input.ShowDialog();
					if (input.cancelled) { return; }
					if (input.result != OwnerPassword)
                    {
						MessageBox.Show("Heslo pre editovanie nie je rovnaké. Prosím, zopakujte.");
						return;
                    }
                }
			}

			// See https://itextpdf.com/en/resources/faq/technical-support/itext-7/how-protect-already-existing-pdf-password
			try
			{

				// Set mouse cursor to wait.
				Cursor.Current = Cursors.WaitCursor;
				Application.DoEvents();

				// Encryption properties:
				var encryptionProperties = (int) Settings.encryption_type;
				
				// If specified, disable encrypting metadata.
				if (!Settings.encrypt_metadata)
				{
					encryptionProperties |= EncryptionConstants.DO_NOT_ENCRYPT_METADATA;
				}

				// Set document options
				var documentOptions = 0;
				if (Settings.allow_printing)	{ documentOptions |= EncryptionConstants.ALLOW_PRINTING; }
				if (Settings.allow_degraded_printing) { documentOptions |= EncryptionConstants.ALLOW_DEGRADED_PRINTING; }
				if (Settings.allow_modifying) { documentOptions |= EncryptionConstants.ALLOW_MODIFY_CONTENTS; }
				if (Settings.allow_modifying_annotations) { documentOptions |= EncryptionConstants.ALLOW_MODIFY_ANNOTATIONS; }
				if (Settings.allow_copying) { documentOptions |= EncryptionConstants.ALLOW_COPY; }
				if (Settings.allow_form_fill) { documentOptions |= EncryptionConstants.ALLOW_FILL_IN; }
				if (Settings.allow_assembly) { documentOptions |= EncryptionConstants.ALLOW_ASSEMBLY; }
				if (Settings.allow_screenreaders) { documentOptions |= EncryptionConstants.ALLOW_SCREENREADERS; }

				var reader = new PdfReader(txtInputFile.Text);	// Create a PdfReader with the input file.
				var prop = new WriterProperties();	// Set properties of output
	
				prop.SetStandardEncryption(Encoding.ASCII.GetBytes(txtPassword.Text), OwnerPassword == "" ? null : Encoding.ASCII.GetBytes(OwnerPassword), documentOptions, encryptionProperties);  // Enable encryption
				// Setting Owner Password to null generates random password.

				var writer = new PdfWriter(txtOutputFile.Text, prop);	// Set up the output file
				var pdf = new PdfDocument(reader, writer);	// Create the new document
				pdf.Close();	// Close the output document.
			}
			catch (Exception ex)
			{
				MessageBox.Show("Chyba počas spracovania súboru: " + ex.Message);
				Cursor.Current = Cursors.Default;
				return;
			}

			// If launching a program:
			if (Settings.run_after)
			{
				// Attempt to run the program, passing the newly encrypted filename.
				try
				{
					Process.Start(Settings.run_after_file, Settings.run_after_arguments + " " + txtOutputFile.Text);
				}
				catch (Exception ex)
                {
					MessageBox.Show("Nie je možné spustiť príkaz.  Chyba: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

			}

			// If opening the output file:
			if (Settings.open_after)
			{
				// Attempt to launch the default app for the file.
				var psi = new ProcessStartInfo
				{
					FileName = txtOutputFile.Text,
					UseShellExecute = true
				};

				Process.Start(psi);

			}

			// If launching Explorer:
			if (Settings.show_folder_after)
			{
				// Attempt to launch the folder with the file highlighted.
				var argument = "/select, \"" + txtOutputFile.Text + "\"";

				Process.Start("explorer.exe", argument);
			}

			// If closing after encryption
			if (Settings.close_after)
			{
				Close();
			}

			Cursor.Current = Cursors.Default;	// Return to default cursor.
		}

		private void btnSettings_Click(object sender, EventArgs e)
		{
			var settings = new frmSettings();
			settings.ShowDialog();
		}

        private void lnkPasswordOwner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			var input = new FrmInputBox();
			input.title = "Nastaviť heslo pre editovanie";
			input.prompt = "Zadajte heslo pre editovanie.\r\n(Heslo pre editovanie umožní plnú kontrolu nad obsahom súboru PDF.)\r\nAk nebude heslo zadané, bude vygenerované náhodné heslo.\r\nStlačte Storno ak chcete anulovať heslo pre editovanie";
			input.password = true;
			input.ShowDialog();
			OwnerPassword = input.cancelled ? "" : input.result;
			lblOwnerPasswordSet.Visible = (OwnerPassword != "");
        }

      
    }
}
