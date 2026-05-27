using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Maui.Handlers;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace TaskManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("InriaSans-Bold.ttf", "InriaSansBold");
                    fonts.AddFont("InriaSans-BoldItalic.ttf", "InriaSansBoldItalic");
                    fonts.AddFont("InriaSans-Italic.ttf", "InriaSansItalic");
                    fonts.AddFont("InriaSans-Light.ttf", "InriaSansLight");
                    fonts.AddFont("InriaSans-LightItalic.ttf", "InriaSansLightItalic");
                    fonts.AddFont("InriaSans-Regular.ttf", "InriaSansRegular");
                }).ConfigureMauiHandlers(handlers => {
#if ANDROID
                    // Remove the line underneat any Entry
                    handlers.AddHandler(typeof(Entry), typeof(EntryHandler));
                    EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                    {
                        handler.PlatformView.Background = null;
                    });
#endif

                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
