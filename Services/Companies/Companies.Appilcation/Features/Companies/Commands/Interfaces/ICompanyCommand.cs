using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.Companies.Commands.Interfaces
{
    public interface ICompanyCommand
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
