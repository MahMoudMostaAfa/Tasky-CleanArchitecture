using Microsoft.EntityFrameworkCore;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;
using Tasky.Infrastructure.Data;

namespace Tasky.Infrastructure.Repositories;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
  public async Task AddAsync(ProjectTask projectTask)
  {
    await context.ProjectTasks.AddAsync(projectTask);
  }

  public async Task<Result<ProjectTask>> GetByIdAsync(Guid id)
  {
    var projectTask = await context.ProjectTasks.FindAsync(id);


    if (projectTask is null) return ProjectTaskErrors.ProjectTaskNotFound;


    return projectTask;

  }
  public async Task<Result<ProjectTask>> GetWithDetailsByIdAsync(Guid id)
  {
    var projectTask = await context.ProjectTasks
        .Include(p => p.Project)
        .FirstOrDefaultAsync(pt => pt.Id == id);

    if (projectTask is null) return ProjectTaskErrors.ProjectTaskNotFound;

    return projectTask;



  }

  public void Delete(ProjectTask projectTask)
  {
    context.ProjectTasks.Remove(projectTask);
  }
}