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
    private AcademicStatus.Status _status;

    [ObservableProperty]
    private Instructor _instructor;

    [ObservableProperty]
    private string _notes;
    
    // TO DO OBSERVABLE COLLECTION
    public List<Assessment> Assessments { get; set; }
}