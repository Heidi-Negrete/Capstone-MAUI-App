using AcademicAssistant.Views;

namespace AcademicAssistant;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //MainPage = new AppShell();
        MainPage = new HomePage();
    }
}
