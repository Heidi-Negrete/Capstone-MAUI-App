using AcademicAssistant.Core.ViewModels;

namespace AcademicAssistant.Views;

[QueryProperty(nameof(TermId), "id")]
public partial class TermDetailsPage : ContentPage
{
	private readonly TermDetailsViewModel _viewModel;
	private int termId;

	public int TermId
	{
		get => termId;
		set
		{
			termId = value;
			_viewModel.LoadData(value);
		}
	}
	public TermDetailsPage(TermDetailsViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = _viewModel;
	}
}