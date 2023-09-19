using FluentValidation;
using ToDo.Application.Commands.ToDoList;

namespace ToDo.Application.Validators
{
    public class CreateToDoListCommandValidator : AbstractValidator<CreateToDoListCommand>
    {
        public CreateToDoListCommandValidator()
        {
            RuleFor(x => x.toDoList.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

            RuleFor(x => x.toDoList.Value)
                .NotEmpty().WithMessage("Value is required.")
                .GreaterThan(0).WithMessage("Value is required and must be a positive number.");

        }
    }

    public class UpdateToDoListCommandValidator : AbstractValidator<UpdateToDoListCommand>
    {
        public UpdateToDoListCommandValidator()
        {
            RuleFor(x => x.inputToDoList.Title)
               .NotEmpty().WithMessage("Title is required.")
               .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

            RuleFor(x => x.inputToDoList.Value)
                .NotEmpty().WithMessage("Value is required.")
                .GreaterThan(0).WithMessage("Value is required and must be a positive number.");

        }
    }

    // You can add more validators for other command models if needed
}