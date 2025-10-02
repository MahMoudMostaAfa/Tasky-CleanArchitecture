using MediatR;
using Tasky.Application.Features.TaskComments.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.TaskComments.Queries.GetCommentsByTask;

public record GetCommentsByTaskQuery(Guid TaskId) : IRequest<Result<List<CommentDto>>>;