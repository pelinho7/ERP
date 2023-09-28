using Contractors.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contractors.UnitTest.Infrastructure
{
    public class CountContractorsByCodeTest
    {
        ContractorRepository _ContractorRepository;
        public CountContractorsByCodeTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _ContractorRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void CountContractorsByCode()
        {
            int companyId = 1;
            string ContractorCode = "code1";
            int? ContractorId = null;
            var count = await _ContractorRepository.CountContractorsByCode(companyId
                , ContractorCode,ContractorId);

            Assert.NotNull(count);
            Assert.Equal(count, 1);
        }

        [Fact]
        public async void CountContractorsByCodeInvorrectCode()
        {
            int companyId = 1;
            string ContractorCode = "code13";
            int? ContractorId = null;
            var count = await _ContractorRepository.CountContractorsByCode(companyId
                , ContractorCode, ContractorId);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }

        [Fact]
        public async void CountContractorsByCodeIncorrectCompanyId()
        {
            int companyId = 2;
            string ContractorCode = "code11";
            int? ContractorId = null;
            var count = await _ContractorRepository.CountContractorsByCode(companyId
                , ContractorCode, ContractorId);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }

        [Fact]
        public async void CountContractorsByCodeSkipItemBaseOnContractorId()
        {
            int companyId = 1;
            string ContractorCode = "code1";
            int? ContractorId = 1;
            var count = await _ContractorRepository.CountContractorsByCode(companyId
                , ContractorCode, ContractorId);

            Assert.NotNull(count);
            Assert.Equal(count, 0);
        }


    }
}
