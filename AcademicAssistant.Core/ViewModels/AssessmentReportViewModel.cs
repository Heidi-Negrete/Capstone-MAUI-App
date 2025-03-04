using System.Collections.ObjectModel;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AcademicAssistant.Core.ViewModels;


public partial class AssessmentReportViewModel : ObservableObject
{
    private readonly IRepository _repository;
    [ObservableProperty] private int _completedAssessments;
    [ObservableProperty] private int _totalAssessments;
    [ObservableProperty] private float _assessmentProgressPercentage = 0;
    [ObservableProperty] private ObservableCollection<Assessment> _listedAssessments;
    [ObservableProperty] private bool _toggleOn = false;
    private List<Assessment> _assessments;
    
    public AssessmentReportViewModel(IRepository repository)
    {
        _repository = repository;
    }

    public async Task LoadData(bool toggled)
    {
        ToggleOn = toggled;
        if (!ToggleOn)
        {
            _assessments = await _repository.GetAssessments();
            _assessments = _assessments.OrderBy(c => c.EndDate).ToList();
            ListedAssessments = new ObservableCollection<Assessment>(_assessments);
            TotalAssessments = _assessments.Count;
            CompletedAssessments = _assessments.Count(c => c.Status == AcademicStatus.Status.Completed);
            if (TotalAssessments > 0)
            {
                AssessmentProgressPercentage = (float)CompletedAssessments / TotalAssessments;
            }
            else
            {
                AssessmentProgressPercentage = 0;
            }
        }
        else
        {
            await Shell.Current.GoToAsync("//progress");
        }
    }

    [RelayCommand]
    public async Task ToggleReport()
    {
        await Shell.Current.GoToAsync("//progress?toggle=true");
    }
    
    [RelayCommand]
    public async Task NavigateHome()
    {
        await Shell.Current.GoToAsync("//home");
    }
}