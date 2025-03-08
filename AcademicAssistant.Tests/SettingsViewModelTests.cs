using AcademicAssistant.Core.ViewModels;
using AcademicAssistant.Repositories;
using AcademicAssistant.Repositories.Models;
using Moq;

namespace AcademicAssistant.Tests;

public class SettingsViewModelTests
{
    [Fact]
    public void AfterLoadingPage_StudentPropertyShouldNotBeNull()
    {
        // Arrange
        var repository = new Mock<IRepository>();
        var student = new Student { Id = 1, Name = "Test Student" };
        repository.Setup(r => r.GetStudent()).ReturnsAsync(student);

        // Act
        var viewModel = new SettingsViewModel(repository.Object);

        // Assert
        Assert.Equal(student, viewModel.Student);
    }
}