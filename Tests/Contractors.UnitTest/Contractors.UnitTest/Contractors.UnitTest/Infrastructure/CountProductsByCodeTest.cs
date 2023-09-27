using Products.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Products.UnitTest.Infrastructure
{
    public class CountProductsByCodeTest
    {
        ProductRepository _productRepository;
        public CountProductsByCodeTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _productRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void CountProductsByCode()
        {
            int companyId = 1;
            string productCode = "code1";
            int? productId = null;
            var count = await _productRepository.CountProductsByCode(companyId
                , productCode,productId);

            Assert.NotNull(count);
            Assert.Equal(count, 1);
        }

        [Fact]
        public async void CountProductsByCodeInvorrectCode()
        {
            int companyId = 1;
            string productCode = "code13";
            int? productId = null;
            var count = await _productRepository.CountProductsByCode(companyId
                , productCode, productId);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }

        [Fact]
        public async void CountProductsByCodeIncorrectCompanyId()
        {
            int companyId = 2;
            string productCode = "code11";
            int? productId = null;
            var count = await _productRepository.CountProductsByCode(companyId
                , productCode, productId);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }

        [Fact]
        public async void CountProductsByCodeSkipItemBaseOnProductId()
        {
            int companyId = 1;
            string productCode = "code1";
            int? productId = 1;
            var count = await _productRepository.CountProductsByCode(companyId
                , productCode, productId);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }


    }
}
