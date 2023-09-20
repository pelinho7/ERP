using FluentValidation;
using Contractors.Application.Features.Contractors.Commands.ArchiveContractor;

namespace Contractors.Application.Features.Contractors.Commands.ArchiveContractor
{
    public class ArchiveContractorCommandValidator : AbstractValidator<ArchiveContractorCommand>
    {
        public ArchiveContractorCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .GreaterThan(0).WithMessage("{Id} must be greater than 0.");
        }
    }
}
