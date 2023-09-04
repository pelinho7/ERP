using MediatR;
using Products.Appilcation.Features.Products;
using Products.Appilcation.Features.Products.Commands;
using Products.Appilcation.Features.Products.Commands.Interfaces;

namespace Products.Application.Features.Products.Commands.ArchiveProduct
{
    public class ArchiveProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public bool Archived { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
