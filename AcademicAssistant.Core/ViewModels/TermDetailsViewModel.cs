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

    [ObservableProperty] private bool _courseSelected = false;
    
    [ObservableProperty] private Course _selectedCourse;
    
    [ObservableProperty] private ObservableCollection<Course> _courses;

    [ObservableProperty] private bool _changesValid = true; // Used to indicate whether 'Save' button should be enabled

    [ObservableProperty] private bool _termDataChanged = false; // Used to indicate whether 'Save' button should be shown
    
    public TermDetailsViewModel(IRepository repository)
    {
        _repository = repository;
    }

    public async void LoadData(int TermId)
    {
        Term = await _repository.GetTermById(TermId);
        Courses = new ObservableCollection<Course>(await _repository.GetCoursesByTermId(TermId));
        TermDataChanged = false;
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
        System.Diagnostics.Trace.WriteLine(Term.CourseCount + " " + Term.MaxCourseCount);
        if (Term.CourseCount == Term.MaxCourseCount)
        {
            await Shell.Current.DisplayAlert("Error", "Max course count reached", "OK");
            return;
        }
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

    [RelayCommand]
    public async void SaveChanges()
    {
        if (!ChangesValid)
        {
            await Shell.Current.DisplayAlert("Error", "Please check all fields before saving.", "OK");
            return;
        }
        try
        {
            await _repository.UpdateTerm(Term.Id, Term);
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Failed to save changes.", e.Message, "OK");
        }
        TermDataChanged = false;
    }
    
    [RelayCommand]
    public async void AskUserToSaveChanges()
    {
        if (!TermDataChanged) await Shell.Current.GoToAsync("//home");
        else
        {
            
            if (ChangesValid)
            {
                var result = await Shell.Current.DisplayAlert("Save Changes", "You have unsaved changes. Would you like to save them?", "Yes", "No");
                if (result)
                {
                    SaveChanges();
                }
            }
            await Shell.Current.GoToAsync("//home");
        }
    }

    [RelayCommand]
    public async void ConfirmTermDataChanged()
    {
        TermDataChanged = true;
        await ValidateData();
    }
    
    private async Task ValidateData()
    {
        if (Term.StartDate > Term.EndDate)
        {
            ChangesValid = false;
            await Shell.Current.DisplayAlert("Invalid Dates", "Please ensure your start date is before your end date.", "OK");
            return;
        }
    }
}
