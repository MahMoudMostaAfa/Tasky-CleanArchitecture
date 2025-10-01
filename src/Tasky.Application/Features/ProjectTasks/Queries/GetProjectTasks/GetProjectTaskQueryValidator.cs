using FluentValidation;

namespace Tasky.Application.Features.ProjectTasks.Queries.GetProjectTasks;

public class GetProjectTaskQueryValidator : AbstractValidator<GetProjectTasksQuery>
{

  public GetProjectTaskQueryValidator()
  {
    RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.")
    .NotEqual(Guid.Empty).WithMessage("ProjectId must be a valid GUID.");
  }

}