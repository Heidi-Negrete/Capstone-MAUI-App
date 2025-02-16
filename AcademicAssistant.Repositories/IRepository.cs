using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Repositories;

public interface IRepository
{
    public Task<Student> GetStudent();
    public Task UpdateStudent(int id, Student student);
    public Task<List<Term>> GetTerms();
    public Task DeleteTerm(int termId);
    public Task AddTerm(int studentId);
    public Task<Term> GetTermById(int termId);
    public Task<List<Course>> GetCoursesByTermId(int termId);
    public Task DeleteCourse(int courseId);
    public Task AddCourse(int termId);
    public Task<Course> GetCourseById(int courseId);
    public Task<List<Assessment>> GetAssessmentsByCourseId(int courseId);
    public Task<Assessment> GetAssessmentById(int assessmentId);
}