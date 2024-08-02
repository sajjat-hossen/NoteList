using FluentValidation;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.ValidatorModels
{
    public class NoteValidator : AbstractValidator<NoteViewModel>
    {
        public NoteValidator()
        {
            RuleFor(note => note.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(note => note.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
        }
    }
}
