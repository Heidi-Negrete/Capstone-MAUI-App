using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Repositories.Models;

public partial class Student : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty] 
    private string _name;
    public List<Term> Terms { get; set; }
}