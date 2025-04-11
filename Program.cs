using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CommandLine;
using PDFPass.Resources;

namespace PDFPass
{
    internal static class Program
    {
        // Command line options (CommandLineParser plugin) https://github.com/commandlineparser/commandline
        public class Options
        {
            [Option("owner_pass", Required = false, HelpText = "OwnerPassOption")]
            public string OwnerPass { get; set; }

            [Option("user_pass", Required = false, HelpText = "UserPassOption")]
            public string UserPass { get; set; }

            [Option('i', "input", Required = false, HelpText = "InputFileOption")]
            public string InputFile { get; set; }

            [Option('o', "output", Required = false, HelpText = "OutputFileOption")]
            public string OutputFile { get; set; }

            [Option("run", HelpText = "RunImmediatelyOption")]
            public bool Immediate { get; set; }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Initialize localization
            LocalizationManager.SetLanguage("sk-SK");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Parse command line:
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(HandleParsed)
                .WithNotParsed(HandleParseError);
        }

        static void HandleParsed(Options opts)
            // This function is called if the CommandLine.Parser succeeds in parsing all command line options.
        {
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

            Application.Run(form);
        }

        static void HandleParseError(IEnumerable<Error> errors)
            // This function is called if the CommandLine.Parser fails to parse some command line options
            // It should output error messages to CLI and/or desktop.
        {
            var errorMessage = $"{Strings.CommandLineError}{errors}";
            Console.WriteLine(errorMessage);
            MessageBox.Show(errorMessage, Strings.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}