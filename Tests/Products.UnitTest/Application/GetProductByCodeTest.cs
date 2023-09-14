using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Application.Contracts.Persistence;
using Products.Application.Features.Products.Commands.ArchiveProduct;
using Products.Application.Features.Products.Commands.UpdateProduct;
using Products.Application.Features.Products.Queries.GetProductByCode;
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
    public class GetProductByCodeTest
    {
        Mock<IProductRepository> _mockProductRepository;
        IMapper _mapper;
        ILogger<GetProductByCodeQuery> _logger;
        GetProductByCodeQuery _query;

        private void setUp()
        {
            _query = new GetProductByCodeQuery();
            _query.ProductCode = "Code";
            _query.CompanyId = 1;

            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(x => x.GetProductByCode(_query.ProductCode, _query.CompanyId)).ReturnsAsync(new Product() { Code="Code",CompanyId=1});

            _mapper = (new Mock<IMapper>()).Object;

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void GetProductByCode()
        {
            setUp();

            var handler = new GetProductByCodeQueryHandler(_mockProductRepository.Object, _mapper);
            var product = await handler.Handle(_query, default);
            Assert.NotNull(product);
            Assert.Equal(_query.ProductCode, product.Code);
            Assert.Equal(_query.CompanyId, product.CompanyId);
        }

        [Fact]
        public async void NullWhenIncorrectProductCode()
        {
            setUp();

            var handler = new GetProductByCodeQueryHandler(_mockProductRepository.Object, _mapper);
            _query.ProductCode = "123";
            var product = await handler.Handle(_query, default);
            Assert.Null(product);
        }

        [Fact]
        public async void NullWhenIncorrectCompanyId()
        {
            setUp();

            var handler = new GetProductByCodeQueryHandler(_mockProductRepository.Object, _mapper);
            _query.CompanyId = 2;
            var product = await handler.Handle(_query, default);
            Assert.Null(product);
        }
    }
}
