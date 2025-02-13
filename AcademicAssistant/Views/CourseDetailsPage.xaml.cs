using AcademicAssistant.Core.ViewModels;

namespace AcademicAssistant.Views;

[QueryProperty(nameof(CourseId), "id")]
public partial class CourseDetailsPage : ContentPage
{
	private readonly CourseDetailsViewModel _viewModel;
	private int courseId;

	public int CourseId
	{
		get => courseId;
		set
		{
			courseId = value;
			_viewModel.LoadData(value);
		}
	}
	public CourseDetailsPage(CourseDetailsViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = _viewModel;
	}
}