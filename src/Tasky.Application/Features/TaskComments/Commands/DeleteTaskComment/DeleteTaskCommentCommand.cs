using MediatR;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.TaskComments.Commands.DeleteTaskComment;

public record DeleteTaskCommentCommand(Guid TaskId, Guid CommentId) : IRequest<Result<Deleted>>;