using MediatR;
using Companies.Domain.Entities;
using Companies.Appilcation.Features.CompanyUsers.Commands.Interfaces;

namespace Companies.Appilcation.Features.CompanyUsers.Commands.CreateCompanyUser
{
    public class CreateCompanyUserCommand : CompanyUserCommandBase, IRequest<CompanyUserVm>, ICompanyUserCommand
    {
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
