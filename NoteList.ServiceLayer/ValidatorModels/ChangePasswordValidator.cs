using FluentValidation;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.ValidatorModels
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old Password is required.")
                .Length(8, 20).WithMessage("Old Password must be between 8 and 20 characters.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required.")
                .Length(8, 20).WithMessage("New Password must be between 8 and 20 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.NewPassword).WithMessage("Password and confirmation password do not match.");
        }
    }
}
