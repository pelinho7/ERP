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

namespace Products.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductHistoryRepository _productHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository
            , IProductHistoryRepository productHistoryRepository
            , IMapper mapper, ILogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productHistoryRepository = productHistoryRepository ?? throw new ArgumentNullException(nameof(productHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _productRepository.GetByIdAsync(request.Id);
            if (productToUpdate == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            
            _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));

            await _productRepository.UpdateAsync(productToUpdate);

            var productHistoryEntity = _mapper.Map<ProductHistory>(productToUpdate);
            await _productHistoryRepository.AddAsync(productHistoryEntity);

            _logger.LogInformation($"Product {productToUpdate.Id} is successfully updated.");

            return _mapper.Map<ProductVm>(productToUpdate);
        }
    }
}
