using FluentValidation;

namespace Tasky.Application.Features.ProjectTasks.Commands.UpdateStatus;

public class UpdateStatusCommadValidator : AbstractValidator<UpdateStatusCommand>
{
  public UpdateStatusCommadValidator()
  {
    RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
    RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required.");
    RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.")
    .IsInEnum().WithMessage("Invalid status value.");

  }

}