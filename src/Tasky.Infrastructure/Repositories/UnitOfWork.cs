using Tasky.Application.Common.Interfaces;
using Tasky.Infrastructure.Data;

namespace Tasky.Infrastructure.Repositories;


public class UnitOfWork : IUnitOfWork
{

  private readonly AppDbContext _context;
  public UnitOfWork(AppDbContext context)
  {
    _context = context;
  }
  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return await _context.SaveChangesAsync(cancellationToken);
  }
}