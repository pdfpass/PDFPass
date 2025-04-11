using System;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using Microsoft.Win32;

// Registry access


namespace PDFPass
{
    public static class Settings
    {
        public enum EncryptionType
        {
            AES_256 = EncryptionConstants.ENCRYPTION_AES_256,
            AES_128 = EncryptionConstants.ENCRYPTION_AES_128,
            RC4_128 = EncryptionConstants.STANDARD_ENCRYPTION_128
        }

        public static bool run_after; // Run program after encrypting?
        public static string run_after_file; // File to run after encrypting
        public static string run_after_arguments; // Arguments to pass to the run_after file.
        public static bool password_confirm; // Confirm password?
        public static bool close_after; // Close after encrypting?
        public static bool show_folder_after; // Show folder in Explorer after encrypting?
        public static bool open_after; // Open the destination file in its default program?

        // Encryption options:
        public static EncryptionType encryption_type; // Type of encryption to use
        public static bool encrypt_metadata; // Should metadata be encrypted?
        public static bool allow_printing; // Should end user be allowed to print PDF?
        public static bool allow_degraded_printing; // Should end user be allowed to print PDF degraded?
        public static bool allow_modifying; // Should end user be allowed to modify the PDF?
        public static bool allow_modifying_annotations; // Should end user be allowed to modify annotations?
        public static bool allow_copying; // Should end user be allowed to copy from PDF?
        public static bool allow_form_fill; // Should end user be allowed to fill in form fields?
        public static bool allow_assembly; // Should end user be allowed to assemble the document?
        public static bool allow_screenreaders; // Should screenreaders be allowed to access the document?
        public static string owner_password;
        public static bool always_default_owner_password;

        // i18n
        public static string language; // The selected language code (e.g., "sk-SK", "en", "cs-CZ")


        // Events to execute upon setting changes
        public delegate void SettingChangedNotification();

        public static List<SettingChangedNotification>
            Notify = []; // Add delegate functions to this list to be notified.

        // Constants:
        const string RegKey = "HKEY_CURRENT_USER\\Software\\PDFPASS\\"; // Main registry key

