using System.Collections.ObjectModel;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

    [ObservableProperty] private bool _termSelected;
    
    [ObservableProperty]
    private Term _selectedTerm;

    [ObservableProperty]
    private ObservableCollection<Term> _terms;
    
    public HomeViewModel(IRepository repository)
    {
        _repository = repository;
        Student = _repository.GetStudent();
        Terms = new ObservableCollection<Term>(Student.Terms);
        TermSelected = false;
    }

    [RelayCommand]
    public void DeleteTerm()
    {
        if (SelectedTerm == null) return;
        _repository.DeleteTerm(SelectedTerm.Id);
        Terms.Remove(SelectedTerm);
        SelectionChanged(null); // On Android if swipeview used to delete, selectionchanged does not fire
    }
    
    [RelayCommand]
    public void AddTerm()
    {
        _repository.AddTerm();
        Terms = new ObservableCollection<Term>(Student.Terms);
    }

    [RelayCommand]
    public void SelectionChanged(Term? term)
    {
        if (term == null) TermSelected = false;
        else
        {
            TermSelected = true;
        }
    }
    
}