using Contractors.Appilcation.Features.Contractors.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.Appilcation.Features.Contractors.Commands
{
    public interface IArchiveContractorCommand
    {
        int Id { get; set; }
        bool Archived { get; set; }
        int CompanyId { get; set; }
        int? LastModifiedBy { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
