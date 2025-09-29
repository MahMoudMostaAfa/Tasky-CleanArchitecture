using MediatR;
using Tasky.Application.Features.Identity.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Identity.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Result<string>>;


