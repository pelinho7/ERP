using Products.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Domain.Entities
{
    public class ProductHistory : ProductBase
    {
        public int CreatedModifiedBy { get; set; }
        public DateTime CreatedModifiedDate { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
