using MediatR;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.ProjectTasks.Commands.DeleteProjectTask;


public record DeleteProjectTaskCommand(Guid ProjectId, Guid TaskId) : IRequest<Result<Deleted>>;