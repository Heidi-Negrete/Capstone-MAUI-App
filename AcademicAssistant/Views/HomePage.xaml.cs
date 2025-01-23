using AcademicAssistant.Core.ViewModels;
namespace AcademicAssistant.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = new HomeViewModel();
    }
}