using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AcademicAssistant.Core.ViewModels;

public partial class AssessmentDetailsViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    
    
    [ObservableProperty] private Assessment _assessment;

    [ObservableProperty] private List<AcademicStatus.Status> _assessmentStatusList;
    
    [ObservableProperty] private bool _dataChanged = false; // Used to indicate whether 'Save' button should be shown
    [ObservableProperty] private bool _changesValid = true; // Used to indicate whether 'Save' button should be enabled
    
    public AssessmentDetailsViewModel(IRepository repository)
    {
        _repository = repository;
    }

    public async void LoadData(int AssessmentId)
    {
        Assessment = await _repository.GetAssessmentById(AssessmentId);
        AssessmentStatusList = Enum.GetValues(typeof(AcademicStatus.Status)).Cast<AcademicStatus.Status>().ToList();
        DataChanged = false;
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
            await _repository.UpdateAssessment(Assessment.Id, Assessment);
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
        if (!DataChanged) await Shell.Current.GoToAsync($"course?id={Assessment.CourseId}");
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
            await Shell.Current.GoToAsync($"course?id={Assessment.CourseId}");
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
        if (Assessment.StartDate > Assessment.EndDate)
        {
            ChangesValid = false;
            await Shell.Current.DisplayAlert("Invalid Dates", "Please ensure your start date is before your end date.", "OK");
            return;
        }
    }
}
