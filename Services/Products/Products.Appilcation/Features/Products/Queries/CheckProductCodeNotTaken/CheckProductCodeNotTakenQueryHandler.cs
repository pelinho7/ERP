using AutoMapper;
using MediatR;
using Products.Appilcation.Features.Products;
using Products.Application.Common;
using Products.Application.Contracts.Persistence;
using Products.Domain.Common;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Features.Products.Queries.CheckProductCodeNotTaken
{
    public class CheckProductCodeNotTakenQueryHandler : IRequestHandler<CheckProductCodeNotTakenQuery, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CheckProductCodeNotTakenQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(CheckProductCodeNotTakenQuery request, CancellationToken cancellationToken)
        {
            var expression = PredicateBuilder.True<Product>();
            expression = expression.And(x => x.CompanyId == request.CompanyId);
            expression = expression.And(x => x.Code.ToUpper() == request.ProductCode.ToUpper());
            if (request.ProductId.HasValue)
                expression = expression.And(x => x.Id!= request.ProductId.Value);
            var count = await _productRepository.CountAsync(expression);
            return count==0;
        }
    }
}
