using System.Text.Json.Serialization;
using Tasky.Api.Services;
using Tasky.Application.Common.Interfaces;
using Tasky.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(
  options =>
  {
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  }
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{

  await app.InitialiseDatabaseAsync();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");


app.Run();
