using FluentValidation;

namespace Contractors.Application.Features.Contractors.Commands.CreateContractor
{
    public class CreateContractorCommandValidator : AbstractValidator<CreateContractorCommand>
    {
        public CreateContractorCommandValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("{Code} is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("{Code} must not exceed 30 characters.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{Name} must not exceed 100 characters.");

            RuleFor(p => p.EAN)
                .NotEmpty().WithMessage("{EAN} is required.")
                .NotNull()
                .MaximumLength(13).WithMessage("{EAN} must not exceed 13 characters.");

            RuleFor(p => p.Unit)
                .NotEmpty().WithMessage("{Unit} is required.")
                .NotNull();

            RuleFor(p => p.SaleNetPrice)
                .GreaterThan(0).WithMessage("{SaleNetPrice} must be greater then 0.");

            RuleFor(p => p.SaleGrossPrice)
                .GreaterThan(0).WithMessage("{SaleGrossPrice} must be greater then 0.");

            RuleFor(p => p.PurchaseNetPrice)
                .GreaterThan(0).WithMessage("{PurchaseNetPrice} must be greater then 0.");

            RuleFor(p => p.PurchaseGrossPrice)
                .GreaterThan(0).WithMessage("{PurchaseGrossPrice} must be greater then 0.");
        }
    }
}
