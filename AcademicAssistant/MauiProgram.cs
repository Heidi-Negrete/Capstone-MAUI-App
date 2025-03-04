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
        builder.Services.AddSingleton<IRepository, SQLiteRepository>();
        
        // Views
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<TermDetailsPage>();
        builder.Services.AddSingleton<CourseDetailsPage>();
        builder.Services.AddSingleton<AssessmentDetailsPage>();
        builder.Services.AddSingleton<ReportPage>();
        builder.Services.AddSingleton<AssessmentReportPage>();
        
        // ViewModels
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<TermDetailsViewModel>();
        builder.Services.AddSingleton<CourseDetailsViewModel>();
        builder.Services.AddSingleton<AssessmentDetailsViewModel>();
        builder.Services.AddSingleton<CourseReportViewModel>();
        builder.Services.AddSingleton<AssessmentReportViewModel>();

        // Notification Manager
        builder.Services.AddSingleton<NotificationManager>();

        return builder.Build();
    }
}
