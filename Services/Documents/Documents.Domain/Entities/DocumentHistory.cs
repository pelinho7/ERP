using Documents.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Documents.Domain.Entities
{
    public class DocumentHistory : DocumentBase
    {
        public int CreatedModifiedBy { get; set; }
        public DateTime CreatedModifiedDate { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
