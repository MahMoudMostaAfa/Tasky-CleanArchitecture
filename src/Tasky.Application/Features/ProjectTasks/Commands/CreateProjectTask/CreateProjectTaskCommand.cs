using MediatR;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks.Enums;

namespace Tasky.Application.Features.ProjectTasks.Commands.CreateProjectTask;


public record CreateProjectTaskCommand(
  Guid ProjectId,
  string Title,
  string? Description,
  DateTime? DueDateUtc,
  ProjectTaskPriority? Priority,
  string? AssignedTo
) : IRequest<Result<Guid>>;