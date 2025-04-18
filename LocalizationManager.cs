using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace PDFPass
{
    /// <summary>
    /// Provides centralized access to localized resources
    /// </summary>
    public static class LocalizationManager
    {
        public static readonly ResourceManager ResourceManager;
        private static CultureInfo _currentCulture;

        // Define the event using EventHandler
        public static event EventHandler LanguageChanged;

        /// <summary>
        /// Static constructor to initialize the ResourceManager
        /// </summary>
        static LocalizationManager()
        {
            ResourceManager = new ResourceManager("PDFPass.Resources.Strings", typeof(LocalizationManager).Assembly);
            // Default to English
            SetLanguage("en-EN");
        }


        // Method to raise the event when language changes
        private static void OnLanguageChanged()
        {
            // Safely invoke the event (checking for null to avoid null reference exceptions)
            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Sets the language for the application
        /// </summary>
        /// <param name="language">Culture name (e.g., "sk-SK", "en-US")</param>
        public static void SetLanguage(string language)
        {
            _currentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = _currentCulture;
            Thread.CurrentThread.CurrentCulture = _currentCulture;

            // Notify subscribers that language has changed
            OnLanguageChanged();
        }

        /// <summary>
        /// Gets a localized string for the specified key
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <returns>Localized string</returns>
        public static string GetString(string key)
        {
            try
            {
                var value = ResourceManager.GetString(key, _currentCulture);
                if (!string.IsNullOrEmpty(value)) return value;
                // If the resource is missing, return the key in a format that makes it obvious
                Console.WriteLine($@"WARNING: Missing resource key: {key}");
                return $"[{key}]";
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error retrieving resource {key}: {ex.Message}");
                return $"[{key}]";
            }
        }

        /// <summary>
        /// Gets a formatted localized string
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Formatted localized string</returns>
        public static string GetFormattedString(string key, params object[] args)
        {
            try
            {
                var format = GetString(key);
                return string.Format(format, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error formatting resource {key}: {ex.Message}");
                return $"[{key}]";
            }
        }
    }
}