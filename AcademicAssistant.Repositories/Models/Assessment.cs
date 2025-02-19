using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace AcademicAssistant.Repositories.Models;

public partial class Assessment : ObservableValidator
{
    [Required]
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [ObservableProperty]
    private string _type;

    [Required]
    [ObservableProperty]
    private string _title;

    [Required] [ObservableProperty] private bool _notificationEnabled = false;

    [Required]
    [ObservableProperty]
    private DateTime _startDate;

    [Required]
    [ObservableProperty] 
    private DateTime _endDate;
    
    [ObservableProperty] 
    private AcademicStatus.Status _status = AcademicStatus.Status.Planned;
    
    [Required]
    [ForeignKey("Course")]
    [ObservableProperty]
    private int _courseId;
    
    [ObservableProperty]
    private string _about;
}
