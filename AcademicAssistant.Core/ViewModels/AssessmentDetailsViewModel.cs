using System;
using System.Collections.ObjectModel;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Core.ViewModels;

public partial class AssessmentDetailsViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    
    
    [ObservableProperty] private Assessment _assessment;

    [ObservableProperty] private List<Enum> _assessmentStatusList;

    [ObservableProperty] private string _assessmentStatus;

    [ObservableProperty] private DateTime _startDate;

    [ObservableProperty] private DateTime _endDate;

    [ObservableProperty] private string _about;
    
    public AssessmentDetailsViewModel(IRepository repository)
    {
        _repository = repository;
    }

    public async void LoadData(int AssessmentId)
    {
        Assessment = await _repository.GetAssessmentById(AssessmentId);
        //AssessmentStatusList = Enum.GetValues(typeof(AcademicStatus)).Cast<AcademicStatus>().ToList();
        AssessmentStatusList = new List<Enum>
        {
            AcademicStatus.Status.InProgress,
            AcademicStatus.Status.Completed,
            AcademicStatus.Status.Failed,
            AcademicStatus.Status.Dropped,
            AcademicStatus.Status.Planned
        };
        AssessmentStatus = Assessment.Status.ToString();
        About = Assessment.About;
        StartDate = Assessment.StartDate;
        EndDate = Assessment.EndDate;
    }
}
