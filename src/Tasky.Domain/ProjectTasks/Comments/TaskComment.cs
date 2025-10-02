using Tasky.Domain.Common;
using Tasky.Domain.Common.Results;

namespace Tasky.Domain.ProjectTasks.Comments;

public class TaskComment : Entity
{

  public string Content { get; private set; } = null!;

  public Guid TaskId { get; private set; }

  public ProjectTask ProjectTask { get; private set; } = null!;

  public string AuthorId { get; private set; } = null!;
  public DateTime CreatedAt { get; private set; }
  public DateTime? UpdatedAt { get; private set; }


  private TaskComment() { }

  private TaskComment(Guid id, string content, Guid taskId, string authorId, DateTime createdAt) : base(id)
  {
    Content = content;
    TaskId = taskId;
    AuthorId = authorId;
    CreatedAt = createdAt;
  }


  public static Result<TaskComment> Create(string content, Guid taskId, string authorId)
  {
    if (string.IsNullOrWhiteSpace(content))
    {

      return TaskCommentErrors.CommentContentEmpty;
    }
    if (taskId == Guid.Empty)
    {
      return TaskCommentErrors.TaskIdEmpty;
    }
    if (string.IsNullOrWhiteSpace(authorId))
    {
      return TaskCommentErrors.AuthorIdEmpty;
    }
    return new TaskComment(Guid.NewGuid(), content, taskId, authorId, DateTime.UtcNow);
  }



  public Result<Updated> UpdateContent(string newContent)
  {
    if (string.IsNullOrWhiteSpace(newContent))
    {
      return TaskCommentErrors.CommentContentEmpty;
    }
    Content = newContent;
    UpdatedAt = DateTime.UtcNow;
    return Result.Updated;
  }


}