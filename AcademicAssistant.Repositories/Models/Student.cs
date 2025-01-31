using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Repositories.Models;

public partial class Student : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty] 
    private string _name;
    
    // To DO setup observable collection
    public List<Term> Terms { get; set; }
}