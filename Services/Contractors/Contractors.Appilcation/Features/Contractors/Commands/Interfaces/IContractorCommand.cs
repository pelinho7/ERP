using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.Appilcation.Features.Contractors.Commands.Interfaces
{
    public interface IContractorCommand
    {
        int CompanyId { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
