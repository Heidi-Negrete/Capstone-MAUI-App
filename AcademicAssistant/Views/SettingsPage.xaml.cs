using System.Diagnostics;
using AcademicAssistant.Core.ViewModels;


namespace AcademicAssistant.Views;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel _viewModel;
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadStudent();
    }
}