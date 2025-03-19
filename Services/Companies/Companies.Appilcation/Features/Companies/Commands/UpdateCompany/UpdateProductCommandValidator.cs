using FluentValidation;

namespace Companies.Application.Features.Companies.Commands.UpdateCompany
{
    public class ArchiveProductCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public ArchiveProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required.")
                .NotNull()
                .MaximumLength(300).WithMessage("{Name} must not exceed 300 characters.");

            RuleFor(p => p.VatId)
                .MaximumLength(13).WithMessage("{VatId} must not exceed 13 characters.");

            RuleFor(p => p.Street)
                .MaximumLength(150).WithMessage("{Street} must not exceed 150 characters.");

            RuleFor(p => p.StreetNo)
                .MaximumLength(10).WithMessage("{StreetNo} must not exceed 10 characters.");

            RuleFor(p => p.ApartmentNo)
                .MaximumLength(10).WithMessage("{ApartmentNo} must not exceed 10 characters.");

            RuleFor(p => p.ZipCode)
                .MaximumLength(6).WithMessage("{ZipCode} must not exceed 6 characters.");

            RuleFor(p => p.PostalOffice)
                .MaximumLength(120).WithMessage("{PostalOffice} must not exceed 120 characters.");

            RuleFor(p => p.City)
                .MaximumLength(120).WithMessage("{City} must not exceed 120 characters.");

            RuleFor(p => p.Country)
                .MaximumLength(80).WithMessage("{Country} must not exceed 80 characters.");

            RuleFor(p => p.Phone)
                .MaximumLength(30).WithMessage("{Phone} must not exceed 30 characters.");

            RuleFor(p => p.Mobile)
                .MaximumLength(30).WithMessage("{Mobile} must not exceed 30 characters.");

            RuleFor(p => p.Email)
                .MaximumLength(50).WithMessage("{Email} must not exceed 50 characters.");

            RuleFor(p => p.Fax)
                .MaximumLength(30).WithMessage("{Fax} must not exceed 30 characters.");

            RuleFor(p => p.SwiftCode)
                .MaximumLength(11).WithMessage("{SwiftCode} must not exceed 11 characters.");

            RuleFor(p => p.BankAccountNumber)
                .MaximumLength(34).WithMessage("{BankAccountNumber} must not exceed 34 characters.");
        }
    }
}
