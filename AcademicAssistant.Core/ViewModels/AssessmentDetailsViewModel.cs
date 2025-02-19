using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Core.ViewModels;

public partial class AssessmentDetailsViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    
    
    [ObservableProperty] private Assessment _assessment;

    [ObservableProperty] private List<AcademicStatus.Status> _assessmentStatusList;
    
    public AssessmentDetailsViewModel(IRepository repository)
    {
        _repository = repository;
    }

    public async void LoadData(int AssessmentId)
    {
        Assessment = await _repository.GetAssessmentById(AssessmentId);
        AssessmentStatusList = Enum.GetValues(typeof(AcademicStatus.Status)).Cast<AcademicStatus.Status>().ToList();
    }
}
