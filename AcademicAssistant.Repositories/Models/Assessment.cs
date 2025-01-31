using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Repositories.Models;

public abstract partial class Assessment : ObservableObject
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
}
