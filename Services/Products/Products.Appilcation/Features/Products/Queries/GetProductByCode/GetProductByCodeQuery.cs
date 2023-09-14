using MediatR;
using Products.Appilcation.Features.Products;
using System;
using System.Collections.Generic;

namespace Products.Application.Features.Products.Queries.GetProductByCode
{
    public class GetProductByCodeQuery : IRequest<ProductVm>
    {
        public GetProductByCodeQuery() { }
        public GetProductByCodeQuery(int companyId, string productCode)
        {
            this.CompanyId = companyId;
            this.ProductCode = productCode;
        }

        public int CompanyId { get; set; }
        public string ProductCode { get; set; }
    }
}
