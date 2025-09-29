using MediatR;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(Guid Id, string? Name, string? Description) : IRequest<Result<Updated>>;