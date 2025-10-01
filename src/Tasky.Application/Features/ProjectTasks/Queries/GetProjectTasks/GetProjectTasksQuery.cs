using MediatR;
using Tasky.Application.Features.ProjectTasks.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.ProjectTasks.Queries.GetProjectTasks;


public record GetProjectTasksQuery(Guid ProjectId) : IRequest<Result<List<ProjectTaskDto>>>;