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
    private List<Assessment> _assessments;

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
                Id = 1, Title = "Course 1", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1), TermId = 1, InstructorId = 1, InstructorName = "Anika Patel", InstructorEmail = "anika.patel @strimeuniversity.edu",InstructorPhone = "555-123-4567", NotificationEnabled = true
            },
            new Course
            {
                Id = 2, Title = "Course 2", StartDate = new DateTime(2021, 2, 1), EndDate = new DateTime(2021, 2, 28), TermId = 1, InstructorId = 1, InstructorName = "Anika Patel", InstructorEmail = "anika.patel @strimeuniversity.edu",InstructorPhone = "555-123-4567"
            },
            new Course
            {
                Id = 3, Title = "Course 3", StartDate = new DateTime(2021, 3, 1), EndDate = new DateTime(2021, 3, 31), TermId = 1, InstructorId = 1, InstructorName = "Anika Patel", InstructorEmail = "anika.patel @strimeuniversity.edu",InstructorPhone = "555-123-4567"
            },
            new Course
            {
                Id = 4, Title = "Course 4", StartDate = new DateTime(2021, 4, 1), EndDate = new DateTime(2021, 4, 30), TermId = 3, InstructorId = 1, InstructorName = "Anika Patel", InstructorEmail = "anika.patel @strimeuniversity.edu",InstructorPhone = "555-123-4567"
            },
            new Course
            {
                Id = 5, Title = "Course 5", StartDate = new DateTime(2021, 5, 1), EndDate = new DateTime(2021, 5, 31), TermId = 3, InstructorId = 1, InstructorName = "Anika Patel", InstructorEmail = "anika.patel @strimeuniversity.edu",InstructorPhone = "555-123-4567"
            },
            new Course
            {
                Id = 6, Title = "Course 6", StartDate = new DateTime(2021, 6, 1), EndDate = new DateTime(2021, 6, 30), TermId = 5, InstructorId = 1, InstructorName = "Anika Patel", InstructorEmail = "anika.patel @strimeuniversity.edu",InstructorPhone = "555-123-4567"
            }
        };
        _assessments = new List<Assessment>()
        {
            new PerformanceAssessment
            {
                Id = 1, Title = "Assessment 1", StartDate = new DateTime(2021, 1, 15), EndDate = DateTime.Today.AddDays(1), CourseId = 1, About = "This is a performance assessment", Status = AcademicStatus.Status.InProgress, NotificationEnabled = true
            },
            new ObjectiveAssessment
            {
                Id = 2, Title = "Assessment 2", StartDate = new DateTime(2021, 2, 15), EndDate = new DateTime(2021, 6, 15), CourseId = 1, About = "This is an objective assessment", Status = AcademicStatus.Status.InProgress
            },
            new PerformanceAssessment
            {
                Id = 3, Title = "Assessment 3", StartDate = new DateTime(2021, 3, 15), EndDate = new DateTime(2021, 6, 15), CourseId = 2, About = "This is a performance assessment", Status = AcademicStatus.Status.InProgress
            },
            new ObjectiveAssessment
            {
                Id = 4, Title = "Assessment 4", StartDate = new DateTime(2021, 4, 15), EndDate = new DateTime(2021, 6, 15), CourseId = 2, About = "This is an objective assessment", Status = AcademicStatus.Status.InProgress
            },
            new PerformanceAssessment
            {
                Id = 5, Title = "Assessment 5", StartDate = new DateTime(2021, 5, 15), EndDate = new DateTime(2021, 6, 15), CourseId = 3, About = "This is a performance assessment", Status = AcademicStatus.Status.InProgress
            },
            new ObjectiveAssessment
            {
                Id = 6, Title = "Assessment 6", StartDate = new DateTime(2021, 6, 15), EndDate = new DateTime(2021, 6, 15), CourseId = 3, About = "This is an objective assessment", Status = AcademicStatus.Status.InProgress
            }
        };
    }
    
    public async Task<Student> GetStudent()
    {
        // return a new student with some hardcoded data
        return _student;
    }
    
    public async Task UpdateStudent(int id, Student student)
    {
         _student = student;
    }
    
    public async Task<List<Term>> GetTerms()
    {
        return _terms;
    }
    
    public async Task DeleteTerm(int termId)
    {
        _terms.Remove(_terms.First(t => t.Id == termId));
    }
    
    public async Task AddTerm(Term? term = null)
    {
        _terms.Add(new Term {Id = TermId++, Title = "New Term", StudentId = _student.Id});
    }
    
    public async Task<Term> GetTermById(int termId)
    {
        return _terms.First(t => t.Id == termId);
    }
    
    public async Task<List<Course>> GetCoursesByTermId(int termId)
    {
        return _courses.Where(c => c.TermId == termId).ToList();
    }
    
    public async Task DeleteCourse(int courseId)
    {
        Course course = await GetCourseById(courseId);
        Term term = await GetTermById(course.TermId);
        term.CourseCount--;
        // To DO Update term in database
        _courses.Remove(_courses.First(c => c.Id == courseId));
    }
    
    public async Task AddCourse(int termId, Course? course = null)
    {
        Term term = await GetTermById(termId);
        if (term.CourseCount == term.MaxCourseCount)
        {
            throw new Exception("Max course count reached");
        }
        term.CourseCount++;
        // To DO Update term in database
        _courses.Add(new Course {Id = CourseId++, Title = "New Course", TermId = termId});
    }
    
    public async Task<Course> GetCourseById(int courseId)
    {
        return _courses.First(c => c.Id == courseId);
    }
    
    public async Task<List<Assessment>> GetAssessmentsByCourseId(int courseId)
    {
        return _assessments.Where(a => a.CourseId == courseId).ToList();
    }
    
    public async Task<Assessment> GetAssessmentById(int assessmentId)
    {
        return _assessments.First(a => a.Id == assessmentId);
    }
    
    public async Task<List<Course>> GetCourses()
    {
        return _courses;
    }
    
    public async Task<List<Assessment>> GetAssessments()
    {
        return _assessments;
    }

    public Task UpdateCourse(int id, Course course)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTerm(int id, Term term)
    {
        throw new NotImplementedException();
    }
}