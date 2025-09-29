using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;

namespace Tasky.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler(ICurrentUser currentUser, IProjectRepository projectRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProjectCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
  {
    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var result = Project.Create(
      name: request.Name,
      description: request.Description,
      ownerId: userId
    );
    await projectRepository.AddAsync(result.Value);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return result.Value.Id;


  }
}