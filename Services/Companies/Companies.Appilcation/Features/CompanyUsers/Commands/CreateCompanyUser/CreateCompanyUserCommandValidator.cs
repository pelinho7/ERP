using Companies.Appilcation.Features.CompanyUsers.Commands.CreateCompanyUser;
using FluentValidation;

namespace Companies.Application.Features.Companies.Commands.CreateCompanyUser
{
    public class CreateCompanyUserCommandValidator : AbstractValidator<CreateCompanyUserCommand>
    {
        public CreateCompanyUserCommandValidator()
        {
            
        }
    }
}
