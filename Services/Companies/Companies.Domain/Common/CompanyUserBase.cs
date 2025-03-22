using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Domain.Common
{
    public abstract class CompanyUserBase: EntityBase
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public bool Administrator { get; set; }
        public bool ContractorsModuleRead { get; set; }
        public bool ContractorsModuleWrite { get; set; }
        public bool ProductsModuleRead { get; set; }
        public bool ProductsModuleWrite { get; set; }
        public bool Archived { get; set; }
    }
}
