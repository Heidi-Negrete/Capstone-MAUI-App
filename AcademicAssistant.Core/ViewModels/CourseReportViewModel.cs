using System.Collections.ObjectModel;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AcademicAssistant.Core.ViewModels;

public partial class CourseReportViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    [ObservableProperty] private int _completedCourses;
    [ObservableProperty] private int _totalCourses;
    [ObservableProperty] private float _courseProgressPercentage = 0;
    [ObservableProperty] private bool _toggleOn = true;
    [ObservableProperty] private ObservableCollection<Course> _listedCourses;
    private List<Course> _courses;

    
    public CourseReportViewModel(IRepository repository)
    {
        _repository = repository;
    }
    public async Task LoadData(bool toggled)
    {
        ToggleOn = toggled;
        if (ToggleOn)
        {
            _courses = await _repository.GetCourses();
            _courses = _courses.OrderBy(c => c.Status).ToList();
            ListedCourses = new ObservableCollection<Course>(_courses);
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
        }
        else
        {
            await Shell.Current.GoToAsync("assessmentreport");
        }
    }
    
    [RelayCommand]
    public async Task ToggleReport()
    {
        await Shell.Current.GoToAsync("assessmentreport?toggle=false");
    }

}