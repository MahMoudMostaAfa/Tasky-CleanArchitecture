using FluentValidation;

namespace Tasky.Application.Features.TaskComments.Queries.GetCommentById;

public class GetCommentByIdQueryValidator : AbstractValidator<GetCommentByIdQuery>
{
  public GetCommentByIdQueryValidator()
  {

    RuleFor(x => x.TaskId).NotEmpty().WithMessage("Task ID is required.");
    RuleFor(x => x.CommentId).NotEmpty().WithMessage("Comment ID is required");
  }

}