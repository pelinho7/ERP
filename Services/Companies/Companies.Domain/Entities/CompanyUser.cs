using Companies.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Domain.Entities
{
    public class CompanyUser: CompanyUserBase
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }


        public Company Company { get; set; }
        public ICollection<CompanyUserHistory> CompanyUserHistories { get; set; }

    }
}
