using MediatR;
using Tasky.Application.Features.Projects.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Projects.Queries.GetProjectById;


public record GetProjectByIdQuery(Guid Id) : IRequest<Result<ProjectDto>>;