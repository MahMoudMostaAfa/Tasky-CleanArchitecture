using MediatR;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks.Enums;

namespace Tasky.Application.Features.ProjectTasks.Commands.UpdateStatus;


public record UpdateStatusCommand(Guid ProjectId, Guid TaskId, ProjectTaskStatus Status) : IRequest<Result<Updated>>;