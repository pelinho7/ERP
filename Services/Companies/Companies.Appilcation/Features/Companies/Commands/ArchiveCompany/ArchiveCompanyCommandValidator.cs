using FluentValidation;
using Companies.Application.Features.Companies.Commands.ArchiveCompany;

namespace Companies.Application.Features.Companies.Commands.ArchiveCompany
{
    public class ArchiveCompanyCommandValidator : AbstractValidator<ArchiveCompanyCommand>
    {
        public ArchiveCompanyCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .GreaterThan(0).WithMessage("{Id} must be greater than 0.");
        }
    }
}
