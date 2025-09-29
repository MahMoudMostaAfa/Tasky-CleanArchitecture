using MediatR;
using Tasky.Application.Features.Identity.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Identity.Queries.GetUserInfo;

public record GetUserInfoQuery : IRequest<Result<UserDto>>;