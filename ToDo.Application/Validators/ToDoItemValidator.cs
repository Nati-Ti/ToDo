using FluentValidation;
using ToDo.Application.Commands.ToDoItem;

namespace ToDo.Application.Validators
{
    public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
    {
        public CreateToDoItemCommandValidator()
        {
            RuleFor(x => x.inputToDoItem.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.inputToDoItem.ToDoId)
                .NotEmpty().WithMessage("ToDoId is required.");

            RuleFor(x => x.inputToDoItem.Value)
                .NotEmpty().WithMessage("Value is required.")
                .GreaterThan(0).WithMessage("Value must be greater than 0.");
        }
    }

    public class UpdateToDoItemProgressCommandValidator : AbstractValidator<UpdateToDoItemProgressCommand>
    {
        public UpdateToDoItemProgressCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.inputToItem.Progress)
                .NotEmpty().WithMessage("Progress is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Progress must be greater than or equal to 0.");
        }
    }

    public class UpdateToDoItemPropertyCommandValidator : AbstractValidator<UpdateToDoItemPropertyCommand>
    {
        public UpdateToDoItemPropertyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.inputToDoItem.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.inputToDoItem.ToDoId)
                .NotEmpty().WithMessage("ToDoId is required.");
        }
    }
}