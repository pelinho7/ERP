using Companies.Appilcation.Features.CompanyUsers.Commands.UpdateCompanyUser;
using FluentValidation;

namespace Companies.Application.Features.Companies.Commands.UpdateCompanyUser
{
    public class UpdateCompanyUserCommandValidator : AbstractValidator<UpdateCompanyUserCommand>
    {
        public UpdateCompanyUserCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("{Id} was not provided.");

            RuleFor(p => p.CompanyId)
                .GreaterThan(0).WithMessage("{CompanyId} was not provided.");

            RuleFor(p => p.UserId)
                .GreaterThan(0).WithMessage("{UserId} was not provided.");
        }
    }
}
