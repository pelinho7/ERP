using AutoMapper;
using MediatR;
using Products.Appilcation.Features.Products;
using Products.Application.Contracts.Persistence;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Features.Products.Queries.GetProductByCode
{
    public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, ProductVm>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByCodeQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductVm> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByCode(request.ProductCode,request.CompanyId);
            return _mapper.Map<ProductVm>(product);
        }
    }
}
