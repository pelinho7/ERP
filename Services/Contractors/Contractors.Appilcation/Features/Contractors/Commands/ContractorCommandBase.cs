using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.Appilcation.Features.Contractors.Commands
{
    public abstract class ContractorCommandBase
    {
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string Unit { get; set; }
        public string EAN { get; set; }
        public string PKWiU { get; set; }
        public decimal VatValue { get; set; }
        public int VatFlag { get; set; }
        public bool SplitPayment { get; set; }
        public string SaleCurrency { get; set; }
        public decimal SaleNetPrice { get; set; }
        public decimal SaleGrossPrice { get; set; }
        public decimal PurchaseNetPrice { get; set; }
        public decimal PurchaseGrossPrice { get; set; }
        public string PurchaseCurrency { get; set; }
        public bool StockLevelControl { get; set; }
        public decimal StockLevel { get; set; }
    }
}
