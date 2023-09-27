using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Application.Contracts.Persistence;
using Products.Application.Features.Orders.Commands.CreateProduct;
using Products.Application.Features.Products.Commands.CreateProduct;
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
    public class CreateProductTest
    {
        Mock<IProductRepository> _mockProductRepository;
        IProductHistoryRepository _productHistoryRepository;
        IMapper _mapper;
        ILogger<CreateProductCommandHandler> _logger;
        CreateProductCommand _command;

        private void setUp()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>())).ReturnsAsync(new Product() {Id=1 });
            _mockProductRepository.Setup(x => x.CountProductsByCode(0,"",null)).ReturnsAsync(0);

            _productHistoryRepository = (new Mock<IProductHistoryRepository>()).Object;
            _mapper = (new Mock<IMapper>()).Object;
            _logger = (new Mock<ILogger<CreateProductCommandHandler>>()).Object;


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _command = new CreateProductCommand();
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
            _command.CreatedBy = 1;
            _command.LastModifiedBy = 1;
            _command.CreatedDate = DateTime.Now;
            _command.StockLevelControl = false;
            _command.StockLevel = 0;
        }

        [Fact]
        public async void CreateProduct()
        {
            setUp();

            var handler = new CreateProductCommandHandler(_mockProductRepository.Object, _productHistoryRepository, _mapper, _logger);
            var newProduct = await handler.Handle(_command, default);
            Assert.NotNull(newProduct);
            Assert.NotEqual(newProduct.Id, 0);
        }

        [Fact]
        public async void ThrowErrorWhenCodeExists()
        {
            setUp();
            
            _mockProductRepository.Setup(x => x.CountProductsByCode(_command.CompanyId, _command.Code, null)).ReturnsAsync(1);
            var handler = new CreateProductCommandHandler(_mockProductRepository.Object, _productHistoryRepository, _mapper, _logger);
            //var newProduct = await handler.Handle(_command, new CancellationToken());
            await Assert.ThrowsAsync(typeof(Exception), (async () => {
                var newProduct = await handler.Handle(_command, default);
            }));
        }
    }
}
