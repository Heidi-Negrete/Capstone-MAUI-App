# Developer Guide
This application is built with C# using the .NET MAUI framework. The project is setup to implement the MVVM (Models, View, View-Models) design pattern.

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [JetBrains Rider](https://www.jetbrains.com/rider/)
- [Android Emulator](https://learn.microsoft.com/en-us/visualstudio/emulator/overview?view=vs-2022) or [iOS Simulator](https://learn.microsoft.com/en-us/xamarin/ios/get-started/installation/device-simulator?tabs=macos) if you wish to test for mobile devices, otherwise Windows 11.

## Setup
1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio or JetBrains Rider.
3. Build the solution to restore all NuGet packages and dependencies.
4. Run the application on your desired platform (Windows, Android, or iOS).

## Project Structure
The solution is split into four projects:  
**AcademicAssistant**: The main application project that contains the main entry point and pages.  
**AcademicAssistant.Core**: Contains the ViewModels, Services, and Converters, and business logic.  
**AcademicAssistant.Repositories**: Contains the data access layer and models.  
**AcademicAssistant.Tests**: Contains the unit tests for the application.  

Here are some of the key files and directories for the project:
- [AppShell.xaml](../AcademicAssistant/AppShell.xaml) The main shell of the application that defines the navigation structure.
- [AppShell.xaml.cs](../AcademicAssistant/AppShell.xaml.cs) The code-behind file for the AppShell.xaml, routes to pages not defined in the flyout tab menu.
- [MauiProgram.cs](../AcademicAssistant/MauiProgram.cs) The main entry point of the application, where services and dependencies are registered.
- [Views](../AcademicAssistant/Views) Contains all the XAML pages for the application.
- [ViewModels](../AcademicAssistant.Core/ViewModels) Contains the ViewModel classes that handle the logic and data binding for the views.
- [Models](../AcademicAssistant.Repositories/Models) Contains the data models for data access.
- [SQLiteRepository.cs](../AcademicAssistant.Repositories/SQLiteRepository.cs) Contains the SQLite database repository for data access.
- [NotificationManager](../AcademicAssistant.Core/NotificationManager.cs) Contains the logic for managing notifications.

#### Packages
- [.NET MAUI Community Toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/)
- [DatePicker](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/datepicker?view=net-maui-8.0)
- [.NET MAUI Local Databases](https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0#install-sqlitepclrawbundle_green)
- [Picker](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/picker?view=net-maui-8.0)
- [Plugin.LocalNotification](https://github.com/thudugala/Plugin.LocalNotification)
- [Share](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/data/share?view=net-maui-8.0&tabs=windows)
- [SQL tite-net](https://github.com/praeclarum/sqlite-net)
- [xUnit](https://xunit.net/)

#### External Documentation
- [MVVM](https://learn.microsoft.com/en-us/dotnet/maui/architecture/mvvm?view=net-maui-8.0)
- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/?view=net-maui-8.0)
- [.NET MAUI Community Toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/)