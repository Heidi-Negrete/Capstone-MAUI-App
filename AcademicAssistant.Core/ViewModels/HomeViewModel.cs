using System.Collections.ObjectModel;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Core.ViewModels;

public class HomeViewModel: ObservableRecipient
{
    private readonly IRepository _repository;
    
    private Student _student;
    public Student Student
    {
        get => _student;
        set
        {
            SetProperty(ref _student, value);
        }
    }
    public string Title { get; set; } = "Academic Assistant";
    
    public ObservableCollection<Term> Terms { get; set; } = new();
    
    public HomeViewModel(IRepository repository)
    {
        _repository = repository;
        Student = _repository.GetStudent();;
    }

    public void LoadStudent()
    {
       Student = _repository.GetStudent(); 
    }
}