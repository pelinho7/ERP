using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Application.Contracts.Persistence;
using Products.Application.Exceptions;
using Products.Application.Features.Orders.Commands.CreateProduct;
using Products.Application.Features.Products.Commands.ArchiveProduct;
using Products.Application.Features.Products.Commands.CreateProduct;
using Products.Application.Features.Products.Commands.UpdateProduct;
using Products.Application.Mappings;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Products.UnitTest.Application
{
    public class ArchiveProductTest
    {
        Mock<IProductRepository> _mockProductRepository;
        IProductHistoryRepository _productHistoryRepository;
        IMapper _mapper;
        ILogger<ArchiveProductCommandHandler> _logger;
        ArchiveProductCommand _command;

        private void setUp()
        {
            _command = new ArchiveProductCommand();
            _command.Id = 1;
            _command.CompanyId = 1;
            _command.Archived = true;

            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(x => x.UpdateAsync(It.IsAny<Product>()));
            _mockProductRepository.Setup(x => x.GetProductById(_command.Id, _command.CompanyId)).ReturnsAsync(new Product());

            _productHistoryRepository = (new Mock<IProductHistoryRepository>()).Object;
            _mapper = (new Mock<IMapper>()).Object;
            _logger = (new Mock<ILogger<ArchiveProductCommandHandler>>()).Object;


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void ArchiveProduct()
        {
            setUp();

            var handler = new ArchiveProductCommandHandler(_mockProductRepository.Object, _productHistoryRepository, _mapper, _logger);
            var productId = await handler.Handle(_command, default);
            Assert.Equal(productId, _command.Id);
        }

        [Fact]
        public async void ThrowErrorWhenProductNotExist()
        {
            setUp();
            _mockProductRepository.Setup(x => x.GetProductById(_command.Id, _command.CompanyId)).ReturnsAsync((Product)null);

            var handler = new ArchiveProductCommandHandler(_mockProductRepository.Object, _productHistoryRepository, _mapper, _logger);
            await Assert.ThrowsAsync(typeof(NotFoundException), (async () =>
            {
                var newProduct = await handler.Handle(_command, default);
            }));
        }
    }
}
