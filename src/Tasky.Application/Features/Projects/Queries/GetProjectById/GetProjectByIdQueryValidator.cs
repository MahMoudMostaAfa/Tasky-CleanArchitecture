using FluentValidation;

namespace Tasky.Application.Features.Projects.Queries.GetProjectById;


public class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
{
  public GetProjectByIdQueryValidator()
  {
    RuleFor(x => x.Id)
      .NotEmpty().WithMessage("Project Id is required.")
      .NotEqual(Guid.Empty).WithMessage("Project Id must be a valid GUID.");
  }
}