using Microsoft.VisualBasic;
using Tasky.Domain.Common;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;
using Tasky.Domain.ProjectTasks.Enums;


namespace Tasky.Domain.ProjectTasks;

public class ProjectTask : Entity
{

  public string Title { get; private set; }

  public string? Description { get; private set; }

  public ProjectTaskStatus Status { get; private set; } = ProjectTaskStatus.ToDo;


  public ProjectTaskPriority Priority { get; private set; } = ProjectTaskPriority.Medium;

  public DateTime? DueDateUtc { get; private set; }

  public Guid ProjectId { get; private set; }
  public Project? Project { get; private set; }

  public string? AssignedTo { get; private set; }

  public DateTime CreatedAt { get; private set; }
  public DateTime? ModifiedAt { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private ProjectTask() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


  private ProjectTask(Guid id, string title, string? description, Guid projectId, DateTime createdAt, DateTime dueDate) : base(id)
  {
    Title = title;
    Description = description;
    ProjectId = projectId;
    CreatedAt = createdAt;
    DueDateUtc = dueDate;
  }


  public static Result<ProjectTask> Create(string title, string? description, Guid projectId, DateTime dueDate)
  {
    if (string.IsNullOrWhiteSpace(title))
    {
      return ProjectTaskErrors.TitleIsRequired;
    }

    if (projectId == Guid.Empty)
    {
      return ProjectTaskErrors.ProjectIdIsRequired;
    }

    var projectTask = new ProjectTask(
      id: Guid.NewGuid(),
      title: title,
      description: description,
      projectId: projectId,
      createdAt: DateTime.UtcNow,
      dueDate: dueDate
    );

    return projectTask;
  }



}