using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using System.Globalization;
using System.Threading;
using System;
using Microsoft.Maui.Media;
using ToolsApp.ViewModels;

namespace ToolsApp.Views;

public partial class TextToSpeechPage : ContentPage, IQueryAttributable
{
    public TextToSpeechPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("text"))
        {
            BindingContext = new TextToSpeechViewModel(query["text"].ToString());
        }
        else
        {
            BindingContext = new TextToSpeechViewModel(null);
        }
    }
}