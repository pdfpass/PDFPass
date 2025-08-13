using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using PDFPass.Resources;
using static System.String;

namespace PDFPass.MVP
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly MainModel _model;
        private const int PwLengthMin = 12;
        private const int PwLengthMax = 24;

        public MainPresenter(IMainView view, MainModel model)
        {
            _view = view;
            _model = model;
            _view.SetPresenter(this);

            _view.EncryptClick += OnEncryptClick;
            _view.DecryptClick += OnDecryptClick;
            _view.SettingsClick += OnSettingsClick;
            _view.ChangeOwnerPasswordClick += OnChangeOwnerPasswordClick;
            _view.GeneratePasswordClick += OnGeneratePasswordClick;
            _view.InputFileChanged += OnInputFileChanged;
            _view.CloseClick += OnCloseClick;
        }

        public void OnFormLoad()
        {
            Settings.Load();
            if (Settings.always_default_owner_password)
            {
                _model.OwnerPassword = Settings.owner_password;
            }
            UpdateView();

            if (_view.EncryptOnStart)
            {
                OnEncryptClick(this, EventArgs.Empty);
            }
        }

        private void OnCloseClick(object sender, EventArgs e)
        {
            _view.CloseForm();
        }

        private void OnInputFileChanged(object sender, EventArgs e)
        {
            _model.InputFile = _view.InputFile;
            if (string.IsNullOrEmpty(_model.InputFile) || !File.Exists(_model.InputFile))
            {
                return;
            }

            try
            {
                PdfUtils.IsPdfFile(_model.InputFile);
            }
            catch (Exception)
            {
                _view.ShowError(Strings.FileNotPdfOrDamaged);
                _view.InputFile = "";
                return;
            }

            var isEncrypted = PdfUtils.IsPdfReaderPasswordSet(_model.InputFile);
            if (IsNullOrEmpty(_view.OutputFile))
            {
                _view.OutputFile = GetFilenameWithSuffix(_model.InputFile, isEncrypted);
            }
            _view.UpdateView(isEncrypted);
        }

        private void OnGeneratePasswordClick(object sender, EventArgs e)
        {
            _view.UserPassword = PdfUtils.GenerateRandomPassword(PwLengthMin, PwLengthMax);
        }

        private void OnChangeOwnerPasswordClick(object sender, EventArgs e)
        {
            var ownerPassword = _view.PromptForPassword(Strings.SetOwnerPassword, Strings.EnterOwnerPasswordPrompt);
            if (ownerPassword != null)
            {
                _view.OwnerPassword = ownerPassword;
                UpdateView();
            }
        }

        private void OnSettingsClick(object sender, EventArgs e)
        {
            var settings = new FrmSettings();
            settings.ShowDialog();
            UpdateView();
        }

        private void OnDecryptClick(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            if (string.IsNullOrEmpty(_view.UserPassword))
            {
                _view.ShowError(Strings.NoPasswordEntered);
                return;
            }

            if (!PdfUtils.IsPasswordCorrect(_view.InputFile, _view.UserPassword))
            {
                _view.ShowError(Strings.IncorrectPassword);
                return;
            }

            if (PdfUtils.IsPasswordWithFullPermissions(_view.InputFile, _view.UserPassword))
            {
                if (File.Exists(_view.OutputFile) && !_view.ConfirmOverwrite())
                {
                    return;
                }

                try
                {
                    PdfUtils.WriteDecryptedPdf(_view.InputFile, _view.OutputFile, _view.UserPassword);
                }
                catch (Exception ex)
                {
                    _view.ShowError($"{Strings.UnknownErrorShort} {ex.Message}");
                    return;
                }
                ExecuteAfterSteps();
            }
            else
            {
                _view.ShowInfo(Strings.OwnerPasswordRequired);
            }
        }

        private void OnEncryptClick(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            if (IsNullOrWhiteSpace(_view.UserPassword) && IsNullOrWhiteSpace(_view.OwnerPassword))
            {
                _view.ShowError(Strings.NoPasswordEntered);
                return;
            }

            if (IsNullOrWhiteSpace(_view.UserPassword))
            {
                if (_view.ShowWarning(Strings.NoReadingPasswordWarning))
                {
                    return;
                }
            }

            if (Settings.password_confirm)
            {
                var confirmedPassword = _view.PromptForPassword(Strings.ConfirmReadingPasswordTitle, Strings.ConfirmReadingPassword);
                if (confirmedPassword == null)
                {
                    return; // User cancelled
                }
                if (confirmedPassword != _view.UserPassword)
                {
                    _view.ShowError(Strings.PasswordsMismatch);
                    return;
                }

                if (!string.IsNullOrEmpty(_view.OwnerPassword))
                {
                    var confirmedOwnerPassword = _view.PromptForPassword(Strings.ConfirmOwnerPasswordTitle, Strings.OwnerPasswordSetConfirm);
                    if (confirmedOwnerPassword == null)
                    {
                        return; // User cancelled
                    }
                    if (confirmedOwnerPassword != _view.OwnerPassword)
                    {
                        _view.ShowError(Strings.OwnerPasswordMismatch);
                        return;
                    }
                }
            }

            try
            {
                var writerProperties = GetWriterProperties();
                var waterMarkText = _view.WatermarkEnabled ? _view.WatermarkText : "";
                PdfUtils.WriteEncryptedPdf(_view.InputFile, _view.OutputFile, writerProperties, waterMarkText);
            }
            catch (Exception ex)
            {
                _view.ShowError($"{Strings.UnknownError} {ex.Message}");
                return;
            }

            ExecuteAfterSteps();
        }

        private bool ValidateInput()
        {
            _view.InputFile = _view.InputFile.Trim();
            _view.OutputFile = _view.OutputFile.Trim();

            if (string.Equals(_view.InputFile, _view.OutputFile, StringComparison.CurrentCultureIgnoreCase))
            {
                _view.ShowError(Strings.SourceAndDestinationSame);
                return false;
            }

            if (!File.Exists(_view.InputFile))
            {
                _view.ShowError(Strings.SourceFileDoesNotExist);
                return false;
            }

            if (File.Exists(_view.OutputFile) && !_view.ConfirmOverwrite())
            {
                return false;
            }

            return true;
        }

        private WriterProperties GetWriterProperties()
        {
            var encryptionProperties = (int)Settings.encryption_type;
            if (!Settings.encrypt_metadata)
            {
                encryptionProperties |= EncryptionConstants.DO_NOT_ENCRYPT_METADATA;
            }

            var documentOptions = 0;
            if (Settings.allow_printing) documentOptions |= EncryptionConstants.ALLOW_PRINTING;
            if (Settings.allow_degraded_printing) documentOptions |= EncryptionConstants.ALLOW_DEGRADED_PRINTING;
            if (Settings.allow_modifying) documentOptions |= EncryptionConstants.ALLOW_MODIFY_CONTENTS;
            if (Settings.allow_modifying_annotations) documentOptions |= EncryptionConstants.ALLOW_MODIFY_ANNOTATIONS;
            if (Settings.allow_copying) documentOptions |= EncryptionConstants.ALLOW_COPY;
            if (Settings.allow_form_fill) documentOptions |= EncryptionConstants.ALLOW_FILL_IN;
            if (Settings.allow_assembly) documentOptions |= EncryptionConstants.ALLOW_ASSEMBLY;
            if (Settings.allow_screenreaders) documentOptions |= EncryptionConstants.ALLOW_SCREENREADERS;

            var writerProperties = new WriterProperties();
            var userPassword = Encoding.UTF8.GetBytes(_view.UserPassword);
            var ownerPassword = Encoding.UTF8.GetBytes(_view.OwnerPassword);

            writerProperties.SetStandardEncryption(userPassword,
                IsNullOrEmpty(_view.OwnerPassword) ? userPassword : ownerPassword,
                documentOptions,
                encryptionProperties);

            return writerProperties;
        }

        private void ExecuteAfterSteps()
        {
            if (Settings.run_after)
            {
                try
                {
                    Process.Start(Settings.run_after_file, Settings.run_after_arguments + " " + _view.OutputFile);
                }
                catch (Exception ex)
                {
                    _view.ShowError($"{Strings.CannotRunCommand} {ex.Message}");
                }
            }

            if (Settings.open_after)
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _view.OutputFile,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }

            if (Settings.show_folder_after)
            {
                Process.Start("explorer.exe", $"/select, \"{_view.OutputFile}\"");
            }

            if (Settings.close_after)
            {
                _view.CloseForm();
            }
        }

        private string GetFilenameWithSuffix(string fileName, bool isInputEncrypted)
        {
            var newFileName = isInputEncrypted
                ? $"{Path.GetFileNameWithoutExtension(fileName).Replace(Strings.Encrypted, "")}{Strings.Decrypted}.pdf"
                : $"{Path.GetFileNameWithoutExtension(fileName)}-{Strings.Encrypted}.pdf";
            return Path.Combine(Path.GetDirectoryName(fileName)!, newFileName);
        }

        private void UpdateView()
        {
            if (string.IsNullOrEmpty(_view.InputFile) || !File.Exists(_view.InputFile))
            {
                _view.UpdateView(false);
                return;
            }
            var isEncrypted = PdfUtils.IsPdfReaderPasswordSet(_view.InputFile);
            _view.UpdateView(isEncrypted);
        }
    }
}
