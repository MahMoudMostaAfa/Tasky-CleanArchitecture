using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasky.Domain.Common;
using Tasky.Domain.Projects;
using Tasky.Domain.ProjectTasks;
using Tasky.Domain.ProjectTasks.Comments;
using Tasky.Infrastructure.Identity;

namespace Tasky.Infrastructure.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : IdentityDbContext<AppUser>(options)
{


  public DbSet<Project> Projects => Set<Project>();
  public DbSet<ProjectTask> ProjectTasks => Set<ProjectTask>();
  public DbSet<TaskComment> TaskComments => Set<TaskComment>();
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    await DispatchDomainEventsAsync(cancellationToken);
    return await base.SaveChangesAsync(cancellationToken);
  }

  private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
  {
    var domainEntities = ChangeTracker.Entries()
        .Where(e => e.Entity is Entity baseEntity && baseEntity.DomainEvents.Count != 0)
        .Select(e => (Entity)e.Entity)
        .ToList();

    var domainEvents = domainEntities
        .SelectMany(e => e.DomainEvents)
        .ToList();

    foreach (var domainEvent in domainEvents)
    {
      await mediator.Publish(domainEvent, cancellationToken);
    }

    foreach (var entity in domainEntities)
    {
      entity.ClearDomainEvents();
    }
  }

}