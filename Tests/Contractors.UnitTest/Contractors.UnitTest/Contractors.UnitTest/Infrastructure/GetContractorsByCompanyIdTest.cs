using Contractors.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Contractors.UnitTest.Infrastructure
{
    public class GetContractorsByCompanyIdTest
    {
        ContractorRepository _ContractorRepository;
        public GetContractorsByCompanyIdTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _ContractorRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void GetContractorsByCompanyIdForPageOne()
        {
            int companyId = 1;
            string ContractorName = null;
            int pageNumber = 1;
            int pageSize = 2;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractors = await _ContractorRepository.GetContractorsByCompanyId(companyId
                , ContractorName, pageNumber, pageSize);

            Assert.NotNull(Contractors);
            Assert.Equal(Contractors.Count(), 2);
            Assert.Equal(Contractors.Count(x=>x.CompanyId == companyId), 2);
            Assert.Equal(Contractors.Count(x => x.Archived), 0);
        }

        [Fact]
        public async void GetContractorsByCompanyIdForPageTwo()
        {
            int companyId = 1;
            string ContractorName = null;
            int pageNumber = 2;
            int pageSize = 2;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractors = await _ContractorRepository.GetContractorsByCompanyId(companyId
                , ContractorName, pageNumber, pageSize);

            Assert.NotNull(Contractors);
            Assert.Equal(Contractors.Count(), 2);
            Assert.Equal(Contractors.Count(x => x.CompanyId == companyId), 2);
            Assert.Equal(Contractors.Count(x => x.Archived), 0);
        }

        [Fact]
        public async void GetContractorsByCompanyIdWithNameFilterForPageOne()
        {
            int companyId = 1;
            string ContractorName = "me1";
            int pageNumber = 1;
            int pageSize = 2;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractors = await _ContractorRepository.GetContractorsByCompanyId(companyId
                , ContractorName, pageNumber, pageSize);

            Assert.NotNull(Contractors);
            Assert.Equal(Contractors.Count(), 2);
            Assert.Equal(Contractors.Count(x => x.CompanyId != companyId), 0);
        }

        [Fact]
        public async void NonExistentPage()
        {
            int companyId = 1;
            string ContractorName = "me1";
            int pageNumber = 2;
            int pageSize = 2;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractors = await _ContractorRepository.GetContractorsByCompanyId(companyId
                , ContractorName, pageNumber, pageSize);

            Assert.NotNull(Contractors);
            Assert.Equal(Contractors.Count(), 0);
        }
    }
}
