using System.Collections.ObjectModel;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AcademicAssistant.Core.ViewModels;

public partial class CourseDetailsViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    
    
    [ObservableProperty] private Course _course;
    
    [ObservableProperty] private List<AcademicStatus.Status> _courseStatusList;
    
    [ObservableProperty] private bool _assessmentSelected;
    
    [ObservableProperty] private Assessment _selectedAssessment;
    
    [ObservableProperty] private ObservableCollection<Assessment> _assessments;
    
    public CourseDetailsViewModel(IRepository repository)
    {
        _repository = repository;
    }

    public async void LoadData(int CourseId)
    {
        Course = await _repository.GetCourseById(CourseId);
        Assessments = new ObservableCollection<Assessment>(await _repository.GetAssessmentsByCourseId(CourseId));
        AssessmentSelected = false;
        CourseStatusList = Enum.GetValues(typeof(AcademicStatus.Status)).Cast<AcademicStatus.Status>().ToList();
    }
    
    [RelayCommand]
    public void SelectionChanged(Assessment? assessment)
    {
        if (assessment == null) AssessmentSelected = false;
        else
        {
            AssessmentSelected = true;
        }
    }

    [RelayCommand]
    public async void ViewSelectedAssessment()
    {
        if (SelectedAssessment == null) return;
        await Shell.Current.GoToAsync($"assessment?id={SelectedAssessment.Id}");
    }
}
