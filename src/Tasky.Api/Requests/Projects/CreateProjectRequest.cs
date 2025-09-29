using System.ComponentModel.DataAnnotations;

namespace Tasky.Api.Request.Projects;

public class CreateProjectRequest
{
  [Required(ErrorMessage = "Project name is required")]
  public string Name { get; set; }

  public string? Description { get; set; }
}