using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.CompanyUsers.Commands
{
    public interface IUpdateCompanyUserCommand
    {
        int Id { get; set; }
        int? LastModifiedBy { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
