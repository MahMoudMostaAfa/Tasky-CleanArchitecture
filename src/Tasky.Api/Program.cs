using System.Text.Json.Serialization;
using Tasky.Api;
using Tasky.Api.Services;
using Tasky.Application.Common.Interfaces;
using Tasky.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation(builder.Configuration).AddInfrastructure(builder.Configuration).AddApplication();




var app = builder.Build();


if (app.Environment.IsDevelopment())
{

  await app.InitialiseDatabaseAsync();

}
app.UseStatusCodePages();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();



app.Run();
