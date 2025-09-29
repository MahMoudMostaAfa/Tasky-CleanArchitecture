using MediatR;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Projects.Commands.DeleteProject;



public record DeleteProjectCommand(Guid Id) : IRequest<Result<Deleted>>;

