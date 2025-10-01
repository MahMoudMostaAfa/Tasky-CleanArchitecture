using MediatR;
using Tasky.Application.Features.ProjectTasks.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.ProjectTasks.Queries.GetProjectTaskById;


public record GetProjectTaskByIdQuery(Guid ProjectId, Guid TaskId) : IRequest<Result<ProjectTaskDto>>;