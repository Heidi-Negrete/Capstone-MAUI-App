using AcademicAssistant.Core.Managers;
using AcademicAssistant.Core.ViewModels;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using AcademicAssistant.Views;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Plugin.LocalNotification;

namespace AcademicAssistant;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
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
        builder.Services.AddTransient<CourseDetailsPage>();
        builder.Services.AddTransient<AssessmentDetailsPage>();
        
        // ViewModels
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddTransient<TermDetailsViewModel>();
        builder.Services.AddTransient<CourseDetailsViewModel>();
        builder.Services.AddTransient<AssessmentDetailsViewModel>();
        
        // Notification Manager
        builder.Services.AddSingleton<NotificationManager>();

        return builder.Build();
    }
}
