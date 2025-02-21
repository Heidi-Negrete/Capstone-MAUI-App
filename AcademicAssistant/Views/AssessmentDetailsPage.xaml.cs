using AcademicAssistant.Core.ViewModels;

namespace AcademicAssistant.Views;

[QueryProperty(nameof(AssessmentId), "id")]
public partial class AssessmentDetailsPage : ContentPage
{
    private readonly AssessmentDetailsViewModel _viewModel;
    private int assessmentId;

    public int AssessmentId
    {
        get => assessmentId;
        set
        {
            assessmentId = value;
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
        _viewModel.LoadData(AssessmentId);
    }
}