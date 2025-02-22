using Documents.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Documents.Domain.Entities
{
    public class Document : DocumentBase
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public ICollection<DocumentHistory> DocumentHistories { get; set; }

    }
}
