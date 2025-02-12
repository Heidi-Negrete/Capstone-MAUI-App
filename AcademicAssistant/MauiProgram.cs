using AcademicAssistant.Core.ViewModels;
using AcademicAssistant.Repositories;
using AcademicAssistant.Views;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace AcademicAssistant;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        // repository for dependency injection
        builder.Services.AddSingleton<IRepository, HardCodedRepository>();
        
        // Views
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddTransient<TermDetailsPage>();
        
        // ViewModels
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddTransient<TermDetailsViewModel>();

        return builder.Build();
    }
}
