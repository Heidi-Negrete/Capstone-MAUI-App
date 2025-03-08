using AcademicAssistant.Core.ViewModels;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using Moq;

namespace AcademicAssistant.Tests;

public class TermDetailsViewModelTests
{
    [Fact]
    public async Task AfterLoadingPage_NoViewModelPropertyNull()
    {
        // Arrange
        var repository = new Mock<IRepository>();
        int TermId = 1;
        var term = new Term { Id = TermId, Title = "Test Term" };
        var courses = new List<Course>
        {
            new Course { Id = 1, Title = "Course 1", TermId = TermId },
            new Course { Id = 2, Title = "Course 2", TermId = TermId }
        };
        
        repository.Setup(r => r.GetTermById(TermId)).ReturnsAsync(term);
        repository.Setup(r => r.GetCoursesByTermId(TermId)).ReturnsAsync(courses);
        
        var viewModel = new TermDetailsViewModel(repository.Object);
        
        // Act
        viewModel.LoadData(TermId); // Normally called by the UI page, here we can see if it functions independent of the UI
        
        // Assert
        Assert.NotNull(viewModel.Term);
        Assert.NotNull(viewModel.Courses);
    }
}