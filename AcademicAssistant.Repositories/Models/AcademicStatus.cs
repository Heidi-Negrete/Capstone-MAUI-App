namespace AcademicAssistant.Repositories.Models;

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