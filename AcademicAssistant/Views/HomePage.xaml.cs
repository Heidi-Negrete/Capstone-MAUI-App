using AcademicAssistant.Core.ViewModels;
using AcademicAssistant.Repositories;

namespace AcademicAssistant.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }
}