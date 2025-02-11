using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Repositories;

public class HardCodedRepository : IRepository
{
    private Student _student;
    private int TermId = 7;

    public HardCodedRepository()
    {
        _student = new Student
        {
            Id = 1,
            Name = "John Doe",
            Terms = new List<Term>() {new Term {Id = 1, Title = "Fall 2021"}, new Term {Id = 2, Title = "Winter 2021"}, new Term {Id = 3, Title = "Term Three"}, new Term {Id = 4, Title = "Term Four"}, new Term {Id = 5, Title = "Term Five"}, new Term {Id = 6, Title = "Final Term"}}
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
    
    public void DeleteTerm(int termId)
    {
        _student.Terms.Remove(_student.Terms.First(t => t.Id == termId));
    }
    
    public void AddTerm()
    {
        _student.Terms.Add(new Term {Id = TermId++, Title = "New Term"});
    }
}