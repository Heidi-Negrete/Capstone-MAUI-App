using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Repositories;

public interface IRepository
{
    public Student GetStudent();
    public void UpdateStudent(int id, Student student);
    public List<Term> GetTerms();
    public void DeleteTerm(int termId);
    public void AddTerm();
    public Term GetTermById(int termId);
    public List<Course> GetCoursesByTermId(int termId);
    public void DeleteCourse(int courseId);
    public void AddCourse(int termId);
}