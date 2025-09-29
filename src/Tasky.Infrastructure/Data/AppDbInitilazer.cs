using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tasky.Infrastructure.Identity;

namespace Tasky.Infrastructure.Data;


public class ApplicationDbContextInitialiser(
    ILogger<ApplicationDbContextInitialiser> logger,
    AppDbContext context, UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
  private readonly ILogger<ApplicationDbContextInitialiser> _logger = logger;
  private readonly AppDbContext _context = context;
  private readonly UserManager<AppUser> _userManager = userManager;
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;

  public async Task InitialiseAsync()
  {
    try
    {
      await _context.Database.EnsureCreatedAsync();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while initialising the database.");
      throw;
    }
  }

  public async Task SeedAsync()
  {
    try
    {
      await TrySeedAsync();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while seeding the database.");
      throw;
    }
  }

  public async Task TrySeedAsync()
  {
    // Default roles
    var managerRole = new IdentityRole("User");

    if (_roleManager.Roles.All(r => r.Name != managerRole.Name))
    {
      await _roleManager.CreateAsync(managerRole);
    }


  }
}


public static class InitialiserExtensions
{
  public static async Task InitialiseDatabaseAsync(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();

    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

    await initialiser.InitialiseAsync();

    await initialiser.SeedAsync();
  }
}