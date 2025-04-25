using FluentValidation;

namespace Companies.Appilcation.Features.CompanyUsers.Commands.ArchiveCompanyUser
{
    public class ArchiveCompanyUserCommandValidator : AbstractValidator<ArchiveCompanyUserCommand>
    {
        public ArchiveCompanyUserCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .GreaterThan(0).WithMessage("{Id} must be greater than 0.");
        }
    }
}
