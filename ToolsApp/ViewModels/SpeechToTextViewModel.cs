using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.ViewModels
{
    public class SpeechToTextViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the result of the speech-to-text conversion.
        /// </summary>
        private string result;

        /// <summary>
        /// Gets or sets the color of the microphone icon.
        /// </summary>
        private Color colorMicro;

        public Color ColorMicro
        {
            get => colorMicro;
            set => SetProperty(ref colorMicro, value);
        }

        public string Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        #endregion Properties

        #region Commands

        /// <summary>
        /// Command to share the speech-to-text result.
        /// </summary>
        public Command ShareCommand { get; }

        /// <summary>
        /// Command to initiate the speech-to-text listening process.
        /// </summary>
        public Command ListenCommand { get; }

        /// <summary>
        /// Command to copy the speech-to-text result to the clipboard.
        /// </summary>
        public Command CopyTextCommand { get; }

        /// <summary>
        /// Command to open the text-to-speech functionality.
        /// </summary>
        public Command OpenTextToSpeechCommand { get; }

        #endregion Commands

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeechToTextViewModel"/> class.
        /// </summary>
        public SpeechToTextViewModel()
        {
            // Set default color for the microphone icon
            ColorMicro = Colors.WhiteSmoke;

            // Initialize commands
            ShareCommand = new Command(OnShare);
            ListenCommand = new Command(OnListen);
            CopyTextCommand = new Command(OnCopyText);
            OpenTextToSpeechCommand = new Command(OnOpenTextToSpeech);
        }

        #endregion Constructor

        #region Speech-to-Text Operations

        /// <summary>
        /// Initiates the speech-to-text listening process.
        /// </summary>
        async Task Listen(CancellationToken cancellationToken)
        {
            // Request necessary permissions for speech-to-text
            var isGranted = await SpeechToText.RequestPermissions(cancellationToken);

            if (!isGranted)
            {
                await Toast.Make("Permission not granted").Show(CancellationToken.None);
                return;
            }

            // Perform speech-to-text listening
            var recognitionResult = await SpeechToText.ListenAsync(
                                        CultureInfo.GetCultureInfo("fr-FR"),
                                        new Progress<string>(partialText =>
                                        {
                                            Result = "En train d'ecrire votre texte ...";
                                        }), cancellationToken);

            // Handle the result of speech-to-text
            if (recognitionResult.IsSuccessful)
            {
                Result = recognitionResult.Text;
            }
            else
            {
                await Toast.Make(recognitionResult.Exception?.Message ?? "Unable to recognize speech").Show(CancellationToken.None);
            }
        }

        #endregion Speech-to-Text Operations

        #region Command Handlers

        private async void OnListen()
        {
            // Change microphone icon color during speech-to-text listening
            ColorMicro = Colors.Orange;

            // Initiate speech-to-text listening
            await Listen(CancellationToken.None);

            // Reset microphone icon color
            ColorMicro = Colors.WhiteSmoke;
        }

        private async void OnCopyText()
        {
            if (string.IsNullOrEmpty(Result))
            {
                await Shell.Current.DisplaySnackbar("Text Empty");
                return;
            }

            await Clipboard.Default.SetTextAsync(Result);

            // Display a snackbar message indicating successful copy
            await Shell.Current.DisplaySnackbar("Copy to clipboard");
        }

        private async void OnShare()
        {
            if (string.IsNullOrEmpty(Result))
            {
                await Shell.Current.DisplaySnackbar("Text Empty");
                return;
            }

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = Result,
                Title = "Text with ToolBox App"
            });
        }

        private async void OnOpenTextToSpeech()
        {
            // Check if the speech-to-text result is empty
            if (string.IsNullOrEmpty(Result))
            {
                await Shell.Current.DisplaySnackbar("Text Empty");
                return;
            }

            // Navigate to the text-to-speech page with the speech-to-text result as a parameter
            var navigationParameter = new Dictionary<string, object>()
        {
            { "text", Result }
        };

            await Shell.Current.GoToAsync("textToSpeech", navigationParameter);
        }

        #endregion Command Handlers
    }

}
