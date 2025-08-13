using System;

namespace PDFPass.MVP
{
    public interface IMainView
    {
        // Properties
        string InputFile { get; set; }
        string OutputFile { get; set; }
        string UserPassword { get; set; }
        string OwnerPassword { get; set; }
        bool WatermarkEnabled { get; set; }
        string WatermarkText { get; set; }
        bool EncryptOnStart { get; set; }

        // Events
        event EventHandler EncryptClick;
        event EventHandler DecryptClick;
        event EventHandler SettingsClick;
        event EventHandler ChangeOwnerPasswordClick;
        event EventHandler GeneratePasswordClick;
        event EventHandler InputFileChanged;
        event EventHandler OutputFileChanged;
        event EventHandler CloseClick;


        // Methods
        void ShowError(string message);
        bool ShowWarning(string message);
        void ShowInfo(string message);
        void UpdateView(bool isInputEncrypted);
        void CloseForm();
        void SetPresenter(MainPresenter presenter);
        bool ConfirmOverwrite();
        string PromptForPassword(string title, string prompt);
    }
}
