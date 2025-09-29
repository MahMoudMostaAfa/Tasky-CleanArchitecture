using Tasky.Domain.Common;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Domain.Projects;

public class Project : Entity
{

  public string Name { get; private set; }
  public string? Description { get; private set; }

  public string OwnerId { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime? ModifiedAt { get; private set; }

  private readonly List<ProjectTask> _projectTasks = new();

  public IReadOnlyList<ProjectTask> ProjectTasks => _projectTasks.AsReadOnly();


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private Project()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  {
  }
  private Project(Guid id, string name, string? description, string ownerId, DateTime createdAt) : base(id)
  {
    Name = name;
    Description = description;
    OwnerId = ownerId;
    CreatedAt = createdAt;
  }



}