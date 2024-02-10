using ToolsApp.ViewModels;

namespace ToolsApp.Views;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage()
	{
		InitializeComponent();
        BindingContext = new CalculatorViewModel(grid);
    }

}