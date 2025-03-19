using MediatR;
using Companies.Appilcation.Features.Companies;
using Companies.Appilcation.Features.Companies.Commands;
using Companies.Appilcation.Features.Companies.Commands.Interfaces;

namespace Companies.Application.Features.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : CompanyCommandBase,IRequest<CompanyVm>, IUpdateCompanyCommand
    {
        public int Id { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
