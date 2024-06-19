using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CommandLine;

namespace PDFPass
{
    static class Program
    {
        // Command line options (CommandLineParser plugin) https://github.com/commandlineparser/commandline
        private class Options
        {
            [Option("owner_pass", Required = false, HelpText = "Zadajte heslo vlastníka")]
            public string OwnerPass { get; set; }

            [Option("user_pass", Required = false, HelpText = "Zadajte heslo používateľa")]
            public string UserPass { get; set; }

            [Option('i', "input", Required = false, HelpText = "Uveďte vstupný PDF súbor k zašifrovaniu")]
            public string InputFile { get; set; }

            [Option('o', "output", Required = false, HelpText = "Uveďte výstupný zašifrovaný súbor")]
            public string OutputFile { get; set; }

            [Option("run",
                HelpText = "Spustiť šifrovanie okamžite po štarte (nevyžaduje stlačenie tlačidla Zahesluj)")]
            public bool Immediate { get; set; }
        }


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// Parse command line:
			Parser.Default.ParseArguments<Options>(args)
				.WithParsed(HandleParsed)
				.WithNotParsed(HandleParseError); 
		}

		static void HandleParsed(Options opts)
        // This function is called if the CommandLine.Parser succeeds in parsing all command line options.
        {
			Application.SetCompatibleTextRenderingDefault(false);

			// Create the UI form instance
			var form = new FrmMain();
											
			// If input filename was specified, set it in the main form
			if (opts.InputFile != null)
            {
				form.txtInputFile.Text = opts.InputFile;
            }

			// If output filename was specified, set it in the main form
			if (opts.OutputFile != null)
            {
				form.txtOutputFile.Text = opts.OutputFile;
            }
            else if (opts.InputFile != null)
            {
                var outputFile = opts.InputFile;
                var extension = Path.GetExtension(opts.InputFile);
                {
	                var result = outputFile.Substring(0, outputFile.Length - extension.Length);
	                form.txtOutputFile.Text = result + "_zašifrovaný" + extension;
                }
            }

			// If user password was specified, set it in the main form
			if (opts.UserPass != null)
            {
				form.txtPassword.Text = opts.UserPass;
            }

			// If owner password was specified, set it in the main form and show message.
			if (opts.OwnerPass != null)
            {
				form.OwnerPassword = opts.OwnerPass;
				form.lblOwnerPasswordSet.Visible = true;
            }

			// If executing immediately, set the Run flag.
			form.EncryptOnStart = (opts.Immediate);

			Application.EnableVisualStyles();
			Application.Run(form);
		}

        static void HandleParseError(IEnumerable<Error> errors)
            // This function is called if the CommandLine.Parser fails to parse some command line options
            // It should output error messages to CLI and/or desktop.
        {
            Console.WriteLine("Chybné parametre príkazového riadku: " + errors.ToString());
            MessageBox.Show("Chybné parametre príkazového riadku: " + errors.ToString(), "Chyba!", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
