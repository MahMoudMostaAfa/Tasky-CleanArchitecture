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

  public static Result<Project> Create(string name, string? description, string ownerId)
  {

    if (string.IsNullOrWhiteSpace(ownerId))
    {
      return ProjectErrors.InValidOwnerId;
    }

    if (string.IsNullOrWhiteSpace(name))
    {
      return ProjectErrors.NameIsRequired;
    }

    var Project = new Project(
      id: Guid.NewGuid(),
      name: name,
      description: description,
      ownerId: ownerId,
      createdAt: DateTime.UtcNow
    );

    return Project;

  }

  public Result<Updated> Update(string? name, string? description)
  {

    if (string.IsNullOrWhiteSpace(name))
    {
      return ProjectErrors.NameIsRequired;
    }


    Name = name != Name ? name : Name;
    Description = description != Description ? description : Description;
    ModifiedAt = DateTime.UtcNow;

    return Result.Updated;
  }
}


