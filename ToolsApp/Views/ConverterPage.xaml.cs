using ToolsApp.ViewModels;

namespace ToolsApp.Views;

public partial class ConverterPage : ContentPage
{
	public ConverterPage()
	{
		InitializeComponent();
        BindingContext = new ConverterViewModel();
        arrowbtn.Text = "\ue5c4";
    }
}