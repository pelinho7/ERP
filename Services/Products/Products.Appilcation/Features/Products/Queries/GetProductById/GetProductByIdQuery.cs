using MediatR;
using Products.Appilcation.Features.Products;
using System;
using System.Collections.Generic;

namespace Products.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductVm>
    {
        public GetProductByIdQuery(int companyId, int productId)
        {
            this.CompanyId = companyId;
            this.ProductId = productId;
        }

        public int CompanyId { get; set; }
        public int ProductId { get; set; }
    }
}
