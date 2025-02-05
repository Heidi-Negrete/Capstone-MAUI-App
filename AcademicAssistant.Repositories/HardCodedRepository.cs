using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Repositories;

public class HardCodedRepository : IRepository
{
    private Student _student; 

    public HardCodedRepository()
    {
        _student = new Student
        {
            Id = 1,
            Name = "John Doe",
            Terms = new List<Term>() {new Term {Id = 1, Title = "Fall 2021"}, new Term {Id = 1, Title = "Slrjg 2021"}, new Term {Id = 1, Title = "dadfad1"}, new Term {Id = 1, Title = "dadfad1"}, new Term {Id = 1, Title = "dadfad1"}, new Term {Id = 1, Title = "dadfad1"}, new Term {Id = 1, Title = "dadfad1"}, new Term {Id = 1, Title = "dadfad1"}, new Term {Id = 1, Title = "dadfad1"}, new Term {Id = 1, Title = "dadfad1"}, new Term {Id = 1, Title = "dadfad1"}}
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
}