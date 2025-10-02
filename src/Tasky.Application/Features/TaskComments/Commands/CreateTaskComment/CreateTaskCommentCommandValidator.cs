using FluentValidation;

namespace Tasky.Application.Features.TaskComments.Commands.CreateTaskComment;

public class CreateTaskCommentCommandValidator : AbstractValidator<CreateTaskCommentCommand>
{
  public CreateTaskCommentCommandValidator()
  {
    RuleFor(x => x.TaskId).NotEmpty();
    RuleFor(x => x.Content).NotEmpty().MaximumLength(500);
  }
}