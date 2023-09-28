using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Features.Contractors.Commands.ArchiveContractor;
using Contractors.Application.Features.Contractors.Commands.UpdateContractor;
using Contractors.Application.Features.Contractors.Queries.GetContractorByCode;
using Contractors.Application.Features.Contractors.Queries.GetContractorById;
using Contractors.Application.Mappings;
using Contractors.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Contractors.Infrastructure.Repositories;

namespace Contractors.UnitTest.Application
{
    public class GetContractorByCodeTest
    {
        Mock<IContractorRepository> _mockContractorRepository;
        IMapper _mapper;
        ILogger<GetContractorByCodeQuery> _logger;
        GetContractorByCodeQuery _query;

        private void setUp()
        {
            _query = new GetContractorByCodeQuery();
            _query.ContractorCode = "Code";
            _query.CompanyId = 1;

            _mockContractorRepository = new Mock<IContractorRepository>();
            _mockContractorRepository.Setup(x => x.GetContractorByCode(_query.ContractorCode, _query.CompanyId)).ReturnsAsync(new Contractor() { Code="Code",CompanyId=1});

            _mapper = (new Mock<IMapper>()).Object;

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void GetContractorByCode()
        {
            setUp();

            var handler = new GetContractorByCodeQueryHandler(_mockContractorRepository.Object, _mapper);
            var contractor = await handler.Handle(_query, default);
            Assert.NotNull(contractor);
            Assert.Equal(_query.ContractorCode, contractor.Code);
            Assert.Equal(_query.CompanyId, contractor.CompanyId);
        }

        [Fact]
        public async void NullWhenIncorrectContractorCode()
        {
            setUp();

            var handler = new GetContractorByCodeQueryHandler(_mockContractorRepository.Object, _mapper);
            _query.ContractorCode = "123";
            var contractor = await handler.Handle(_query, default);
            Assert.Null(contractor);
        }

        [Fact]
        public async void NullWhenIncorrectCompanyId()
        {
            setUp();

            var handler = new GetContractorByCodeQueryHandler(_mockContractorRepository.Object, _mapper);
            _query.CompanyId = 2;
            var contractor = await handler.Handle(_query, default);
            Assert.Null(contractor);
        }
    }
}
