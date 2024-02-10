using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ToolsApp.ViewModels;
using ToolsApp.Views;

namespace ToolsApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("NotoSerif-Bold.ttf", "NotoSerifBold");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemibold");
                    fonts.AddFont("Poppins-Regular.ttf", "Poppins");
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", "Material");
                })
                .ConfigureEssentials(essentials =>
                 {
                     essentials
                         .AddAppAction("unit_convert", "Unit Convert", icon: "icon_convert")
                         .AddAppAction("compass", "Compass", icon: "icon_compass")
                         .AddAppAction("calculator", "Calculator", icon: "icon_calculator")
                         .OnAppAction(App.HandleAppActions);
                 });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IUnitConvert, UnitConvert>();
            mauiAppBuilder.Services.AddTransient<ConverterViewModel>();
            mauiAppBuilder.Services.AddTransient<CalculatorViewModel>();
            mauiAppBuilder.Services.AddTransient<TextToSpeechViewModel>();
            mauiAppBuilder.Services.AddTransient<SpeechToTextViewModel>();
            mauiAppBuilder.Services.AddTransient<SectionViewModel>();
            mauiAppBuilder.Services.AddTransient<CompassViewModel>();
            mauiAppBuilder.Services.AddTransient<CompassPage>();
            mauiAppBuilder.Services.AddTransient<ConverterPage>();
            mauiAppBuilder.Services.AddTransient<TextToSpeechPage>();
            mauiAppBuilder.Services.AddTransient<SpeechToTextPage>();
            mauiAppBuilder.Services.AddTransient<SectionPage>();
            mauiAppBuilder.Services.AddTransient<CalculatorPage>();


            return mauiAppBuilder;
        }
    }
}