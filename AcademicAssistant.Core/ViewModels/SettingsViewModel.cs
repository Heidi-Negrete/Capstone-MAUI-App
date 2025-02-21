using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AcademicAssistant.Core.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IRepository _repository;
    
    [ObservableProperty]
    private Student _student;

    public SettingsViewModel(IRepository repository)
    {
        _repository = repository;
        LoadStudent();
    }
    
    public async void LoadStudent()
    {
        Student = await _repository.GetStudent(); 
    }
    
    [RelayCommand]
    public async void UpdateStudent()
    {
        _repository.UpdateStudent(Student.Id, Student);

        await Shell.Current.GoToAsync($"//home");
    }
}