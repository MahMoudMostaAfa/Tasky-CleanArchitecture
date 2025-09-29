using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasky.Domain.Projects;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Infrastructure.Data.Configuration;


public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
  public void Configure(EntityTypeBuilder<Project> builder)
  {

    builder.HasKey(p => p.Id);
    builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
    builder.Property(p => p.Description).HasMaxLength(1000);
    builder.Property(p => p.OwnerId).IsRequired();
    builder.Property(p => p.CreatedAt).IsRequired();

    builder.HasMany(p => p.ProjectTasks).WithOne(pt => pt.Project)
    .HasForeignKey(pt => pt.ProjectId)
    .OnDelete(DeleteBehavior.Cascade);

    builder.Property(p => p.ModifiedAt).IsRequired(false);

  }
}