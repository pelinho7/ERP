using MediatR;
using Products.Appilcation.Features.Products;
using System;
using System.Collections.Generic;

namespace Products.Application.Features.Products.Queries.CheckProductCodeNotTaken
{
    public class CheckProductCodeNotTakenQuery : IRequest<bool>
    {
        public CheckProductCodeNotTakenQuery(int companyId, string productCode, int? productId)
        {
            CompanyId = companyId;
            ProductCode = productCode;
            ProductId = productId;
        }

        public int CompanyId { get; set; }
        public string ProductCode { get; set; }
        public int? ProductId { get; set; }
    }
}
