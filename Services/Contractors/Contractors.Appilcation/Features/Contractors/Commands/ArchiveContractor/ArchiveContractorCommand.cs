using MediatR;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Appilcation.Features.Contractors.Commands;
using Contractors.Appilcation.Features.Contractors.Commands.Interfaces;

namespace Contractors.Application.Features.Contractors.Commands.ArchiveContractor
{
    public class ArchiveContractorCommand : IRequest<int>, IArchiveContractorCommand
    {
        public int Id { get; set; }
        public bool Archived { get; set; } = true;
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int CompanyId { get; set; }
    }
}
