using FluentValidation;
using NoteList.DomainLayer.Models;

namespace NoteList.ServiceLayer.ValidatorModels
{
    public class CreateRoleValidator : AbstractValidator<CreateRole>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role Name is required.")
                .NotNull().WithMessage("Role Name is required.");
        }
    }
}
