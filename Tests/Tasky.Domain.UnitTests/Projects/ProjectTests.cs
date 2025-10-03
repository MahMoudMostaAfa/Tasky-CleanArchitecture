using Tasky.Domain.Projects;
using Xunit;

namespace Tasky.Domain.UnitTests.Projects;

public class ProjectTests
{

  [Fact]
  public void CreateProject_WithValidParameters_ShouldCreateProject()
  {
    // Arrange
    var name = "Test Project";
    var description = "This is a test project.";

    // Act
    var projectResult = Project.Create(name: name, description: description, ownerId: "owner-123");

    var project = projectResult.Value;

    Assert.True(projectResult.IsSuccess);
    Assert.Equal(name, project.Name);
    Assert.Equal(description, project.Description);
    Assert.Equal("owner-123", project.OwnerId);
    Assert.NotEqual(Guid.Empty, project.Id);
  }

  [Fact]
  public void CreateProject_WithEmptyName_ShouldReturnError()
  {
    // Arrange
    var name = "";
    var description = "This is a test project.";

    // Act
    var projectResult = Project.Create(name: name, description: description, ownerId: "owner-123");

    // Assert
    Assert.False(projectResult.IsSuccess);
    Assert.Equal(ProjectErrors.NameIsRequired, projectResult.Errors[0]);
  }
  [Fact]
  public void CreateProject_WithEmptyOwnerId_ShouldReturnError()
  {
    // Arrange
    var name = "Test Project";
    var description = "This is a test project.";

    // Act
    var projectResult = Project.Create(name: name, description: description, ownerId: "");

    // Assert
    Assert.False(projectResult.IsSuccess);
    Assert.Equal(ProjectErrors.InValidOwnerId, projectResult.Errors[0]);
  }

  [Fact]
  public void UpdateProject_WithEmptyName_ShouldReturnError()
  {
    var project = Project.Create(name: "Initial Name", description: "Initial Description", ownerId: "owner-123").Value;
    var updateResult = project.Update(name: "", description: "Updated Description");

    Assert.False(updateResult.IsSuccess);
    Assert.Equal(ProjectErrors.NameIsRequired, updateResult.Errors[0]);
  }

  [Fact]
  public void UpdateProject_WithValidParameters_ShouldUpdateProject()
  {
    // Arrange
    var project = Project.Create(name: "Initial Name", description: "Initial Description", ownerId: "owner-123").Value;

    // Act
    var updateResult = project.Update(name: "Updated Name", description: "Updated Description");

    // Assert
    Assert.True(updateResult.IsSuccess);
    Assert.Equal("Updated Name", project.Name);
    Assert.Equal("Updated Description", project.Description);
    Assert.NotNull(project.ModifiedAt);
  }
}