using MediatR;
using Companies.Appilcation.Features.Companies;
using Companies.Appilcation.Features.Companies.Commands;
using Companies.Appilcation.Features.Companies.Commands.Interfaces;
using Companies.Domain.Entities;

namespace Companies.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : CompanyCommandBase,IRequest<CompanyVm>, ICompanyCommand
    {
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
