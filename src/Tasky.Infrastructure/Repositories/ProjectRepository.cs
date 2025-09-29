using Microsoft.EntityFrameworkCore;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;
using Tasky.Infrastructure.Data;

namespace Tasky.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
  private readonly AppDbContext _context;

  public ProjectRepository(AppDbContext context)
  {
    _context = context;
  }
  public async Task AddAsync(Project project)
  {
    await _context.Projects.AddAsync(project);
  }

  public async Task<IEnumerable<Project>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken = default)
  {
    return await _context.Projects
      .Where(p => p.OwnerId == userId)
      .ToListAsync(cancellationToken);
  }

  public async Task<Result<Project>> GetByIdAsync(Guid id)
  {
    var project = await _context.Projects.FindAsync(id);

    if (project is null) return ProjectErrors.ProjectNotFound;

    return project;
  }

  public void Delete(Project project)
  {
    _context.Projects.Remove(project);

  }

}