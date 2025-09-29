using MediatR;
using Tasky.Application.Features.Projects.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Projects.Queries.GetAllProjects;


public record GetAllProjectsQuery : IRequest<Result<List<ProjectDto>>>;
