using Contractors.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contractors.Domain.Entities
{
    public class Contractor : ContractorBase
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public ICollection<ContractorHistory> ContractorHistories { get; set; }

    }
}
