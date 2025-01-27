namespace AcademicAssistant.Core.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Term> Terms { get; set; }
}