using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Domain.Common
{
    public abstract class ProductBase: EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string Unit { get; set; }
        public string EAN { get; set; }
        public string PKWiU { get; set; }
        [Column(TypeName = "decimal(4,2)")]
        public decimal VatValue { get; set; }
        public int VatFlag { get; set; }
        public bool SplitPayment { get; set; }

        public string SaleCurrency { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal SaleNetPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal SaleGrossPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PurchaseNetPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PurchaseGrossPrice { get; set; }
        public string PurchaseCurrency { get; set; }
        public bool StockLevelControl { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal StockLevel { get; set; }
        public bool Archived { get; set; }
    }
}
