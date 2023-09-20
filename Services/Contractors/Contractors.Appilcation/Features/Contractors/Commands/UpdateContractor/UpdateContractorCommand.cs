using MediatR;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Appilcation.Features.Contractors.Commands;
using Contractors.Appilcation.Features.Contractors.Commands.Interfaces;

namespace Contractors.Application.Features.Contractors.Commands.UpdateContractor
{
    public class UpdateContractorCommand : ContractorCommandBase, IRequest<ContractorVm>, IUpdateContractorCommand
    {
        public int Id { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
