using FluentValidation;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.ValidatorModels
{
    public class TodoListValidator : AbstractValidator<TodoListViewModel>
    {
        public TodoListValidator()
        {
            RuleFor(todo => todo.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(todo => todo.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
        }
    }
}
