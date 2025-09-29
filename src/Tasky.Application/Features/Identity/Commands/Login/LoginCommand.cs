using MediatR;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Identity.Commands.Login;


public record LoginCommand(string Email, string Password) : IRequest<Result<string>>;