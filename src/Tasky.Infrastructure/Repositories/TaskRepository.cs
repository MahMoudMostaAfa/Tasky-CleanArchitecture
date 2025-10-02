using Microsoft.EntityFrameworkCore;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;
using Tasky.Domain.ProjectTasks.Comments;
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
        .Include(p => p.Project).Include(p => p.Comments)
        .FirstOrDefaultAsync(pt => pt.Id == id);

    if (projectTask is null) return ProjectTaskErrors.ProjectTaskNotFound;

    return projectTask;



  }

  public void Delete(ProjectTask projectTask)
  {
    context.ProjectTasks.Remove(projectTask);
  }
  public async Task AddCommentAsync(TaskComment comment)
  {
    await context.TaskComments.AddAsync(comment);
  }
  public async Task<Result<TaskComment>> GetCommentByIdAsync(Guid id)
  {
    var comment = await context.TaskComments.FindAsync(id);
    if (comment is null) return TaskCommentErrors.TaskCommentNotFound;

    return comment;
  }

  public void DeleteComment(TaskComment comment)
  {
    context.TaskComments.Remove(comment);
  }
}