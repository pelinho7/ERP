using MediatR;
using Products.Appilcation.Features.Products;
using System;
using System.Collections.Generic;

namespace Products.Application.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQuery : IRequest<PagedList<ProductVm>>
    {
        public GetProductsListQuery(int companyId, string productName, int pageNumber, int pageSize)
        {
            CompanyId = companyId;
            ProductName = productName;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int CompanyId { get; set; }
        public string ProductName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        
    }
}
