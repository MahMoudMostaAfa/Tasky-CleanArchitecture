using FluentValidation;

namespace Tasky.Application.Features.TaskComments.Queries.GetCommentsByTask;

public class GetCommentsByTaskQueryValidator : AbstractValidator<GetCommentsByTaskQuery>
{
  public GetCommentsByTaskQueryValidator()
  {
    RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required.");
  }
}