using Companies.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Companies.Domain.Entities
{
    public class Company : CompanyBase
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
