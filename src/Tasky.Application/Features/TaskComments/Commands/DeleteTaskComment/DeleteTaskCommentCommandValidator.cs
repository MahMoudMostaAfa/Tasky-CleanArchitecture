using FluentValidation;

namespace Tasky.Application.Features.TaskComments.Commands.DeleteTaskComment;

public class DeleteTaskCommentCommandValidator : AbstractValidator<DeleteTaskCommentCommand>
{
  public DeleteTaskCommentCommandValidator()
  {
    RuleFor(x => x.TaskId).NotEmpty();
    RuleFor(x => x.CommentId).NotEmpty();
  }
}