using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Tasky.Api.OpenApi.Transformers;
using Tasky.Api.Services;
using Tasky.Application.Common.Interfaces;
namespace Tasky.Api;

public static class DependencyInjection
{

  public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
  {

    services
    .AddCustomApiVersioning()
    .AddControllersWithJsonConfigurations()
    .AddIdentityInfraStructure()
    .AddApiDocumentation();
    ;


    return services;

  }

  public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
  {
    services.AddApiVersioning(
      options =>
      {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();

      }
    );

    return services;

  }
  public static IServiceCollection AddControllersWithJsonConfigurations(this IServiceCollection services)
  {

    services.AddControllers().AddJsonOptions(
    options =>
    {
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    return services;

  }

  public static IServiceCollection AddIdentityInfraStructure(this IServiceCollection services)
  {

    services.AddScoped<ICurrentUser, CurrentUser>();
    services.AddHttpContextAccessor();

    return services;
  }

  public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
  {

    string[] versions = ["v1"];

    foreach (var version in versions)
    {
      services.AddOpenApi(version, options =>
      {

        //versioning config 

        options.AddDocumentTransformer<VersionInfoTransformer>();


        // Security Scheme config
        options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        options.AddOperationTransformer<BearerSecuritySchemeTransformer>();
      });

    }

    return services;

  }
}