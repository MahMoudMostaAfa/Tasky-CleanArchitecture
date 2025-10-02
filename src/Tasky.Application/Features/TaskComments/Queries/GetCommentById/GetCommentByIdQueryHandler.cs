using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.TaskComments.Dtos;
using Tasky.Application.Features.TaskComments.Mappers;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Application.Features.TaskComments.Queries.GetCommentById;

public class GetCommentByIdQueryHandler(ICurrentUser currentUser, ITaskRepository taskRepository) : IRequestHandler<GetCommentByIdQuery, Result<CommentDto>>
{
  public async Task<Result<CommentDto>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
  {
    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var comment = await taskRepository.GetCommentByIdAsync(request.CommentId);
    if (comment.IsError) return comment.Errors;

    return comment.Value.ToDto();
  }
}