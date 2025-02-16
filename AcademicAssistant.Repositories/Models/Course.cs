using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace AcademicAssistant.Repositories.Models;

public partial class Course : ObservableValidator
{
    [Required]
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Required]
    [ObservableProperty] 
    private string _title;

    [Required]
    [ObservableProperty] 
    private DateTime _startDate;

    [Required]
    [ObservableProperty]
    private DateTime _endDate;
    
    [Required]
    [ForeignKey("Term")]
    [ObservableProperty]
    private int _termId;

    [ObservableProperty]
    private AcademicStatus.Status _status;

    [ForeignKey("Instructor")]
    [ObservableProperty]
    private int _instructorId;
    
    [ObservableProperty]
    private string _notes;
    
    // TO DO OBSERVABLE COLLECTION
    public List<Assessment> Assessments { get; set; }
}