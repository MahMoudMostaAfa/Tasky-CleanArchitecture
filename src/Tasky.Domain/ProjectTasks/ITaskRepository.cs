using Tasky.Domain.Common.Results;

namespace Tasky.Domain.ProjectTasks;


public interface ITaskRepository
{
  Task AddAsync(ProjectTask projectTask);

  Task<Result<ProjectTask>> GetByIdAsync(Guid id);
  Task<Result<ProjectTask>> GetWithDetailsByIdAsync(Guid id);
  void Delete(ProjectTask projectTask);
}