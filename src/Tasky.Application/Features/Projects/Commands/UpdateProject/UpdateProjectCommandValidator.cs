using FluentValidation;

namespace Tasky.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{

  public UpdateProjectCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Project Id is required.")
    .NotEqual(Guid.Empty).WithMessage("Project Id cannot be an empty GUID.");

    RuleFor(x => x.Name)
        .MaximumLength(100).WithMessage("Project name cannot exceed 100 characters.");


    RuleFor(x => x.Description)
        .MaximumLength(500).WithMessage("Project description cannot exceed 500 characters.");


  }
}