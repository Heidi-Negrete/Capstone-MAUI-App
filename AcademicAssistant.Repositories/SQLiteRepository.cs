using AcademicAssistant.Repositories.Models;
using SQLite;

namespace AcademicAssistant.Repositories;

public class SQLiteRepository : IRepository
{
    private SQLiteAsyncConnection _database;
    
    // WHEN TIME TO IMPLEMENT SEARCH SEE U 44. 8:46
    
    public SQLiteRepository()
    {
        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        _database.CreateTableAsync<Student>().Wait();
        _database.CreateTableAsync<Term>().Wait();
        _database.CreateTableAsync<Course>().Wait();
        _database.CreateTableAsync<Assessment>().Wait();
        _database.CreateTableAsync<Instructor>().Wait();
    }

    private async Task SeedData()
    {
        // To Do seed data for testing
    }

    public async Task<Student> GetStudent()
    {
        return await _database.Table<Student>().FirstOrDefaultAsync();
    }

    public async Task UpdateStudent(int id, Student student)
    {
        await _database.UpdateAsync(student);
    }
    
    // NOT IN USE YET. Should have StudentId?
    public async Task AddTerm(Term term)
    {
        await _database.InsertAsync(term);
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

    public async Task AddTerm(int studentId)
    {
        Term term = CreateNewTerm(studentId);
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
            await _database.DeleteAsync(course);
        }
    }

    public async Task AddCourse(int termId)
    {
        Course course = CreateNewCourse(termId);
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

    public Course CreateNewCourse(int termId)
    {
        return new Course()
        {
            Title = "New Course",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7),
            Status = AcademicStatus.Status.InProgress,
            TermId = termId
        };
    }

    public Term CreateNewTerm(int studentId)
    {
        return new Term()
        {
            Title = "New Term",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(90),
            StudentId = studentId
        };
    }
}