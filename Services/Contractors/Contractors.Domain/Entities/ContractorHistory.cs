using Contractors.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contractors.Domain.Entities
{
    public class ContractorHistory : ContractorBase
    {
        public int CreatedModifiedBy { get; set; }
        public DateTime CreatedModifiedDate { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
