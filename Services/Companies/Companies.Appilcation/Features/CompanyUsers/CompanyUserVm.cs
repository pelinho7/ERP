using Companies.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.CompanyUsers
{
    public class CompanyUserVm:EntityBase
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public bool Administrator { get; set; }
        public bool ContractorsModuleRead { get; set; }
        public bool ContractorsModuleWrite { get; set; }
        public bool ProductsModuleRead { get; set; }
        public bool ProductsModuleWrite { get; set; }
    }
}
