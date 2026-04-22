using Microsoft.Extensions.Logging;

namespace TaskManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("InriaSans-Bold.ttf", "InriaSansBold");
                    fonts.AddFont("InriaSans-BoldItalic.ttf", "InriaSansBoldItalic");
                    fonts.AddFont("InriaSans-Italic.ttf", "InriaSansItalic");
                    fonts.AddFont("InriaSans-Light.ttf", "InriaSansLight");
                    fonts.AddFont("InriaSans-LightItalic.ttf", "InriaSansLightItalic");
                    fonts.AddFont("InriaSans-Regular.ttf", "InriaSansRegular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
