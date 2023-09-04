using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Appilcation.Features.Products;
using Products.Application.Contracts.Persistence;
using Products.Application.Exceptions;
using Products.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Features.Products.Commands.ArchiveProduct
{
    public class ArchiveProductCommandHandler : IRequestHandler<ArchiveProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductHistoryRepository _productHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ArchiveProductCommandHandler> _logger;

        public ArchiveProductCommandHandler(IProductRepository productRepository
            , IProductHistoryRepository productHistoryRepository
            , IMapper mapper, ILogger<ArchiveProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productHistoryRepository = productHistoryRepository ?? throw new ArgumentNullException(nameof(productHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(ArchiveProductCommand request, CancellationToken cancellationToken)
        {
            var productToArchive = await _productRepository.GetByIdAsync(request.Id);
            if (productToArchive == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            
            _mapper.Map(request, productToArchive, typeof(ArchiveProductCommand), typeof(Product));

            await _productRepository.UpdateAsync(productToArchive);

            var productHistoryEntity = _mapper.Map<ProductHistory>(productToArchive);
            await _productHistoryRepository.AddAsync(productHistoryEntity);

            _logger.LogInformation($"Product {productToArchive.Id} is successfully archived.");

            return productToArchive.Id;
        }
    }
}
