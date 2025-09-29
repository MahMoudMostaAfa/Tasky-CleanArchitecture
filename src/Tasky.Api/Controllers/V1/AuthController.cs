using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Identity;
using Tasky.Application.Features.Identity.Commands.Login;
using Tasky.Application.Features.Identity.Commands.Register;
using Tasky.Application.Features.Identity.Queries.GetUserInfo;


namespace Tasky.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]

public class AuthController : ApiController
{
  private readonly ISender _sender;

  public AuthController(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand, CancellationToken ct = default)
  {
    var result = await _sender.Send(registerCommand, ct);

    return result.Match(
     token => Ok(new { Token = token }),
     errors => Problem(errors)

    );
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand, CancellationToken ct = default)
  {
    var result = await _sender.Send(loginCommand, ct);


    return result.Match(
      token => Ok(new { Token = token }),
      errors => Problem(errors)
    );


  }


  [HttpGet("me")]
  [Authorize]
  public async Task<IActionResult> GetUserInfo()
  {
    var GetUserInfoQuery = new GetUserInfoQuery();
    var result = await _sender.Send(GetUserInfoQuery);

    return result.Match(
      user => Ok(user),
      errors => Problem(errors)
    );
  }
}

