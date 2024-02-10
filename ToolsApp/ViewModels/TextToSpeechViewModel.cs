using CommunityToolkit.Maui.Alerts;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.ViewModels
{
    public class TextToSpeechViewModel : BaseViewModel
    {
        #region Fields

        private CultureInfo culture = CultureInfo.InvariantCulture;

        #endregion

        #region Properties

        private float pitch;
        private float volume;
        private string text;
        private string cultureName;
        private ImageSource source;

        public string CultureName
        {
            get => cultureName;
            set => SetProperty(ref cultureName, value);
        }

        public ImageSource SourceButton
        {
            get => source;
            set => SetProperty(ref source, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public float Volume
        {
            get => volume;
            set => SetProperty(ref volume, value);
        }

        public float Pitch
        {
            get => pitch;
            set => SetProperty(ref pitch, value);
        }

        #endregion

        #region Commands

        public Command ListenCommand { get; }
        public Command DisplayLanguageCommand { get; }
        public Command CopyTextCommand { get; }

        #endregion

        #region Constructor

        public TextToSpeechViewModel(string? text)
        {
            Text = text;
            SourceButton = "listenplay.svg";
            ListenCommand = new Command(OnListen);
            DisplayLanguageCommand = new Command(OnDisplayLanguage);
            CopyTextCommand = new Command(OnCopyText);
            Initialise();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes view model properties.
        /// </summary>
        private async void Initialise()
        {
            try
            {
                string result = await SecureStorage.GetAsync("Pitch");
                if (float.TryParse(result, out float pitchValue))
                    Pitch = pitchValue;

                result = await SecureStorage.GetAsync("Volume");
                if (float.TryParse(result, out float volumeValue))
                    Volume = volumeValue;

                result = await SecureStorage.GetAsync("Culture");
                if (!string.IsNullOrEmpty(result))
                {
                    culture = CultureInfo.GetCultureInfo(result);
                    CultureName = culture.DisplayName;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error during initialization: {ex.Message}");
            }
        }

        /// <summary>
        /// Begins text-to-speech playback.
        /// </summary>
        private async void OnListen()
        {
            try
            {
                if (string.IsNullOrEmpty(Text))
                {
                    await Shell.Current.DisplaySnackbar("Text empty");
                    return;
                }

                SourceButton = "listenpause.svg";
                IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();
                string cultureLanguageCode = culture.TwoLetterISOLanguageName;
                Locale desiredLocale = locales.FirstOrDefault(l => l.Language == cultureLanguageCode);
                SpeechOptions options = new SpeechOptions()
                {
                    Pitch = Pitch,
                    Volume = Volume,
                    Locale = desiredLocale
                };
                await TextToSpeech.Default.SpeakAsync(Text, options);

                await SecureStorage.SetAsync("Pitch", Pitch.ToString());
                await SecureStorage.SetAsync("Volume", Volume.ToString());
                SourceButton = "listenplay.svg";
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error during text-to-speech playback: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays a language selection dialog.
        /// </summary>
        private async void OnDisplayLanguage()
        {
            try
            {
                var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

                var uniqueLanguageCodes = allCultures
                    .Select(c => c.DisplayName)
                    .Distinct()
                    .ToArray();

                var action = await Shell.Current.DisplayActionSheet("Langue", null, null, uniqueLanguageCodes);

                if (!string.IsNullOrEmpty(action))
                {
                    var selectedCulture = allCultures.FirstOrDefault(c => c.DisplayName.Equals(action));

                    if (selectedCulture != null)
                    {
                        await SecureStorage.SetAsync("Culture", selectedCulture.TwoLetterISOLanguageName);

                        culture = selectedCulture;
                        CultureName = selectedCulture.DisplayName;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error during language selection: {ex.Message}");
            }
        }

        /// <summary>
        /// Copies the text to clipboard.
        /// </summary>
        private async void OnCopyText()
        {
            try
            {
                if (string.IsNullOrEmpty(Text))
                {
                    await Shell.Current.DisplaySnackbar("Text empty");
                    return;
                }
                await Clipboard.Default.SetTextAsync(Text);
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error during text copy: {ex.Message}");
            }
        }

        #endregion
    }
}