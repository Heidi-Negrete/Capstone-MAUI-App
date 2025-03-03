using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Repositories;

public interface IRepository
{
    public Task<Student> GetStudent();
    public Task UpdateStudent(int id, Student student);
    public Task<List<Term>> GetTerms();
    public Task DeleteTerm(int termId);
    public Task AddTerm(Term? term = null);
    public Task<Term> GetTermById(int termId);
    public Task<List<Course>> GetCoursesByTermId(int termId);
    public Task DeleteCourse(int courseId);
    public Task AddCourse(int termId, Course? course = null);
    public Task<Course> GetCourseById(int courseId);
    public Task<List<Assessment>> GetAssessmentsByCourseId(int courseId);
    public Task<Assessment> GetAssessmentById(int assessmentId, string type);
    public Task<List<Course>> GetCourses();
    public Task<List<Assessment>> GetAssessments();
    public Task UpdateCourse(int id, Course course);
    public Task UpdateTerm(int id, Term term);
    public Task UpdateAssessment(int id, Assessment assessment);
    public Task<List<Term>> Search(string searchtext);
}