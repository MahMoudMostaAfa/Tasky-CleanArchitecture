using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using Tasky.Api;
using Tasky.Api.Services;
using Tasky.Application.Common.Interfaces;
using Tasky.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation(builder.Configuration).AddInfrastructure(builder.Configuration).AddApplication();




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();

  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/openapi/v1.json", "TASKY API V1");

    options.EnableDeepLinking();
    options.DisplayRequestDuration();
    options.EnableFilter();
  });

  app.MapScalarApiReference();


  await app.InitialiseDatabaseAsync();

}
app.UseStatusCodePages();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();



app.Run();
