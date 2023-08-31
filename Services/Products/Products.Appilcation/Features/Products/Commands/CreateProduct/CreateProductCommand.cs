using MediatR;
using Products.Appilcation.Features.Products;
using Products.Appilcation.Features.Products.Commands;
using Products.Appilcation.Features.Products.Commands.Interfaces;

namespace Products.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : ProductCommandBase,IRequest<ProductVm>, IProductCommand
    {
        //public int CompanyId { get; set; }
        //public string Code { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public int Type { get; set; }
        //public string Unit { get; set; }
        //public string EAN { get; set; }
        //public string PKWiU { get; set; }
        //public decimal VatValue { get; set; }
        //public int VatFlag { get; set; }
        //public bool SplitPayment { get; set; }
        //public string SaleCurrency { get; set; }
        //public decimal SaleNetPrice { get; set; }
        //public decimal SaleGrossPrice { get; set; }
        //public decimal PurchaseNetPrice { get; set; }
        //public decimal PurchaseGrossPrice { get; set; }
        //public string PurchaseCurrency { get; set; }
        //public bool StockLevelControl { get; set; }
        //public decimal StockLevel { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
