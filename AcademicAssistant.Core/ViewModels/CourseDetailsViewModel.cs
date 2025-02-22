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

    [ObservableProperty] private bool _assessmentSelected = false;
    
    [ObservableProperty] private Assessment _selectedAssessment;
    
    [ObservableProperty] private ObservableCollection<Assessment> _assessments;
    
    [ObservableProperty] private bool _dataChanged = false; // Used to indicate whether 'Save' button should be shown
    [ObservableProperty] private bool _changesValid = true; // Used to indicate whether 'Save' button should be enabled
    
    public CourseDetailsViewModel(IRepository repository)
    {
        _repository = repository;
    }

    public async void LoadData(int CourseId)
    {
        Course = await _repository.GetCourseById(CourseId);
        Assessments = new ObservableCollection<Assessment>(await _repository.GetAssessmentsByCourseId(CourseId));
        CourseStatusList = Enum.GetValues(typeof(AcademicStatus.Status)).Cast<AcademicStatus.Status>().ToList();
        DataChanged = false;
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

    [RelayCommand]
    public async Task ShareNotes(string text)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = $"{Course.Title} Notes"
        });
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
            await _repository.UpdateCourse(Course.Id, Course);
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Failed to save changes.", e.Message, "OK");
        }
        DataChanged = false;
    }
    
    [RelayCommand]
    public async void AskUserToSaveChanges()
    {
        if (!DataChanged) await Shell.Current.GoToAsync($"term?id={Course.TermId}");
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
            await Shell.Current.GoToAsync($"term?id={Course.TermId}");
        }
    }

    [RelayCommand]
    public async Task ConfirmDataChanged()
    {
        DataChanged = true;
        await ValidateData();
    }
    
    private async Task ValidateData()
    {
        if (Course.StartDate > Course.EndDate)
        {
            ChangesValid = false;
            await Shell.Current.DisplayAlert("Invalid Dates", "Please ensure your start date is before your end date.", "OK");
            return;
        }
    }
}
