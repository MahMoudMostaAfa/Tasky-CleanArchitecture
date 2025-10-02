using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks.Comments;

namespace Tasky.Domain.ProjectTasks;


public interface ITaskRepository
{
  Task AddAsync(ProjectTask projectTask);

  Task<Result<ProjectTask>> GetByIdAsync(Guid id);
  Task<Result<ProjectTask>> GetWithDetailsByIdAsync(Guid id);
  void Delete(ProjectTask projectTask);

  Task AddCommentAsync(TaskComment comment);
  Task<Result<TaskComment>> GetCommentByIdAsync(Guid id);
  void DeleteComment(TaskComment comment);
}