        public static void Load()
        {
            // Read settings from registry.
            object obj;

            // Run program after encryption?
            obj = Registry.GetValue(RegKey, "run_after", 0);
            if (obj == null)
            {
                obj = 0;
            }

            run_after = (int)obj == 1; // Convert to boolean.

            // Program to run:
            obj = Registry.GetValue(RegKey, "run_after_file", null);
            if (obj == null)
            {
                obj = "";
            }

            run_after_file = (string)obj;

            // Run After arguments
            obj = Registry.GetValue(RegKey, "run_after_arguments", null);
            if (obj == null)
            {
                obj = "";
            }

            run_after_arguments = (string)obj;

            // Require password confirmation
            obj = Registry.GetValue(RegKey, "password_confirm", 0);
            if (obj == null)
            {
                obj = 0;
            }

            password_confirm = (int)obj == 1;

            // Close after encrypting
            obj = Registry.GetValue(RegKey, "close_after", 0);
            if (obj == null)
            {
                obj = 0;
            }

            close_after = (int)obj == 1;


            // Show folder after encrypting
            obj = Registry.GetValue(RegKey, "show_folder_after", 0);
            if (obj == null)
            {
                obj = 0;
            }

            show_folder_after = (int)obj == 1;

            // Open file after encrypting
            obj = Registry.GetValue(RegKey, "open_after", 0);
            if (obj == null)
            {
                obj = 0;
            }

            open_after = (int)obj == 1;


            // Encryption options:
            // Encryption type:
            obj = Registry.GetValue(RegKey, "encryption_type", null);
            if (obj == null)
            {
                obj = (int)EncryptionType.AES_256;
            }

            if (!Enum.IsDefined(typeof(EncryptionType), (int)obj)) // If not a valid option, use default:
            {
                encryption_type = EncryptionType.AES_256; // Default to AES_256
            }
            else
            {
                encryption_type = (EncryptionType)obj;
            }

            // Encrypt metadata
            obj = Registry.GetValue(RegKey, "encrypt_metadata", 0);
            if (obj == null)
            {
                obj = 0;
            }

            encrypt_metadata = (int)obj == 1;

            // Allow printing
            obj = Registry.GetValue(RegKey, "allow_printing", 0);
            if (obj == null)
            {
                obj = 0;
            }

            allow_printing = (int)obj == 1;

            // Allow degraded printing
            obj = Registry.GetValue(RegKey, "allow_degraded_printing", 0);
            if (obj == null)
            {
                obj = 0;
            }

            allow_degraded_printing = (int)obj == 1;

            // Allow modifying
            obj = Registry.GetValue(RegKey, "allow_modifying", 0);
            if (obj == null)
            {
                obj = 0;
            }

            allow_modifying = (int)obj == 1;

            // Allow modifying notations
            obj = Registry.GetValue(RegKey, "allow_modifying_annotations", 0);
            if (obj == null)
            {
                obj = 0;
            }

            allow_modifying_annotations = (int)obj == 1;

            // Allow copying
            obj = Registry.GetValue(RegKey, "allow_copying", 0);
            if (obj == null)
            {
                obj = 0;
            }

            allow_copying = (int)obj == 1;

            // Allow form fill
            obj = Registry.GetValue(RegKey, "allow_form_fill", 0);
            if (obj == null)
            {
                obj = 0;
            }

            allow_form_fill = (int)obj == 1;

            // Allow assembly
            obj = Registry.GetValue(RegKey, "allow_assembly", 0) ?? 0;
            allow_assembly = (int)obj == 1;

            // Allow screenreaders
            obj = Registry.GetValue(RegKey, "allow_screenreaders", 0);
            if (obj == null)
            {
                obj = 0;
            }

            allow_screenreaders = (int)obj == 1;

            // Owner Password:
            obj = Registry.GetValue(RegKey, "owner_password", null);
            if (obj == null)
            {
                obj = PdfUtils.GenerateRandomPassword(20, 25);
                Registry.SetValue(RegKey, "owner_password", (string)obj, RegistryValueKind.String);
            }

            owner_password = (string)obj;

            //Set Always Deafult Owner Password:
            obj = Registry.GetValue(RegKey, "always_default_owner_password", 1);
            if (obj == null)
            {
                obj = 1;
                Registry.SetValue(RegKey, "always_default_owner_password", (int)obj, RegistryValueKind.DWord);
            }

            always_default_owner_password = (int)obj == 1;

            // Selected language
            obj = Registry.GetValue(RegKey, "language", null);
            if (obj == null)
            {
                obj = "sk-SK"; // Default to Slovak
            }

            language = (string)obj;

            // Notify all listeners of updates.
            CallNotify();
        }

        private static void CallNotify()
            // Notify all listeners of updates.
        {
            // Notify each listener of the updates.
            foreach (var changedNotification in Notify)
            {
                changedNotification(); // Call the function.
            }
        }


        public static void Save()
            // Write all settings to registry
        {
            Registry.SetValue(RegKey, "run_after", run_after, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "run_after_file", run_after_file, RegistryValueKind.String);
            Registry.SetValue(RegKey, "run_after_arguments", run_after_arguments, RegistryValueKind.String);

            Registry.SetValue(RegKey, "password_confirm", password_confirm, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "close_after", close_after, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "show_folder_after", show_folder_after, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "open_after", open_after, RegistryValueKind.DWord);

            // Encryption options:
            Registry.SetValue(RegKey, "encryption_type", encryption_type, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "encrypt_metadata", encrypt_metadata, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "allow_printing", allow_printing, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "allow_degraded_printing", allow_degraded_printing, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "allow_modifying", allow_modifying, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "allow_modifying_annotations", allow_modifying_annotations,
                RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "allow_copying", allow_copying, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "allow_form_fill", allow_form_fill, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "allow_assembly", allow_assembly, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "allow_screenreaders", allow_screenreaders, RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "owner_password", owner_password, RegistryValueKind.String);
            Registry.SetValue(RegKey, "always_default_owner_password", always_default_owner_password,
                RegistryValueKind.DWord);
            Registry.SetValue(RegKey, "language", language, RegistryValueKind.String);
            // Notify all listeners
            CallNotify();
        }
    }
}