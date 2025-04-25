using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.CompanyUsers.Commands.Interfaces
{
    public interface ICompanyUserCommand
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
