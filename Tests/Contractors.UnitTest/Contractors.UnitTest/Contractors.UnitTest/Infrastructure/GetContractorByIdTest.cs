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
    public class GetContractorByIdTest
    {
        ContractorRepository _ContractorRepository;
        public GetContractorByIdTest()
        {
            createRepository();
        }

        private async void createRepository()
        {
            _ContractorRepository = await Repository.CreateRepository();
        }

        [Fact]
        public async void GetContractorById()
        {
            int ContractorId = 1;
            int companyId = 1;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractor=await _ContractorRepository.GetContractorById(ContractorId, companyId);
            Assert.NotNull(Contractor);
            Assert.Equal(ContractorId, Contractor.Id);
            Assert.Equal(companyId, Contractor.CompanyId);
        }

        [Fact]
        public async void NullWhenIncorrectContractorId()
        {
            int ContractorId = 5;
            int companyId = 1;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractor = await _ContractorRepository.GetContractorById(ContractorId, companyId);
            Assert.Null(Contractor);
        }

        [Fact]
        public async void NullWhenIncorrectCompanyId()
        {
            int ContractorId = 1;
            int companyId = 2;
            //var _ContractorRepository = await Repository.GetInstance();
            var Contractor = await _ContractorRepository.GetContractorById(ContractorId, companyId);
            Assert.Null(Contractor);
        }
    }
}
