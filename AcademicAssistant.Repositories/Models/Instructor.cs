using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Repositories.Models;

public partial class Instructor : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty] 
    private string _name;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _phoneNumber;
}
