using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AcademicAssistant.Core.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
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
        _repository.UpdateStudent(_student.Id, _student);

        await Shell.Current.GoToAsync($"//home");
    }
}