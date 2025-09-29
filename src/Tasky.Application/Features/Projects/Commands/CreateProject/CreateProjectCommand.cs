using MediatR;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Projects.Commands.CreateProject;

public record CreateProjectCommand(string Name, string? Description) : IRequest<Result<Guid>>;