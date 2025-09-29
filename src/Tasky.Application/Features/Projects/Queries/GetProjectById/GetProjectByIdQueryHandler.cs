using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Projects.Dtos;
using Tasky.Application.Features.Projects.Mappers;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;

namespace Tasky.Application.Features.Projects.Queries.GetProjectById;


public class GetProjectByIdQueryHandler(ICurrentUser currentUser, IProjectRepository projectRepository) : IRequestHandler<GetProjectByIdQuery, Result<ProjectDto>>
{
  public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
  {
    var userId = currentUser.UserId;

    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var ProjectResult = await projectRepository.GetByIdAsync(request.Id);





    if (ProjectResult.IsError)
    {
      return ProjectResult.Errors;
    }

    return ProjectResult.Value.ToDto();


  }
}