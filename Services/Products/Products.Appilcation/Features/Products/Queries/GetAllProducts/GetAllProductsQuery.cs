using MediatR;
using Products.Appilcation.Features.Products;
using System;
using System.Collections.Generic;

namespace Products.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductVm>>
    {
        public GetAllProductsQuery() { }
        public GetAllProductsQuery(int companyId)
        {
            CompanyId = companyId;
        }

        public int CompanyId { get; set; }        
    }
}
