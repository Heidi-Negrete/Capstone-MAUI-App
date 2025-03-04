using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicAssistant.Core.ViewModels;

namespace AcademicAssistant.Views;

[QueryProperty(nameof(Toggled), "toggle")]
public partial class AssessmentReportPage : ContentPage
{
    private readonly AssessmentReportViewModel _viewModel;
    private bool toggled = false;

    public bool Toggled
    {
        get => toggled;
        set
        {
            toggled = value;
        }
    }
    
    public AssessmentReportPage(AssessmentReportViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.LoadData(Toggled);
    }
}