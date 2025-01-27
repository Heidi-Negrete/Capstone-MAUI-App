namespace AcademicAssistant.Core.Models;

public class Term
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Course> Courses { get; private set; }

    // maybe this should be in the UI layer
    public void addCourse(Course course)
    {
        // User can only have 6 courses in a term
        if (Courses.Count == 6)
        {
            // to do add a custom exception
            throw new Exception("Cannot add more than 6 courses to a term");
        }
        else
        {
            Courses.Add(course);
        }
    }

    public void removeCourse(Course course)
    {
        // User can only remove courses if there are courses in the term
        if (Courses.Count != 0)
        {
            Courses.Remove(course);
        }
    }
}
