using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace AcademicAssistant.Core.ViewModels;


public partial class TermDetailsViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    
    
    [ObservableProperty] private Term _term;
    
    public TermDetailsViewModel(IRepository repository)
    {
        _repository = repository;
        Trace.WriteLine("TermDetailsViewModel created.");
    }

    public void LoadData(int TermId)
    {
        Trace.WriteLine("Loading term data for term id: " + TermId);
        Term = _repository.GetTermById(TermId);
    }
}
