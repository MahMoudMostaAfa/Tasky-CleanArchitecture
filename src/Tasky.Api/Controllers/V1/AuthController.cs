using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Users;

namespace Tasky.Api.V1.Controllers;

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
  public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
  {


    return Problem();
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest loginRequest)
  {


    return Problem();
  }


  [HttpGet("me")]
  [Authorize]
  public async Task<IActionResult> GetUserInfo()
  {




    return Problem();
  }
}

