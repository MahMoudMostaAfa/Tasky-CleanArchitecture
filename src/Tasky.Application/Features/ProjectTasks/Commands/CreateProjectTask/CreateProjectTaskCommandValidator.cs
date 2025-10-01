using System.Data;
using FluentValidation;

namespace Tasky.Application.Features.ProjectTasks.Commands.CreateProjectTask;

public class CreateProjectTaskCommandValidator : AbstractValidator<CreateProjectTaskCommand>
{


  public CreateProjectTaskCommandValidator()
  {
    RuleFor(x => x.Title)
    .NotEmpty().WithMessage("Title is required.")
    .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");


    RuleFor(x => x.Description)
      .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");


    RuleFor(x => x.ProjectId)
    .NotEmpty().WithMessage("ProjectId is required.")
    .NotEqual(Guid.Empty).WithMessage("ProjectId must be a valid GUID.");

    RuleFor(x => x.DueDateUtc)
    .Must(date => !date.HasValue || date.Value > DateTime.UtcNow.AddHours(1))
    .WithMessage("DueDateUtc must be in the future.");

    RuleFor(x => x.Priority)
    .IsInEnum().WithMessage("Priority must be a valid value.");

    RuleFor(x => x.AssignedTo)
    .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.AssignedTo)).WithMessage("AssignedTo must not exceed 100 characters.");

  }
}