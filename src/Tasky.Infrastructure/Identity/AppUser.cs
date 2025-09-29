using Microsoft.AspNetCore.Identity;

namespace Tasky.Infrastructure.Identity;

public class AppUser : IdentityUser
{

  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
};
