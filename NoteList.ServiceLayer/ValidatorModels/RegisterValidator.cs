using FluentValidation;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.ValidatorModels
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is Required.")
                .EmailAddress().WithMessage("Valid Email Address is Required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Required.")
                .Length(8, 20).WithMessage("Password must be between 8 and 20 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Password and confirmation password do not match.");
        }
    }
}
