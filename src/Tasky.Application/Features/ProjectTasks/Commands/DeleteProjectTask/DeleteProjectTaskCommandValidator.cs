using FluentValidation;

namespace Tasky.Application.Features.ProjectTasks.Commands.DeleteProjectTask;

public class DeleteProjectTaskCommandValidator : AbstractValidator<DeleteProjectTaskCommand>
{
  public DeleteProjectTaskCommandValidator()
  {
    RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId must be provided.");
    RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId must be provided.");
  }
}