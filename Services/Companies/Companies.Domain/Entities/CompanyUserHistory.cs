using Companies.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Domain.Entities
{
    public class CompanyUserHistory:CompanyUserBase
    {
        public int CreatedModifiedBy { get; set; }
        public DateTime CreatedModifiedDate { get; set; }

        public int CompanyUserId { get; set; }
        public CompanyUser CompanyUser { get; set; }
    }
}
