using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Projects.Dtos;
using Tasky.Application.Features.Projects.Mappers;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;

namespace Tasky.Application.Features.Projects.Queries.GetAllProjects;


public class GetAllProjectsHandler(ICurrentUser currentUser, IProjectRepository projectRepository) : IRequestHandler<GetAllProjectsQuery, Result<List<ProjectDto>>>
{
  public async Task<Result<List<ProjectDto>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
  {

    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var projectsResults = await projectRepository.GetAllByUserIdAsync(userId, cancellationToken);

    return projectsResults.ToDtoList();

  }
}