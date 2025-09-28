using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Users;

namespace Tasky.Api.Controllers;


[Route("api/auth")]
public class AuthController(IIdentityService _identityService, ITokenProvider _tokenProvider, ICurrentUser _currentUser) : ApiController
{

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
  {
    Console.WriteLine(registerUserRequest);

    var result = await _identityService.RegisterUserAsync(registerUserRequest);
    Console.WriteLine(result.Value);
    if (result.IsSuccess)
    {
      var token = _tokenProvider.GenerateToken(result.Value);
      return Ok(new { Token = token });
    }

    return Problem();
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest loginRequest)
  {
    var result = await _identityService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);

    if (result.IsSuccess)
    {
      var token = _tokenProvider.GenerateToken(result.Value);
      return Ok(new { Token = token });
    }

    return Problem();
  }


  [HttpGet("me")]
  [Authorize]
  public async Task<IActionResult> GetUserInfo()
  {

    var currentUser = _currentUser.UserId;

    if (currentUser == null) return Unauthorized();

    var result = await _identityService.GetUserByIdAsync(currentUser);
    if (result.IsSuccess)
    {
      return Ok(result.Value);
    }

    return Problem();
  }
}

