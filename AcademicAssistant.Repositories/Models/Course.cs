using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Repositories.Models;

public partial class Course : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty] 
    private string _title;

    [ObservableProperty] 
    private DateTime _startDate;

    [ObservableProperty]
    private DateTime _endDate;
    
    [ObservableProperty]
    private int _termId;

    [ObservableProperty]
    private AcademicStatus.Status _status;

    [ObservableProperty]
    private int _instructorId;

    [ObservableProperty]
    private string _notes;
    
    // TO DO OBSERVABLE COLLECTION
    public List<Assessment> Assessments { get; set; }
}