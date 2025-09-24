using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommandLine;
using PDFPass.MVP;
using PDFPass.Resources;

namespace PDFPass
{
    internal static class Program
    {
        // Command line options (CommandLineParser plugin) https://github.com/commandlineparser/commandline
        private class Options
        {
            [Option("owner_pass", Required = false, HelpText = "OwnerPassOption")]
            public string? OwnerPass { get; set; }

            [Option("user_pass", Required = false, HelpText = "UserPassOption")]
            public string? UserPass { get; set; }

            [Option('i', "input", Required = false, HelpText = "InputFileOption")]
            public string? InputFile { get; set; }

            [Option('o', "output", Required = false, HelpText = "OutputFileOption")]
            public string? OutputFile { get; set; }

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

        private static void HandleParsed(Options opts)
        {
            // 1. Create an instance of the View (your form).
            var view = new FrmMain();

            // 2. Create an instance of the Presenter, passing the View to its constructor.
            //    (Note: You may need to replace 'MainPresenter' with your actual presenter class name).
            var presenter = new MainPresenter(view, new MainModel());


            if (opts.InputFile != null)
            {
                view.InputFile = opts.InputFile;
            }

            if (opts.OutputFile != null)
            {
                view.OutputFile = opts.OutputFile;
            }

            if (opts.UserPass != null)
            {
                view.UserPassword = opts.UserPass;
            }

            if (opts.OwnerPass != null)
            {
                view.OwnerPassword = opts.OwnerPass;
            }

            view.EncryptOnStart = opts.Immediate;

            Application.Run(view);
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