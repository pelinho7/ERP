using MediatR;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Appilcation.Features.Contractors.Commands;
using Contractors.Appilcation.Features.Contractors.Commands.Interfaces;

namespace Contractors.Application.Features.Contractors.Commands.CreateContractor
{
    public class CreateContractorCommand : ContractorCommandBase,IRequest<ContractorVm>, IContractorCommand
    {
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
