using System.Collections.ObjectModel;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AcademicAssistant.Core.ViewModels;


public partial class TermDetailsViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    
    
    [ObservableProperty] private Term _term;
    
    [ObservableProperty] private bool _courseSelected;
    
    [ObservableProperty] private Course _selectedCourse;
    
    [ObservableProperty] private ObservableCollection<Course> _courses;
    
    public TermDetailsViewModel(IRepository repository)
    {
        _repository = repository;
    }

    public async void LoadData(int TermId)
    {
        Term = await _repository.GetTermById(TermId);
        Courses = new ObservableCollection<Course>(await _repository.GetCoursesByTermId(TermId));
        CourseSelected = false;
    }
    
    [RelayCommand]
    public void DeleteCourse()
    {
        if (SelectedCourse == null) return;
        _repository.DeleteCourse(SelectedCourse.Id);
        Courses.Remove(SelectedCourse);
        SelectionChanged(null); // On Android if swipeview used to delete, selectionchanged does not fire
    }
    
    [RelayCommand]
    public async void AddCourse()
    
    {
        _repository.AddCourse(Term.Id);
        Courses = new ObservableCollection<Course>(await _repository.GetCoursesByTermId(Term.Id));
    }

    [RelayCommand]
    public void SelectionChanged(Course? course)
    {
        if (course == null) CourseSelected = false;
        else
        {
            CourseSelected = true;
        }
    }

    [RelayCommand]
    public async void ViewSelectedCourse()
    {
        if (SelectedCourse == null) return;
        await Shell.Current.GoToAsync($"course?id={SelectedCourse.Id}");
    }
}
