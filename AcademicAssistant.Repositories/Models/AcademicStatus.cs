namespace AcademicAssistant.Repositories.Models;

public class AcademicStatus
{
    public enum Status
    {
        InProgress,
        Completed,
        Failed,
        Unknown,
        Dropped,
        Planned
    }
}