using Tasky.Application.Features.TaskComments.Dtos;
using Tasky.Domain.ProjectTasks.Comments;

namespace Tasky.Application.Features.TaskComments.Mappers;

public static class CommentMapper
{
  public static CommentDto ToDto(this TaskComment comment) =>
    new(comment.Id, comment.Content, comment.CreatedAt, comment.AuthorId);


  public static List<CommentDto> ToDtoList(this IEnumerable<TaskComment> comments) =>
    comments.Select(c => c.ToDto()).ToList();
}