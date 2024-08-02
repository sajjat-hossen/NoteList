using FluentValidation;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.ValidatorModels
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is Required.")
                .EmailAddress().WithMessage("Valid Email Address is Required.");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Required.")
                .Length(8, 20).WithMessage("Password must be between 8 and 20 characters.");

            RuleFor(x => x.RememberMe)
                .NotNull().WithMessage("Remember Me must be specified.");
        }
    }
}
