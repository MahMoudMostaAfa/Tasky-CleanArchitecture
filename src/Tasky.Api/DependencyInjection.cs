using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Tasky.Api.Infrastructure;
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
    .AddApiDocumentation()
    .AddConfiguredCors()
    .AddAppRateLimiting()
    .AddExceptionHandling()
    .AddCustomProblemDetails();


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
  public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
  {
    services.AddProblemDetails(options => options.CustomizeProblemDetails = (context) =>
    {
      context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
      context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);
    });

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

  public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
  {
    services.AddExceptionHandler<GlobalExceptionHandler>();
    return services;
  }
  public static IServiceCollection AddConfiguredCors(this IServiceCollection services)
  {

    services.AddCors(options => options.AddPolicy(
       "All",
        policy => policy
            .WithOrigins("https://localhost:7017")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()));

    return services;
  }
  public static IServiceCollection AddAppRateLimiting(this IServiceCollection services)
  {
    services.AddRateLimiter(options =>
    {
      options.AddSlidingWindowLimiter("SlidingWindow", limiterOptions =>
          {
            limiterOptions.PermitLimit = 100;
            limiterOptions.Window = TimeSpan.FromMinutes(1);
            limiterOptions.SegmentsPerWindow = 6;
            limiterOptions.QueueLimit = 10;
            limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            limiterOptions.AutoReplenishment = true;
          });

      options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    });

    return services;
  }
  public static IApplicationBuilder UseCoreMiddleWares(this IApplicationBuilder app)
  {

    app.UseExceptionHandler();

    // 2. Status code pages for handling HTTP status codes
    app.UseStatusCodePages();

    // 3. HTTPS redirection (before any other middleware that might generate URLs)
    app.UseHttpsRedirection();


    // 5. CORS (before authentication/authorization)
    app.UseCors("All");

    // 6. Rate limiting (before authentication to protect auth endpoints)
    app.UseRateLimiter();

    // 7. Authentication (must come before authorization)
    app.UseAuthentication();

    // 8. Authorization (must come after authentication)
    app.UseAuthorization();


    return app;
  }
}