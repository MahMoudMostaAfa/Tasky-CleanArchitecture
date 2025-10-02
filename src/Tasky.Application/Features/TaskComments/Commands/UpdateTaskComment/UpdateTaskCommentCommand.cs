using MediatR;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.TaskComments.Commands.UpdateTaskComment;

public record UpdateTaskCommentCommand(Guid TaskId, Guid CommentId, string Content) : IRequest<Result<Updated>>;