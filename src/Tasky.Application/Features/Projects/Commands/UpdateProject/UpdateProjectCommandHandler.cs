using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;

namespace Tasky.Application.Features.Projects.Commands.UpdateProject;


public class UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository, ICurrentUser currentUser) : IRequestHandler<UpdateProjectCommand, Result<Updated>>
{
  public async Task<Result<Updated>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
  {
    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var projectResult = await projectRepository.GetByIdAsync(request.Id);
    if (projectResult.IsError) return projectResult.Errors;

    var project = projectResult.Value;
    if (project.OwnerId != userId) return ApplicationErrors.UserNotAuthorized;

    var result = project.Update(request.Name, request.Description);

    if (result.IsError) return result.Errors;

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return Result.Updated;
  }
}