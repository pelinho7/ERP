using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Appilcation.Features.Products;
using Products.Application.Contracts.Persistence;
using Products.Application.Features.Products.Commands.CreateProduct;
using Products.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Features.Orders.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVm>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductHistoryRepository _productHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository
            ,IProductHistoryRepository productHistoryRepository, IMapper mapper
            , ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productHistoryRepository = productHistoryRepository ?? throw new ArgumentNullException(nameof(productHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductVm> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);
            var count = await _productRepository.CountProductsByCode(request.CompanyId, request.Code, null);
            if(count > 0)
            {
                throw new Exception($"Product with the code {request.Code} already exists.");
            }
            var newProduct = await _productRepository.AddAsync(productEntity);
            
            var productHistoryEntity = _mapper.Map<ProductHistory>(newProduct);
            await _productHistoryRepository.AddAsync(productHistoryEntity);

            _logger.LogInformation($"Product {newProduct.Id} is successfully created.");
            
            return _mapper.Map<ProductVm>(newProduct);
        }
    }
}
