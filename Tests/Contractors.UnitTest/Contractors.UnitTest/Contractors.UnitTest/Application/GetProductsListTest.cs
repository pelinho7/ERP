using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Appilcation.Features.Products;
using Products.Application.Contracts.Persistence;
using Products.Application.Features.Products.Commands.ArchiveProduct;
using Products.Application.Features.Products.Commands.UpdateProduct;
using Products.Application.Features.Products.Queries.GetProductByCode;
using Products.Application.Features.Products.Queries.GetProductById;
using Products.Application.Features.Products.Queries.GetProductsList;
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
    public class GetProductsListTest
    {
        Mock<IProductRepository> _mockProductRepository;
        IMapper _mapper;
        ILogger<GetProductsListQuery> _logger;
        GetProductsListQuery _query;
        List<Product> products;

        private void setUp()
        {
            _query = new GetProductsListQuery();
            _query.CompanyId = 1;
            _query.PageNumber = 1;
            _query.PageSize = 2;

            _mockProductRepository = new Mock<IProductRepository>();
            products = new List<Product>();
            products.Add(new Product() { Id=1, CompanyId=1, Name = "Product1A" });
            products.Add(new Product() { Id = 2, CompanyId = 1, Name="Product1B" });
            products.Add(new Product() { Id = 3, CompanyId = 2, Name = "Product2" });
            products.Add(new Product() { Id = 4, CompanyId = 1, Name = "Product2" });

            _mockProductRepository.Setup(x => x.GetProductsByCompanyId(_query.CompanyId, _query.ProductName,_query.PageNumber,_query.PageSize)).ReturnsAsync(products);
            _mockProductRepository.Setup(x => x.CountProducts(_query.CompanyId, _query.ProductName)).ReturnsAsync(products.Count());

            _mapper = (new Mock<IMapper>()).Object;

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void GetFirstPageProductsListWithoutFilters()
        {
            setUp();

            var handler = new GetProductsListQueryHandler(_mockProductRepository.Object, _mapper);

            var p = products.Where(x=>x.CompanyId == _query.CompanyId).ToList();
            _mockProductRepository.Setup(x => x.GetProductsByCompanyId(_query.CompanyId, _query.ProductName, _query.PageNumber, _query.PageSize)).ReturnsAsync(p);
            _mockProductRepository.Setup(x => x.CountProducts(_query.CompanyId, _query.ProductName)).ReturnsAsync(p.Count());

            var returnedProducts = await handler.Handle(_query, default);
            Assert.NotNull(returnedProducts);
            Assert.NotNull(returnedProducts.Items);
            Assert.Equal(returnedProducts.Items.Count(),p.Count);
            Assert.Equal(returnedProducts.Pagination.Page, _query.PageNumber);
        }

        [Fact]
        public async void GetSecondPageProductsListWithoutFilters()
        {
            setUp();
            _query.PageNumber = 2;

            var handler = new GetProductsListQueryHandler(_mockProductRepository.Object, _mapper);

            var p = products.Where(x => x.CompanyId == _query.CompanyId).ToList();
            _mockProductRepository.Setup(x => x.GetProductsByCompanyId(_query.CompanyId, _query.ProductName, _query.PageNumber, _query.PageSize)).ReturnsAsync(p);
            _mockProductRepository.Setup(x => x.CountProducts(_query.CompanyId, _query.ProductName)).ReturnsAsync(p.Count());

            var returnedProducts = await handler.Handle(_query, default);
          
            Assert.NotNull(returnedProducts);
            Assert.NotNull(returnedProducts.Items);
            Assert.Equal(returnedProducts.Items.Count(), p.Count);
            Assert.Equal(returnedProducts.Pagination.Page, _query.PageNumber);
        }

        [Fact]
        public async void GetProductsListWithFilters()
        {
            setUp();
            _query.ProductName = "Product1";

            var handler = new GetProductsListQueryHandler(_mockProductRepository.Object, _mapper);

            var p = products.Where(x => x.CompanyId == _query.CompanyId && x.Name.Contains(_query.ProductName)).ToList();
            _mockProductRepository.Setup(x => x.GetProductsByCompanyId(_query.CompanyId, _query.ProductName, _query.PageNumber, _query.PageSize)).ReturnsAsync(p);
            _mockProductRepository.Setup(x => x.CountProducts(_query.CompanyId, _query.ProductName)).ReturnsAsync(p.Count());

            var returnedProducts = await handler.Handle(_query, default);
            Console.WriteLine(returnedProducts.Items.Count());
            Assert.NotNull(returnedProducts);
            Assert.NotNull(returnedProducts.Items);
            Assert.Equal(returnedProducts.Items.Count(), p.Count);
            Assert.Equal(returnedProducts.Pagination.Page, _query.PageNumber);
        }
    }
}
