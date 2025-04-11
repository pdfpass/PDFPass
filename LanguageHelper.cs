using System.Collections.Generic;
using System.Windows.Forms;

namespace PDFPass
{
    public static class LanguageHelper
    {
        public static Dictionary<string, string> AvailableLanguages => new Dictionary<string, string>
        {
            { "sk-SK", "Slovenčina" },
            { "en", "English" },
            { "cs-CZ", "Čeština" }
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
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                var item = (KeyValuePair<string, string>)comboBox.Items[i];
                if (item.Key == currentLanguage)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the selected language code from a language ComboBox
        /// </summary>
        public static string GetSelectedLanguage(ComboBox comboBox)
        {
            if (comboBox.SelectedItem != null)
            {
                return ((KeyValuePair<string, string>)comboBox.SelectedItem).Key;
            }

            return "sk-SK"; // Default to Slovak
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