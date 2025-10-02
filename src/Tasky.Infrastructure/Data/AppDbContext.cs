using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasky.Domain.Projects;
using Tasky.Domain.ProjectTasks;
using Tasky.Domain.ProjectTasks.Comments;
using Tasky.Infrastructure.Identity;

namespace Tasky.Infrastructure.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{


  public DbSet<Project> Projects => Set<Project>();
  public DbSet<ProjectTask> ProjectTasks => Set<ProjectTask>();
  public DbSet<TaskComment> TaskComments => Set<TaskComment>();
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }


}