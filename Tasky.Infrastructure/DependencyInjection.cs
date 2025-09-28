using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tasky.Application.Common.Interfaces;
using Tasky.Infrastructure.Data;
using Tasky.Infrastructure.Identity;

namespace Microsoft.Extensions.DependencyInjection;


public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    ArgumentNullException.ThrowIfNullOrEmpty(connectionString);


    // register AppDbContext
    services.AddDbContext<AppDbContext>(options =>
    {
      options.UseSqlServer(connectionString);
    });

    // register identity 

    services.AddIdentityCore<AppUser>(options =>
    {
      options.Password.RequiredLength = 6;
      options.Password.RequireDigit = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireUppercase = false;
      options.Password.RequireLowercase = false;
      options.Password.RequiredUniqueChars = 1;
      options.SignIn.RequireConfirmedAccount = false;

    }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

    // register  jwt service 
    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {

      var jwtSetting = configuration.GetSection("JwtSettings");

      options.TokenValidationParameters = new TokenValidationParameters()
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSetting["Issuer"],
        ValidAudience = jwtSetting["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["Secret"]!))
      };
    });
    services.AddScoped<IIdentityService, IdentityService>();

    services.AddScoped<ITokenProvider, TokenProvider>();

    services.AddScoped<ApplicationDbContextInitialiser>();

    return services;

  }
}