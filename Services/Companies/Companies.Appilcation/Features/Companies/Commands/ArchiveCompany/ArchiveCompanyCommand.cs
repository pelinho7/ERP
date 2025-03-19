using MediatR;
using Companies.Appilcation.Features.Companies;
using Companies.Appilcation.Features.Companies.Commands;
using Companies.Appilcation.Features.Companies.Commands.Interfaces;

namespace Companies.Application.Features.Companies.Commands.ArchiveCompany
{
    public class ArchiveCompanyCommand : IRequest<int>, IArchiveCompanyCommand
    {
        public int Id { get; set; }
        public bool Archived { get; set; } = true;
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
