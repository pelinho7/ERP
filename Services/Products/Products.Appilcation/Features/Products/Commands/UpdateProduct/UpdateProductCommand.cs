using MediatR;
using Products.Appilcation.Features.Products;
using Products.Appilcation.Features.Products.Commands;
using Products.Appilcation.Features.Products.Commands.Interfaces;

namespace Products.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : ProductCommandBase,IRequest<ProductVm>, IUpdateProductCommand
    {
        public int Id { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
