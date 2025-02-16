using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace AcademicAssistant.Repositories.Models;

public partial class Instructor : ObservableValidator
{
    [Required]
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Required]
    [ObservableProperty] 
    private string _name;

    [Required]
    [ObservableProperty]
    private string _email;

    [Required]
    [ObservableProperty]
    private string _phoneNumber;
}
