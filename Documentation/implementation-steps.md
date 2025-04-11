# PDFPass Multilingual Implementation Steps

Follow these steps to implement multilingual support in your PDFPass application:

## 1. Add Resource Files

Add the following resource files to your project:

1. Create a `Resources` folder if it doesn't exist
2. Add `Strings.sk-SK.resx` for Slovak
3. Add `Strings.en.resx` for English
4. Add `Strings.cs-CZ.resx` for Czech
5. Add an empty `Strings.resx` as the default resource file

## 2. Add Support Classes

Add these classes to your project:

1. `LocalizationManager.cs` - Core utility for resource management
2. `Resources/Strings.cs` - Strongly-typed wrapper for resources
3. `LanguageHelper.cs` - Helper for language selection UI

## 3. Update Settings Class

Modify your Settings.cs file to add language support:

1. Add the `language` property
2. Update the `Load()` method to load the language setting
3. Update the `Save()` method to save the language setting

## 4. Update Program.cs

Modify your Program.cs to initialize localization:

1. Call `Settings.Load()` at the start of `Main()`
2. Call `LocalizationManager.SetLanguage(Settings.language)` before creating forms

## 5. Update FrmSettings

Modify your Settings form to add language selection:

1. Add a GroupBox and ComboBox for language selection to the designer
2. Initialize the language ComboBox in the form load event
3. Save the selected language when the OK button is clicked

## 6. Test and Verify

Test your implementation:

1. Run the application and verify the default language (Slovak) works
2. Change to English and check that all text updates properly
3. Change to Czech and verify Czech translations appear
4. Test various workflows in each language
5. Restart the application to verify language preference is saved

## Implementation Notes

- The initial language selection will be based on what's stored in the registry
- The default language is Slovak ("sk-SK") if no language is saved
- Users can switch between Slovak, English, and Czech from the Settings form
- A message will notify users that a restart is recommended after changing languages
- Some UI updates will apply immediately, but a full restart ensures consistent language display

## Troubleshooting

If you encounter issues:

- Check that all resource files are added with "Embedded Resource" build action
- Verify that `Strings.resx` (empty default file) exists
- Check for missing resource strings - they will appear as `[ResourceKey]` in the UI
- Ensure all forms reference the `PDFPass.Resources` namespace
