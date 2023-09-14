using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Application.Contracts.Persistence;
using Products.Application.Features.Products.Commands.ArchiveProduct;
using Products.Application.Features.Products.Commands.UpdateProduct;
using Products.Application.Features.Products.Queries.GetProductById;
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
    public class GetProductByIdTest
    {
        Mock<IProductRepository> _mockProductRepository;
        IMapper _mapper;
        ILogger<GetProductByIdQueryHandler> _logger;
        GetProductByIdQuery _query;

        private void setUp()
        {
            _query = new GetProductByIdQuery();
            _query.ProductId = 1;
            _query.CompanyId = 1;

            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(x => x.GetProductById(_query.ProductId, _query.CompanyId)).ReturnsAsync(new Product() { Id=1,CompanyId=1});

            _mapper = (new Mock<IMapper>()).Object;
            _logger = (new Mock<ILogger<GetProductByIdQueryHandler>>()).Object;


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void GetProductById()
        {
            setUp();

            var handler = new GetProductByIdQueryHandler(_mockProductRepository.Object, _mapper);
            var product = await handler.Handle(_query, default);
            Assert.NotNull(product);
            Assert.Equal(_query.ProductId, product.Id);
            Assert.Equal(_query.CompanyId, product.CompanyId);
        }

        [Fact]
        public async void NullWhenIncorrectProductId()
        {
            setUp();

            var handler = new GetProductByIdQueryHandler(_mockProductRepository.Object, _mapper);
            _query.ProductId = 2;
            var product = await handler.Handle(_query, default);
            Assert.Null(product);
        }

        [Fact]
        public async void NullWhenIncorrectCompanyId()
        {
            setUp();

            var handler = new GetProductByIdQueryHandler(_mockProductRepository.Object, _mapper);
            _query.CompanyId = 2;
            var product = await handler.Handle(_query, default);
            Assert.Null(product);
        }
    }
}
