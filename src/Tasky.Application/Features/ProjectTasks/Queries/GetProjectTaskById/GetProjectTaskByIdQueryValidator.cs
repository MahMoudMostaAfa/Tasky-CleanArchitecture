using FluentValidation;

namespace Tasky.Application.Features.ProjectTasks.Queries.GetProjectTaskById;


public class GetProjectTaskByIdQueryValidator : AbstractValidator<GetProjectTaskByIdQuery>
{

  public GetProjectTaskByIdQueryValidator()
  {
    RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
    RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required.");

  }
}