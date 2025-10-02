using Tasky.Domain.Common.Results;

namespace Tasky.Domain.ProjectTasks.Comments;

public static class TaskCommentErrors
{
  public static Error CommentContentEmpty => Error.Validation("TaskComment.Content", "Comment content cannot be empty.");

  public static Error TaskCommentNotFound => Error.NotFound("TaskComment.NotFound", "The specified task comment was not found.");

  public static Error TaskIdEmpty => Error.Validation("TaskComment.TaskId", "The Task ID cannot be empty.");
  public static Error AuthorIdEmpty => Error.Validation("TaskComment.AuthorId", "The Author ID cannot be empty.");

}