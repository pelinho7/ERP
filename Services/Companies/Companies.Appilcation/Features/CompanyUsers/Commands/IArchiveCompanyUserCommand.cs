using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.CompanyUsers.Commands
{
    public interface IArchiveCompanyUserCommand
    {
        int Id { get; set; }
        bool Archived { get; set; }
        int? LastModifiedBy { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
