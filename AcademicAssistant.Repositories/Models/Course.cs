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
    private string _title = "New Course";

    [Required] [ObservableProperty] private bool _notificationEnabled = false;

    [Required] [ObservableProperty] private DateTime _startDate = DateTime.Today;

    [Required] [ObservableProperty] private DateTime _endDate = DateTime.Now.AddDays(7);
    
    [Required]
    [ForeignKey("Term")]
    [ObservableProperty]
    private int _termId;

    [ObservableProperty]
    private AcademicStatus.Status _status = AcademicStatus.Status.Planned;

    [ForeignKey("Instructor")]
    [ObservableProperty]
    private int _instructorId;
    
    [ObservableProperty]
    private string _notes;
    
    [Required]
    [ObservableProperty] 
    private string _instructorName;

    [Required]
    [ObservableProperty]
    private string _instructorEmail;

    [Required]
    [ObservableProperty]
    private string _instructorPhone;
}