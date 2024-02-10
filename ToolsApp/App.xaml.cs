using ToolsApp.Views;

namespace ToolsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Initialise();
        }

        async void Initialise()
        {
            var result = await SecureStorage.GetAsync("Volume");
            if (result == null)
            {
                await SecureStorage.SetAsync("Volume", "0,5");
            }
            result = await SecureStorage.GetAsync("Pitch");
            if (result == null)
            {
                await SecureStorage.SetAsync("Pitch", "0,8");
            }
            result = await SecureStorage.GetAsync("Culture");
            if (result == null)
            {
                await SecureStorage.SetAsync("Culture", "fr");
            }
        }


        public static void HandleAppActions(AppAction appAction)
        {
            App.Current.Dispatcher.Dispatch(async () =>
            {
                var page = appAction.Id switch
                {
                    "calculator" => new CalculatorPage(),
                    "unit_convert" => new ConverterPage(),
                    "compass" => new CompassPage(),
                    _ => default(Page)
                };

                if (page != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(page);
                }
            });
        }

    }
}