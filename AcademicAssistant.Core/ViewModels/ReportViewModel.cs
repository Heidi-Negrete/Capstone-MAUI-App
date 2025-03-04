using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Core.ViewModels;

public partial class ReportViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    [ObservableProperty] private int _completedCourses;
    [ObservableProperty] private int _totalCourses;
    [ObservableProperty] private float _courseProgressPercentage = 0;
    private List<Course> _courses;
    [ObservableProperty] private int _completedAssessments;
    [ObservableProperty] private int _totalAssessments;
    [ObservableProperty] private float _assessmentProgressPercentage = 0;
    private List<Assessment> _assessments;
    
    public ReportViewModel(IRepository repository)
    {
        _repository = repository;
    }
    public async Task LoadData()
    {
        // Courses
        _courses = await _repository.GetCourses();
        TotalCourses = _courses.Count;
        CompletedCourses = _courses.Count(c => c.Status == AcademicStatus.Status.Completed);
        if (TotalCourses > 0)
        {
            CourseProgressPercentage = (float)CompletedCourses / TotalCourses;
        }
        else
        {
            CourseProgressPercentage = 0;
        }
        
        // Assessments
        _assessments = await _repository.GetAssessments();
        TotalAssessments = _assessments.Count;
        CompletedAssessments = _assessments.Count(a => a.Status == AcademicStatus.Status.Completed);
        if (TotalAssessments > 0)
        {
            AssessmentProgressPercentage = (float)CompletedAssessments / TotalAssessments;
        }
        else
        {
            AssessmentProgressPercentage = 0;
        }
    }
}