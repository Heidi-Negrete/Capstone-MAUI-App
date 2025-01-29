namespace AcademicAssistant.Repositories.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public AcademicStatus.Status Status { get; set; }

    public Instructor Instructor { get; set; }

    public string Notes { get; set; }

    public List<Assessment> Assessments { get; set; }
}