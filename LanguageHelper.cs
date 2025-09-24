using System.Collections.Generic;
using System.Windows.Forms;

namespace PDFPass
{
    public static class LanguageHelper
    {
        public const string DefaultLanguage = "en-EN";

        public static Dictionary<string, string> AvailableLanguages => new()
        {
            { DefaultLanguage, "English" },
            { "sk-SK", "Slovenčina" },
            { "cs-CZ", "Čeština" },
            { "fi-FI", "Suomi" }
        };

        /// <summary>
        /// Initializes a ComboBox with available languages
        /// </summary>
        public static void InitializeLanguageComboBox(ComboBox comboBox, string currentLanguage)
        {
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
            comboBox.DataSource = new BindingSource(AvailableLanguages, null);

            // Set current language
            for (var i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i] is not KeyValuePair<string, string> item)
                {
                    continue;
                }

                if (item.Key != currentLanguage) continue;
                comboBox.SelectedIndex = i;
                break;
            }
        }

        /// <summary>
        /// Gets the selected language code from a language ComboBox
        /// </summary>
        public static string GetSelectedLanguage(ComboBox comboBox)
        {
            if (comboBox.SelectedItem is KeyValuePair<string, string> kv)
            {
                return kv.Key;
            }

            return "sk-SK";
        }

        /// <summary>
        /// Applies language changes to all open forms
        /// </summary>
        public static void ApplyLanguageChange(string languageCode)
        {
            LocalizationManager.SetLanguage(languageCode);
        }
    }
}