namespace Tasky.Application.Features.TaskComments.Dtos;

public record CommentDto(Guid Id, string Content, DateTime CreatedAt, string AuthorId);