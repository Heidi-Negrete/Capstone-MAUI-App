using AcademicAssistant.Views;

namespace AcademicAssistant;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("term", typeof(TermDetailsPage));
        Routing.RegisterRoute("course", typeof(CourseDetailsPage));
        Routing.RegisterRoute("assessment", typeof(AssessmentDetailsPage));
    }
}
