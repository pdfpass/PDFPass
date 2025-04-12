namespace PDFPass.Resources
{
    /// <summary>
    /// Provides strongly-typed access to localized resources
    /// </summary>
    public static class Strings
    {
        // Main form resources
        public static string ApplicationTitle => LocalizationManager.GetString(nameof(ApplicationTitle));
        public static string InputFile => LocalizationManager.GetString(nameof(InputFile));
        public static string SelectFileForEncryption => LocalizationManager.GetString(nameof(SelectFileForEncryption));
        public static string OutputFile => LocalizationManager.GetString(nameof(OutputFile));

        public static string SelectPathForEncryptedFile =>
            LocalizationManager.GetString(nameof(SelectPathForEncryptedFile));

        public static string Passwords => LocalizationManager.GetString(nameof(Passwords));
        public static string Change => LocalizationManager.GetString(nameof(Change));
        public static string OwnerPasswordSet => LocalizationManager.GetString(nameof(OwnerPasswordSet));
        public static string OwnerPasswordEmpty => LocalizationManager.GetString(nameof(OwnerPasswordEmpty));
        public static string CopiedToClipboard => LocalizationManager.GetString(nameof(CopiedToClipboard));
        public static string PasswordForLocking => LocalizationManager.GetString(nameof(PasswordForLocking));
        public static string PasswordForUnlocking => LocalizationManager.GetString(nameof(PasswordForUnlocking));
        public static string Generate => LocalizationManager.GetString(nameof(Generate));
        public static string EnterPassword => LocalizationManager.GetString(nameof(EnterPassword));
        public static string PasswordLengthWarning => LocalizationManager.GetString(nameof(PasswordLengthWarning));
        public static string Paste => LocalizationManager.GetString(nameof(Paste));
        public static string Copy => LocalizationManager.GetString(nameof(Copy));
        public static string Decrypt => LocalizationManager.GetString(nameof(Decrypt));
        public static string Encrypt => LocalizationManager.GetString(nameof(Encrypt));
        public static string Close => LocalizationManager.GetString(nameof(Close));
        public static string PDFFiles => LocalizationManager.GetString(nameof(PDFFiles));
        public static string AllFiles => LocalizationManager.GetString(nameof(AllFiles));
        public static string Settings => LocalizationManager.GetString(nameof(Settings));
        public static string Version => LocalizationManager.GetString(nameof(Version));
        public static string Watermark => LocalizationManager.GetString(nameof(Watermark));
        public static string Text => LocalizationManager.GetString(nameof(Text));
        public static string UseWatermark => LocalizationManager.GetString(nameof(UseWatermark));
        public static string Sample => LocalizationManager.GetString(nameof(Sample));
        public static string WCopy => LocalizationManager.GetString(nameof(WCopy));
        public static string Confidential => LocalizationManager.GetString(nameof(Confidential));
        public static string Draft => LocalizationManager.GetString(nameof(Draft));
        public static string ClipboardValuePrefix => LocalizationManager.GetString(nameof(ClipboardValuePrefix));

        // Error messages
        public static string FileNotPdfOrDamaged => LocalizationManager.GetString(nameof(FileNotPdfOrDamaged));
        public static string FileNotExist => LocalizationManager.GetString(nameof(FileNotExist));
        public static string ErrorTitle => LocalizationManager.GetString(nameof(ErrorTitle));

        public static string SourceAndDestinationSame =>
            LocalizationManager.GetString(nameof(SourceAndDestinationSame));

        public static string SourceDestinationError => LocalizationManager.GetString(nameof(SourceDestinationError));
        public static string SourceFileDoesNotExist => LocalizationManager.GetString(nameof(SourceFileDoesNotExist));
        public static string ConfirmOverwriteFile => LocalizationManager.GetString(nameof(ConfirmOverwriteFile));
        public static string OverwriteFile => LocalizationManager.GetString(nameof(OverwriteFile));
        public static string NoPasswordEntered => LocalizationManager.GetString(nameof(NoPasswordEntered));

        public static string NoReadingPasswordWarning =>
            LocalizationManager.GetString(nameof(NoReadingPasswordWarning));

        public static string Warning => LocalizationManager.GetString(nameof(Warning));
        public static string ConfirmReadingPassword => LocalizationManager.GetString(nameof(ConfirmReadingPassword));

        public static string ConfirmReadingPasswordTitle =>
            LocalizationManager.GetString(nameof(ConfirmReadingPasswordTitle));

        public static string PasswordsMismatch => LocalizationManager.GetString(nameof(PasswordsMismatch));
        public static string OwnerPasswordSetConfirm => LocalizationManager.GetString(nameof(OwnerPasswordSetConfirm));

        public static string ConfirmOwnerPasswordTitle =>
            LocalizationManager.GetString(nameof(ConfirmOwnerPasswordTitle));

        public static string OwnerPasswordMismatch => LocalizationManager.GetString(nameof(OwnerPasswordMismatch));
        public static string UnknownError => LocalizationManager.GetString(nameof(UnknownError));
        public static string IncorrectPassword => LocalizationManager.GetString(nameof(IncorrectPassword));
        public static string OwnerPasswordRequired => LocalizationManager.GetString(nameof(OwnerPasswordRequired));
        public static string Information => LocalizationManager.GetString(nameof(Information));
        public static string UnknownErrorShort => LocalizationManager.GetString(nameof(UnknownErrorShort));
        public static string CannotRunCommand => LocalizationManager.GetString(nameof(CannotRunCommand));

        // Settings form resources
        public static string SettingsTitle => LocalizationManager.GetString(nameof(SettingsTitle));
        public static string Copyright => LocalizationManager.GetString(nameof(Copyright));
        public static string Confirm => LocalizationManager.GetString(nameof(Confirm));
        public static string Cancel => LocalizationManager.GetString(nameof(Cancel));

        public static string AfterSuccessfulEncryption =>
            LocalizationManager.GetString(nameof(AfterSuccessfulEncryption));

        public static string OutputFilePathAdded => LocalizationManager.GetString(nameof(OutputFilePathAdded));
        public static string Parameters => LocalizationManager.GetString(nameof(Parameters));
        public static string ParametersPlaceholder => LocalizationManager.GetString(nameof(ParametersPlaceholder));
        public static string OpenOutputFile => LocalizationManager.GetString(nameof(OpenOutputFile));
        public static string OpenFileInExplorer => LocalizationManager.GetString(nameof(OpenFileInExplorer));
        public static string ClosePDFPass => LocalizationManager.GetString(nameof(ClosePDFPass));
        public static string RunProgram => LocalizationManager.GetString(nameof(RunProgram));
        public static string ProgramPathPlaceholder => LocalizationManager.GetString(nameof(ProgramPathPlaceholder));
        public static string EncryptionOptions => LocalizationManager.GetString(nameof(EncryptionOptions));
        public static string SetAutomatically => LocalizationManager.GetString(nameof(SetAutomatically));
        public static string OwnerPassword => LocalizationManager.GetString(nameof(OwnerPassword));
        public static string PermanentOwnerPassword => LocalizationManager.GetString(nameof(PermanentOwnerPassword));

        public static string PermissionsIgnoredWarning =>
            LocalizationManager.GetString(nameof(PermissionsIgnoredWarning));

        public static string AllowPrintingLowRes => LocalizationManager.GetString(nameof(AllowPrintingLowRes));
        public static string AllowPageArrangement => LocalizationManager.GetString(nameof(AllowPageArrangement));
        public static string AllowAccessTechnologies => LocalizationManager.GetString(nameof(AllowAccessTechnologies));
        public static string AllowFillForm => LocalizationManager.GetString(nameof(AllowFillForm));
        public static string AllowAddAnnotations => LocalizationManager.GetString(nameof(AllowAddAnnotations));

        public static string AllowDocumentModification =>
            LocalizationManager.GetString(nameof(AllowDocumentModification));

        public static string AllowContentCopy => LocalizationManager.GetString(nameof(AllowContentCopy));

        public static string PermissionsForOutputFile =>
            LocalizationManager.GetString(nameof(PermissionsForOutputFile));

        public static string AllowPrintingHighRes => LocalizationManager.GetString(nameof(AllowPrintingHighRes));
        public static string EncryptMetadata => LocalizationManager.GetString(nameof(EncryptMetadata));
        public static string Algorithm => LocalizationManager.GetString(nameof(Algorithm));
        public static string ConfirmPassword => LocalizationManager.GetString(nameof(ConfirmPassword));
        public static string ExecutableFiles => LocalizationManager.GetString(nameof(ExecutableFiles));
        public static string SupportDeveloper => LocalizationManager.GetString(nameof(SupportDeveloper));
        public static string AES256Recommended => LocalizationManager.GetString(nameof(AES256Recommended));
        public static string AES128 => LocalizationManager.GetString(nameof(AES128));
        public static string RC4128 => LocalizationManager.GetString(nameof(RC4128));
        public static string RunFileNotExists => LocalizationManager.GetString(nameof(RunFileNotExists));

        // Input box resources
        public static string SetOwnerPassword => LocalizationManager.GetString(nameof(SetOwnerPassword));

        public static string EnterOwnerPasswordPrompt =>
            LocalizationManager.GetString(nameof(EnterOwnerPasswordPrompt));

        public static string FillWithDefaultPassword => LocalizationManager.GetString(nameof(FillWithDefaultPassword));

        // Command-line options
        public static string OwnerPassOption => LocalizationManager.GetString(nameof(OwnerPassOption));
        public static string UserPassOption => LocalizationManager.GetString(nameof(UserPassOption));
        public static string InputFileOption => LocalizationManager.GetString(nameof(InputFileOption));
        public static string OutputFileOption => LocalizationManager.GetString(nameof(OutputFileOption));
        public static string RunImmediatelyOption => LocalizationManager.GetString(nameof(RunImmediatelyOption));
        public static string CommandLineError => LocalizationManager.GetString(nameof(CommandLineError));

        // Language settings
        public static string LanguageTitle => LocalizationManager.GetString(nameof(LanguageTitle));

        public static string LanguageChangeRestartRequired =>
            LocalizationManager.GetString(nameof(LanguageChangeRestartRequired));

        // File handling
        public static string Encrypted => LocalizationManager.GetString(nameof(Encrypted));
        public static string Decrypted => LocalizationManager.GetString(nameof(Decrypted));
    }
}