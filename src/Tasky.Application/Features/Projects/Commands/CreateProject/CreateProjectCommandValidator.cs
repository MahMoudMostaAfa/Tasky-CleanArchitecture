using FluentValidation;

namespace Tasky.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
  public CreateProjectCommandValidator()
  {
    RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Project name is required.")
        .MaximumLength(100).WithMessage("Project name must not exceed 100 characters.");

    RuleFor(x => x.Description)
        .MaximumLength(500).WithMessage("Project description must not exceed 500 characters.");
  }
}