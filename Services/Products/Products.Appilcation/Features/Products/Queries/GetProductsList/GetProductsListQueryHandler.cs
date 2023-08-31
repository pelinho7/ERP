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

namespace Products.Application.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, PagedList<ProductVm>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedList<ProductVm>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProductsByCompanyId(request.CompanyId
                , request.ProductName, request.PageNumber, request.PageSize);
            var count = await _productRepository.CountProducts(request.CompanyId
                , request.ProductName);
            var items = _mapper.Map<List<ProductVm>>(productList);

            return new PagedList<ProductVm>(items, count, request.PageNumber, request.PageSize);
        }
    }
}
