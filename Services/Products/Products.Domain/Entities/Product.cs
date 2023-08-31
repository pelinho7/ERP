using Products.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Domain.Entities
{
    public class Product : ProductBase
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public ICollection<ProductHistory> ProductHistories { get; set; }

    }
}
