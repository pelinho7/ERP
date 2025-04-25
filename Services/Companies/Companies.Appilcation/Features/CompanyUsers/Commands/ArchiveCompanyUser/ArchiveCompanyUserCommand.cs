using MediatR;

namespace Companies.Appilcation.Features.CompanyUsers.Commands.ArchiveCompanyUser
{
    public class ArchiveCompanyUserCommand : IRequest<int>, IArchiveCompanyUserCommand
    {
        public int Id { get; set; }
        public bool Archived { get; set; } = true;
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
