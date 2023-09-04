using FluentValidation;
using Products.Application.Features.Products.Commands.ArchiveProduct;

namespace Products.Application.Features.Products.Commands.ArchiveProduct
{
    public class ArchiveProductCommandValidator : AbstractValidator<ArchiveProductCommand>
    {
        public ArchiveProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .GreaterThan(0).WithMessage("{Id} must be greater than 0.");
        }
    }
}
