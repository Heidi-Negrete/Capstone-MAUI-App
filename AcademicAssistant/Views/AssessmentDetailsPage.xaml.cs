using AcademicAssistant.Core.Converters;
using AcademicAssistant.Core.ViewModels;
using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Views;

[QueryProperty(nameof(AssessmentArgs), "assessment")]
public partial class AssessmentDetailsPage : ContentPage
{
    private readonly AssessmentDetailsViewModel _viewModel;

    private AssessmentNavigator assessmentArgs;
    public AssessmentNavigator AssessmentArgs
    {
        get => assessmentArgs;
        set
        {
            var id = value.Id;
            var type = value.Type;
            assessmentArgs = new AssessmentNavigator
            {
                Id = id,
                Type = type
            };
        }
    }
    public AssessmentDetailsPage(AssessmentDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.LoadData(AssessmentArgs.Id, AssessmentArgs.Type);
    }
}