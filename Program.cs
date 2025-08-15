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
        private class Options
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
        private static void Main(string[] args)
        {
            // Initialize localization (i18n)
            Settings.Load();
            var language = Settings.language;
            language ??= LanguageHelper.DefaultLanguage;
            LocalizationManager.SetLanguage(language);

            // Application settings
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Parse command line:
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(HandleParsed)
                .WithNotParsed(HandleParseError);
        }

        static void HandleParsed(Options opts)
        {
            var form = new FrmMain();

            if (opts.InputFile != null)
            {
                form.InputFile = opts.InputFile;
            }

            if (opts.OutputFile != null)
            {
                form.OutputFile = opts.OutputFile;
            }

            if (opts.UserPass != null)
            {
                form.UserPassword = opts.UserPass;
            }

            if (opts.OwnerPass != null)
            {
                form.OwnerPassword = opts.OwnerPass;
            }

            form.EncryptOnStart = opts.Immediate;

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