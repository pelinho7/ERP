using MediatR;
using Products.Appilcation.Features.Products;
using Products.Appilcation.Features.Products.Commands;
using Products.Appilcation.Features.Products.Commands.Interfaces;

namespace Products.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : ProductCommandBase,IRequest<ProductVm>, IProductCommand
    {
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
