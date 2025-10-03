using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
namespace Tasky.Api.OpenApi.Transformers;

internal sealed class VersionInfoTransformer : IOpenApiDocumentTransformer
{
  public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
  {
    var version = context.DocumentName;
    document.Info.Version = version;
    document.Info.Title = $"Tasky API {version}";


    return Task.CompletedTask;
  }
}