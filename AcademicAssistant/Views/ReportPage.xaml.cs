using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicAssistant.Core.ViewModels;

namespace AcademicAssistant.Views;

public partial class ReportPage : ContentPage
{
    private readonly ReportViewModel _viewModel;
    
    public ReportPage(ReportViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.LoadData();
    }
}