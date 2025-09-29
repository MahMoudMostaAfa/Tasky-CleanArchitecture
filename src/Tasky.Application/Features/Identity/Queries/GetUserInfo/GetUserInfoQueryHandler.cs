using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Identity.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Identity.Queries.GetUserInfo;

public class GetUserInfoQueryHandler(ICurrentUser currentUser, IIdentityService identityService) : IRequestHandler<GetUserInfoQuery, Result<UserDto>>
{
  public async Task<Result<UserDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
  {

    var userId = currentUser.UserId;

    if (string.IsNullOrWhiteSpace(userId)) return ApplicationErrors.UserIdClaimInvalid;

    var userResult = await identityService.GetUserByIdAsync(userId);

    if (userResult.IsError) return userResult.Errors;


    return userResult.Value;

  }
}