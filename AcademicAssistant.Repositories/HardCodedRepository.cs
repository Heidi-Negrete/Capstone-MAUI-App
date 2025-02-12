using AcademicAssistant.Repositories.Models;

namespace AcademicAssistant.Repositories;

public class HardCodedRepository : IRepository
{
    private Student _student;
    private List<Term> _terms;
    private int TermId = 7; // Start at 7 because we have 6 hardcoded terms

    public HardCodedRepository()
    {
        // Hardcoded data
        _student = new Student
        {
            Id = 1,
            Name = "John Doe",
        };
        _terms = new List<Term>()
        {
            new Term { Id = 1, Title = "Fall 2021" }, new Term { Id = 2, Title = "Winter 2021" },
            new Term { Id = 3, Title = "Term Three" }, new Term { Id = 4, Title = "Term Four" },
            new Term { Id = 5, Title = "Term Five" }, new Term { Id = 6, Title = "Final Term" }
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
}