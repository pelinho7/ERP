using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Exceptions;
using Contractors.Application.Features.Contractors.Commands.ArchiveContractor;
using Contractors.Application.Features.Contractors.Commands.CreateContractor;
using Contractors.Application.Features.Contractors.Commands.UpdateContractor;
using Contractors.Application.Features.Contractors.Queries.GetContractorById;
using Contractors.Application.Mappings;
using Contractors.Domain.Entities;
using Contractors.Infrastructure.Persistence;
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
    public class GetContractorByCodeTest
    {
        ContractorRepository _ContractorRepository;
        public GetContractorByCodeTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _ContractorRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void GetContractorByCode()
        {
            string ContractorCode = "Code1";
            int companyId = 1;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractor=await _ContractorRepository.GetContractorByCode(ContractorCode, companyId);
            Assert.NotNull(Contractor);
            Assert.Equal(ContractorCode, Contractor.Code);
            Assert.Equal(companyId, Contractor.CompanyId);
        }

        [Fact]
        public async void NullWhenIncorrectContractorCode()
        {
            string ContractorCode = "Code5";
            int companyId = 1;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractor = await _ContractorRepository.GetContractorByCode(ContractorCode, companyId);
            Assert.Null(Contractor);
        }

        [Fact]
        public async void NullWhenIncorrectCompanyId()
        {
            string ContractorCode = "Code5";
            int companyId = 2;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractor = await _ContractorRepository.GetContractorByCode(ContractorCode, companyId);
            Assert.Null(Contractor);
        }
    }
}
