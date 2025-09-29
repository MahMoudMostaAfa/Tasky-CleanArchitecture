using Tasky.Domain.Common.Results;

namespace Tasky.Domain.Projects;

public interface IProjectRepository
{
  Task AddAsync(Project project);

  Task<Result<Project>> GetByIdAsync(Guid id);
  Task<IEnumerable<Project>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken = default);

  void Delete(Project project);

}
