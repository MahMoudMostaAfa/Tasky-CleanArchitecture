using MediatR;
using Tasky.Application.Features.TaskComments.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.TaskComments.Queries.GetCommentById;

public record GetCommentByIdQuery(Guid TaskId, Guid CommentId) : IRequest<Result<CommentDto>>;