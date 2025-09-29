using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;

namespace Tasky.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler(ICurrentUser currentUser, IProjectRepository projectRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProjectCommand,
 Result<Deleted>>
{
  public async Task<Result<Deleted>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
  {
    var UserId = currentUser.UserId;

    if (UserId is null) return ApplicationErrors.UserIdClaimInvalid;


    var projectResult = await projectRepository.GetByIdAsync(request.Id);

    if (projectResult.IsError) return projectResult.Errors;

    var project = projectResult.Value;

    if (project.OwnerId != UserId) return ProjectErrors.UserNotAuthorized;

    projectRepository.Delete(project);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return Result.Deleted;
  }
}