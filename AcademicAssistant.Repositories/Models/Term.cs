using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicAssistant.Repositories.Models;

public partial class Term : ObservableObject
{
    public int Id { get; set; }
    
    public int StudentId { get; set; }

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private DateTime _startDate;
    
    [ObservableProperty]
    private DateTime _endDate;
    
    // TO DO OBSERVABLE COLLECTION
    public List<Course> Courses { get; private set; }

    // extract to a manager?
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
