using FluentValidation;

namespace Tasky.Application.Features.TaskComments.Commands.UpdateTaskComment;

public class UpdateTaskCommentCommandValidator : AbstractValidator<UpdateTaskCommentCommand>
{
  public UpdateTaskCommentCommandValidator()
  {
    RuleFor(x => x.Content)
        .NotEmpty().WithMessage("Content is required.")
        .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");

    RuleFor(x => x.TaskId)
        .NotEmpty().WithMessage("TaskId is required.");

    RuleFor(X => X.CommentId)
         .NotEmpty().WithMessage("CommentId is required.");
  }
}