using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using System.Globalization;
using ToolsApp.ViewModels;

namespace ToolsApp.Views;

public partial class SpeechToTextPage : ContentPage
{
	public SpeechToTextPage()
    {
        InitializeComponent();
        BindingContext = new SpeechToTextViewModel();
	}

}