using ToolsApp.ViewModels;

namespace ToolsApp.Views;

public partial class SectionPage : ContentPage
{
	public SectionPage()
	{
		InitializeComponent();
        BindingContext = new SectionViewModel();
    }
}