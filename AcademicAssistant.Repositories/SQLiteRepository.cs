using AcademicAssistant.Repositories.Models;
using SQLite;

namespace AcademicAssistant.Repositories;

public class SQLiteRepository : IRepository
{
    private SQLiteAsyncConnection _database;

    private int _studentId;
    
    // WHEN TIME TO IMPLEMENT SEARCH SEE U 44. 8:46
    
    // TODO Implement cache so we only need to load data once or if data changes?
    
    public SQLiteRepository()
    {
        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        _database.CreateTableAsync<Student>().Wait();
        _database.CreateTableAsync<Term>().Wait();
        _database.CreateTableAsync<Course>().Wait();
        _database.CreateTableAsync<Assessment>().Wait();
        _database.CreateTableAsync<Instructor>().Wait();
        InitializeData().Wait();
    }

    private async Task InitializeData()
    {
        // only create student if it does not already exist
        if (await GetStudent() != null) return;
        
        await _database.InsertAsync(new Student()
        {
            Name = "John Doe",
        });
        _studentId = (await GetStudent()).Id;
        
        // Seed Data for Demo and Testing Only
        
        Term term = new Term
        {
            Title = "Term One"
        };
        await AddTerm(term);
        
        Course course = new Course
        {
            Title = "Course One",
            InstructorName = "Anika Patel",
            InstructorEmail = "anika.patel@strimeuniversity.edu",
            InstructorPhone = "555-123-4567",
        };
        await AddCourse(term.Id, course);
    }
    
    private async Task CreateAssessments(int courseId)
    {
        await _database.InsertAsync(new ObjectiveAssessment
        {
            CourseId = courseId
        });

        await _database.InsertAsync(new PerformanceAssessment
        {
            CourseId = courseId
        });
    }

    public async Task<Student> GetStudent()
    {
        return await _database.Table<Student>().FirstOrDefaultAsync();
    }

    public async Task UpdateStudent(int id, Student student)
    {
        await _database.UpdateAsync(student);
    }

    public async Task<List<Term>> GetTerms()
    {
        return await _database.Table<Term>().ToListAsync();
    }

    public async Task DeleteTerm(int termId)
    {
        Term term = await GetTermById(termId);
        if (term != null)
        {
            await _database.DeleteAsync(term);
        }
    }

    public async Task AddTerm(Term? term = null)
    {
        if (term == null)
        {
            term = new Term();
        }
        term.StudentId = _studentId;
        await _database.InsertAsync(term);
    }

    public async Task<Term> GetTermById(int termId)
    {
        return await _database.Table<Term>().Where(t => t.Id == termId).FirstOrDefaultAsync();
    }

    public async Task<List<Course>> GetCoursesByTermId(int termId)
    {
        return await _database.Table<Course>().Where(c => c.TermId == termId).ToListAsync();
    }

    public async Task DeleteCourse(int courseId)
    {
        Course course = GetCourseById(courseId).Result;
        if (course != null)
        {
            Term term = await GetTermById(course.TermId);
            term.CourseCount--;
            // To DO Update term in database
            await _database.DeleteAsync(course);
        }
    }

    public async Task AddCourse(int termId, Course? course = null)
    {
        Term term = await GetTermById(termId);
        if (term.CourseCount == term.MaxCourseCount)
        {
            throw new Exception("Max course count reached");
        }
        term.CourseCount++;
        if (course == null)
        {
            course = new Course();
        }
        course.TermId = termId;
        await _database.InsertAsync(course);
    }
    
    public async Task<Course> GetCourseById(int courseId)
    {
        return await _database.Table<Course>().Where(c => c.Id == courseId).FirstOrDefaultAsync();
    }

    public async Task<List<Assessment>> GetAssessmentsByCourseId(int courseId)
    {
        return await _database.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync();
    }

    public async Task<Assessment> GetAssessmentById(int assessmentId)
    {
        return await _database.Table<Assessment>().Where(a => a.Id == assessmentId).FirstOrDefaultAsync();
    }
    
    public async Task<List<Course>> GetCourses()
    {
        return await _database.Table<Course>().ToListAsync();
    }
    
    public async Task<List<Assessment>> GetAssessments()
    {
        return await _database.Table<Assessment>().ToListAsync();
    }
}