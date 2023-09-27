using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Application.Contracts.Persistence;
using Products.Application.Exceptions;
using Products.Application.Features.Orders.Commands.CreateProduct;
using Products.Application.Features.Products.Commands.ArchiveProduct;
using Products.Application.Features.Products.Commands.CreateProduct;
using Products.Application.Features.Products.Commands.UpdateProduct;
using Products.Application.Features.Products.Queries.GetProductById;
using Products.Application.Mappings;
using Products.Domain.Entities;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Products.UnitTest.Infrastructure
{
    public class GetProductByIdTest
    {
        ProductRepository _productRepository;
        public GetProductByIdTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _productRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void GetProductById()
        {
            int productId = 1;
            int companyId = 1;
            //var _productRepository = await Repository.GetInstance();
            var product=await _productRepository.GetProductById(productId, companyId);
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
            Assert.Equal(companyId, product.CompanyId);
        }

        [Fact]
        public async void NullWhenIncorrectProductId()
        {
            int productId = 5;
            int companyId = 1;
            //var _productRepository = await Repository.GetInstance();
            var product = await _productRepository.GetProductById(productId, companyId);
            Assert.Null(product);
        }

        [Fact]
        public async void NullWhenIncorrectCompanyId()
        {
            int productId = 1;
            int companyId = 2;
            //var _productRepository = await Repository.GetInstance();
            var product = await _productRepository.GetProductById(productId, companyId);
            Assert.Null(product);
        }
    }
}
