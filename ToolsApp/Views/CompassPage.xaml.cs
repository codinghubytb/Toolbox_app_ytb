using Microsoft.Maui.Controls;
using ToolsApp.ViewModels;

namespace ToolsApp.Views;

    public partial class CompassPage : ContentPage
    {
        CompassViewModel viewModel { get; set; }

        public CompassPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CompassViewModel();
          
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Arr�tez la surveillance de la boussole lorsque la page dispara�t
            viewModel.StopCompassUpdates();
        }

    }
