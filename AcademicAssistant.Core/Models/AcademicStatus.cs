namespace AcademicAssistant.Core.Models;

public class AcademicStatus
{
    public enum Status
    {
        InProgress,
        Completed,
        Failed,

        Dropped,
        Planned
    }
}