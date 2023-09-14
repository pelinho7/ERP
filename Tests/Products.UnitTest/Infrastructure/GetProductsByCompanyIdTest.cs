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
    public class GetProductsByCompanyIdTest
    {
        ProductRepository _productRepository;
        public GetProductsByCompanyIdTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _productRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void GetProductsByCompanyIdForPageOne()
        {
            int companyId = 1;
            string productName = null;
            int pageNumber = 1;
            int pageSize = 2;
            //var _productRepository = await Repository.GetInstance();
            var products = await _productRepository.GetProductsByCompanyId(companyId
                , productName, pageNumber, pageSize);

            Assert.NotNull(products);
            Assert.Equal(products.Count(), 2);
            Assert.Equal(products.Count(x=>x.CompanyId == companyId), 2);
            Assert.Equal(products.Count(x => x.Archived), 0);
        }

        [Fact]
        public async void GetProductsByCompanyIdForPageTwo()
        {
            int companyId = 1;
            string productName = null;
            int pageNumber = 2;
            int pageSize = 2;
            //var _productRepository = await Repository.GetInstance();
            var products = await _productRepository.GetProductsByCompanyId(companyId
                , productName, pageNumber, pageSize);

            Assert.NotNull(products);
            Assert.Equal(products.Count(), 2);
            Assert.Equal(products.Count(x => x.CompanyId == companyId), 2);
            Assert.Equal(products.Count(x => x.Archived), 0);
        }

        [Fact]
        public async void GetProductsByCompanyIdWithNameFilterForPageOne()
        {
            int companyId = 1;
            string productName = "me1";
            int pageNumber = 1;
            int pageSize = 2;
            //var _productRepository = await Repository.GetInstance();
            var products = await _productRepository.GetProductsByCompanyId(companyId
                , productName, pageNumber, pageSize);

            Assert.NotNull(products);
            Assert.Equal(products.Count(), 2);
            Assert.Equal(products.Count(x => x.CompanyId != companyId), 0);
        }

        [Fact]
        public async void NonExistentPage()
        {
            int companyId = 1;
            string productName = "me1";
            int pageNumber = 2;
            int pageSize = 2;
            //var _productRepository = await Repository.GetInstance();
            var products = await _productRepository.GetProductsByCompanyId(companyId
                , productName, pageNumber, pageSize);

            Assert.NotNull(products);
            Assert.Equal(products.Count(), 0);
        }
    }
}
