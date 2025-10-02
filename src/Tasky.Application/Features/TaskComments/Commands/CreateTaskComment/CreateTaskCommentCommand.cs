using MediatR;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.TaskComments.Commands.CreateTaskComment;

public record CreateTaskCommentCommand(Guid TaskId, string Content) : IRequest<Result<Guid>>;