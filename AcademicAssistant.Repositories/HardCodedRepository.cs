using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Repositories;

public class HardCodedRepository : IRepository
{
    private Student _student;
    private List<Term> _terms;
    private int TermId = 7; // Start at 7 because we have 6 hardcoded terms
    private List<Course> _courses;
    private int CourseId = 7; // Start at 7 because we have 6 hardcoded courses
    private Instructor _instructor;

    public HardCodedRepository()
    {
        // Hardcoded data
        _student = new Student
        {
            Id = 1,
            Name = "John Doe",
        };
        _instructor = new Instructor
        {
            Id = 1,
            Name = "Anika Patel",
            Email = "anika.patel @strimeuniversity.edu",
            PhoneNumber = "555-123-4567"
        };
        _terms = new List<Term>()
        {
            new Term { Id = 1, Title = "Fall 2021", StartDate = new DateTime(2021, 1, 1), EndDate = new DateTime(2021, 6, 1)}, 
            new Term { Id = 2, Title = "Winter 2021" },
            new Term { Id = 3, Title = "Term Three" }, 
            new Term { Id = 4, Title = "Term Four" },
            new Term { Id = 5, Title = "Term Five" }, 
            new Term { Id = 6, Title = "Final Term" }
        };
        _courses = new List<Course>()
        {
            new Course
            {
                Id = 1, Title = "Course 1", StartDate = new DateTime(2021, 1, 1), EndDate = new DateTime(2021, 1, 31), TermId = 1, InstructorId = 1 
            },
            new Course
            {
                Id = 2, Title = "Course 2", StartDate = new DateTime(2021, 2, 1), EndDate = new DateTime(2021, 2, 28), TermId = 1, InstructorId = 1
            },
            new Course
            {
                Id = 3, Title = "Course 3", StartDate = new DateTime(2021, 3, 1), EndDate = new DateTime(2021, 3, 31), TermId = 1, InstructorId = 1
            },
            new Course
            {
                Id = 4, Title = "Course 4", StartDate = new DateTime(2021, 4, 1), EndDate = new DateTime(2021, 4, 30), TermId = 3, InstructorId = 1
            },
            new Course
            {
                Id = 5, Title = "Course 5", StartDate = new DateTime(2021, 5, 1), EndDate = new DateTime(2021, 5, 31), TermId = 3, InstructorId = 1
            },
            new Course
            {
                Id = 6, Title = "Course 6", StartDate = new DateTime(2021, 6, 1), EndDate = new DateTime(2021, 6, 30), TermId = 5, InstructorId = 1
            }
        };
    }
    
    public Student GetStudent()
    {
        // return a new student with some hardcoded data
        return _student;
    }
    
    public void UpdateStudent(int id, Student student)
    {
        _student = student;
    }
    
    public List<Term> GetTerms()
    {
        return _terms;
    }
    
    public void DeleteTerm(int termId)
    {
        _terms.Remove(_terms.First(t => t.Id == termId));
    }
    
    public void AddTerm()
    {
        _terms.Add(new Term {Id = TermId++, Title = "New Term"});
    }
    
    public Term GetTermById(int termId)
    {
        return _terms.First(t => t.Id == termId);
    }
    
    public List<Course> GetCoursesByTermId(int termId)
    {
        return _courses.Where(c => c.TermId == termId).ToList();
    }
    
    public void DeleteCourse(int courseId)
    {
        _courses.Remove(_courses.First(c => c.Id == courseId));
    }
    
    public void AddCourse(int termId)
    {
        _courses.Add(new Course {Id = CourseId++, Title = "New Course", TermId = termId});
    }
}