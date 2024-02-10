using ToolsApp.ViewModels;
using ToolsApp.Views;

namespace ToolsApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("calculator", typeof(CalculatorPage));
            Routing.RegisterRoute("compass", typeof(CompassPage));
            Routing.RegisterRoute("converter", typeof(ConverterPage));
            Routing.RegisterRoute("textToSpeech", typeof(TextToSpeechPage));
            Routing.RegisterRoute("speechToText", typeof(SpeechToTextPage));
        }
    }
}