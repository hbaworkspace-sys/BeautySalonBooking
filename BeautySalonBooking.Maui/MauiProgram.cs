using BeautySalonBooking.Maui.Common.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BeautySalonBooking.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("IRANSans.ttf", "Sans");
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialIconsOut");
            });

        builder.Services.AddApiServices();
        builder.Services.AddPresentation();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}