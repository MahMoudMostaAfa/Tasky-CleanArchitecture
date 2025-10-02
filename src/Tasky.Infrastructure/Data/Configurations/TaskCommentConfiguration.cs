using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasky.Domain.ProjectTasks.Comments;

namespace Tasky.Infrastructure.Data.Configurations;


public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
{
  public void Configure(EntityTypeBuilder<TaskComment> builder)
  {
    builder.HasKey(tc => tc.Id);
    builder.Property(tc => tc.Content).IsRequired().HasMaxLength(1000);
    builder.Property(tc => tc.AuthorId).IsRequired();
    builder.Property(tc => tc.CreatedAt).IsRequired();
    builder.Property(tc => tc.UpdatedAt).IsRequired(false);

    builder.HasOne(tc => tc.ProjectTask)
    .WithMany(pt => pt.Comments)
    .HasForeignKey(tc => tc.TaskId)
    .OnDelete(DeleteBehavior.Cascade);

  }
}