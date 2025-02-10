using System.Collections.ObjectModel;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Core.ViewModels;

public partial class HomeViewModel: ObservableRecipient
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

    [ObservableProperty]
    private ObservableCollection<Term> _terms;
    
    public HomeViewModel(IRepository repository)
    {
        _repository = repository;
        Student = _repository.GetStudent();;
        Terms = new ObservableCollection<Term>(Student.Terms);
    }
}