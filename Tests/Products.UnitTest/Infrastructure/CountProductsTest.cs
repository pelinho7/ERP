using Products.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Products.UnitTest.Infrastructure
{
    public class CountProductsTest
    {
        ProductRepository _productRepository;
        public CountProductsTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _productRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void CountProductsByCompany()
        {
            int companyId = 1;
            string productName = null;
            //var _productRepository = await Repository.GetInstance();
            var count = await _productRepository.CountProducts(companyId
                , productName);

            Assert.NotNull(count);
            Assert.Equal(count, 4);
        }

        [Fact]
        public async void CountProductsByCompanyWithNameFilter()
        {
            int companyId = 1;
            string productName = "me1";
            //var _productRepository = await Repository.GetInstance();
            var count = await _productRepository.CountProducts(companyId
                , productName);

            Assert.NotNull(count);
            Assert.Equal(count, 2);
        }

        [Fact]
        public async void CountProductsByCompanyIncorrectCompanyId()
        {
            int companyId = 3;
            string productName = null;
            var count = await _productRepository.CountProducts(companyId
                , productName);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }

        [Fact]
        public async void CountProductsByCompanyIncorrectProductName()
        {
            int companyId = 1;
            string productName = "dasdas";
            var count = await _productRepository.CountProducts(companyId
                , productName);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }

        [Fact]
        public async void CountProductsByCompanyNameFromAnotherCompany()
        {
            int companyId = 1;
            string productName = "me4";
            var count = await _productRepository.CountProducts(companyId
                , productName);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }
    }
}
