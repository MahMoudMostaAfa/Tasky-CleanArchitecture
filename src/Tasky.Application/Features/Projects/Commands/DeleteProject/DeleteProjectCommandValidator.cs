using FluentValidation;

namespace Tasky.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{

  public DeleteProjectCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Project Id is required.")
    .NotEqual(Guid.Empty).WithMessage("Project Id cannot be an empty GUID.");
  }
}

