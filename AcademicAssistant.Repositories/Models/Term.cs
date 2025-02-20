using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace AcademicAssistant.Repositories.Models;

public partial class Term : ObservableValidator
{
    [Required]
    [PrimaryKey, AutoIncrement]                                                                                                                               
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Student")]
    public int StudentId { get; set; }

    [Required]
    [ObservableProperty]
    private string _title = "New Term";

    [Required] [ObservableProperty] private DateTime _startDate = DateTime.Today;

    [Required] [ObservableProperty] private DateTime _endDate = DateTime.Now.AddDays(90);

    [ObservableProperty] private int _courseCount = 0;
    
    [ObservableProperty]
    private int _maxCourseCount = 6;
}
