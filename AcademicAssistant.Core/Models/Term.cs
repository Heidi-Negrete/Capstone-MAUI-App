namespace AcademicAssistant.Core.Models;

public class Term
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<Course> Courses { get; set; }
}
