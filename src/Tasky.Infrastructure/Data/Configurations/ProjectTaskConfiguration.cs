using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Infrastructure.Data.Configurations;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
  public void Configure(EntityTypeBuilder<ProjectTask> builder)
  {
    builder.HasKey(pt => pt.Id);
    builder.Property(pt => pt.Title).IsRequired().HasMaxLength(200);
    builder.Property(pt => pt.Description).HasMaxLength(1000);
    builder.Property(pt => pt.ProjectId).IsRequired();
    builder.Property(pt => pt.Status).HasConversion<string>().IsRequired();
    builder.Property(pt => pt.Priority).HasConversion<string>().IsRequired();
    builder.Property(pt => pt.CreatedAt).IsRequired();
    builder.Property(pt => pt.ModifiedAt).IsRequired(false);
    builder.Property(pt => pt.DueDateUtc).IsRequired(false);
    builder.Property(pt => pt.AssignedTo).IsRequired(false);
  }
}