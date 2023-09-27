using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Application.Contracts.Persistence;
using Products.Application.Exceptions;
using Products.Application.Features.Orders.Commands.CreateProduct;
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
    public class UpdateProductTest
    {
        Mock<IProductRepository> _mockProductRepository;
        IProductHistoryRepository _productHistoryRepository;
        IMapper _mapper;
        ILogger<UpdateProductCommandHandler> _logger;
        UpdateProductCommand _command;

        private void setUp()
        {
            _command = new UpdateProductCommand();
            _command.Id = 1;
            _command.CompanyId = 1;
            _command.Code = "Code";
            _command.Name = "Name";
            _command.Type = 1;
            _command.Description = "Description";
            _command.Unit = "Unit";
            _command.EAN = "13421432";
            _command.PKWiU = "213421341";
            _command.VatValue = 23;
            _command.VatFlag = 1;
            _command.SaleCurrency = "PLN";
            _command.SaleNetPrice = 10;
            _command.PurchaseGrossPrice = (decimal)12.3;
            _command.PurchaseCurrency = "PLN";
            _command.PurchaseNetPrice = 10;
            _command.PurchaseGrossPrice = (decimal)12.3;
            _command.LastModifiedBy = 1;
            _command.LastModifiedDate = DateTime.Now;
            _command.StockLevelControl = false;
            _command.StockLevel = 0;

            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(x => x.UpdateAsync(It.IsAny<Product>()));
            _mockProductRepository.Setup(x => x.GetProductById(_command.Id, _command.CompanyId)).ReturnsAsync(new Product());

            _productHistoryRepository = (new Mock<IProductHistoryRepository>()).Object;
            _mapper = (new Mock<IMapper>()).Object;
            _logger = (new Mock<ILogger<UpdateProductCommandHandler>>()).Object;


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void UpdateProduct()
        {
            setUp();

            var handler = new UpdateProductCommandHandler(_mockProductRepository.Object, _productHistoryRepository, _mapper, _logger);
            var newProduct = await handler.Handle(_command, default);
            Assert.NotNull(newProduct);
            Assert.Equal(newProduct.Id, _command.Id);
        }

        [Fact]
        public async void ThrowErrorWhenProductNotExist()
        {
            setUp();
            _mockProductRepository.Setup(x => x.GetProductById(_command.Id, _command.CompanyId)).ReturnsAsync((Product)null);

            var handler = new UpdateProductCommandHandler(_mockProductRepository.Object, _productHistoryRepository, _mapper, _logger);
            await Assert.ThrowsAsync(typeof(NotFoundException), (async () => {
                var newProduct = await handler.Handle(_command, default);
            }));
        }
    }
}
