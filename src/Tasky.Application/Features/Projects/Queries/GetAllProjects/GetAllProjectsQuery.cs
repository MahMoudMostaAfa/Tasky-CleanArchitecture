using MediatR;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Projects.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Projects.Queries.GetAllProjects;


public record GetAllProjectsQuery : ICachedQuery<Result<List<ProjectDto>>>
{
  public string CacheKey => "project_key";

  public string[] Tags => ["project"];

  public TimeSpan Expiration => TimeSpan.FromMinutes(10);
}