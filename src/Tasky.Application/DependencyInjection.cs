using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tasky.Application.Common.Behaviours;


public static class DependencyInjection
{

  public static IServiceCollection AddApplication(this IServiceCollection services)
  {

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));

    });

    return services;
  }

}