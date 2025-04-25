using MediatR;
using Companies.Domain.Entities;
using Companies.Appilcation.Features.CompanyUsers.Commands.Interfaces;

namespace Companies.Appilcation.Features.CompanyUsers.Commands.UpdateCompanyUser
{
    public class UpdateCompanyUserCommand : CompanyUserCommandBase, IRequest<CompanyUserVm>, IUpdateCompanyUserCommand
    {
        public int Id { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
