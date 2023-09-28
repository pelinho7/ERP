using Contractors.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contractors.UnitTest.Infrastructure
{
    public class CountContractorsTest
    {
        ContractorRepository _ContractorRepository;
        public CountContractorsTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _ContractorRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void CountContractorsByCompany()
        {
            int companyId = 1;
            string ContractorName = null;
            //var _ContractorRepository = await Repository.GetInstance();
            var count = await _ContractorRepository.CountContractors(companyId
                , ContractorName);

            Assert.NotNull(count);
            Assert.Equal(count, 4);
        }

        [Fact]
        public async void CountContractorsByCompanyWithNameFilter()
        {
            int companyId = 1;
            string ContractorName = "me1";
            //var _ContractorRepository = await Repository.GetInstance();
            var count = await _ContractorRepository.CountContractors(companyId
                , ContractorName);

            Assert.NotNull(count);
            Assert.Equal(count, 2);
        }

        [Fact]
        public async void CountContractorsByCompanyIncorrectCompanyId()
        {
            int companyId = 3;
            string ContractorName = null;
            var count = await _ContractorRepository.CountContractors(companyId
                , ContractorName);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }

        [Fact]
        public async void CountContractorsByCompanyIncorrectContractorName()
        {
            int companyId = 1;
            string ContractorName = "dasdas";
            var count = await _ContractorRepository.CountContractors(companyId
                , ContractorName);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }

        [Fact]
        public async void CountContractorsByCompanyNameFromAnotherCompany()
        {
            int companyId = 1;
            string ContractorName = "me4";
            var count = await _ContractorRepository.CountContractors(companyId
                , ContractorName);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }
    }
}
