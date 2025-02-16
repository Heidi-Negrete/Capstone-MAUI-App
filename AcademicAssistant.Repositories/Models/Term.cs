using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace AcademicAssistant.Repositories.Models;

public partial class Term : ObservableValidator
{
    [Required]
    [PrimaryKey, AutoIncrement]                                                                                                                               
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Student")]
    public int StudentId { get; set; }

    [Required]
    [ObservableProperty]
    private string _title;

    [Required]
    [ObservableProperty]
    private DateTime _startDate;
    
    [Required]
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
