using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Features.Contractors.Commands.ArchiveContractor;
using Contractors.Application.Features.Contractors.Commands.UpdateContractor;
using Contractors.Application.Features.Contractors.Queries.GetContractorById;
using Contractors.Application.Mappings;
using Contractors.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contractors.UnitTest.Application
{
    public class GetContractorByIdTest
    {
        Mock<IContractorRepository> _mockContractorRepository;
        IMapper _mapper;
        ILogger<GetContractorByIdQueryHandler> _logger;
        GetContractorByIdQuery _query;

        private void setUp()
        {
            _query = new GetContractorByIdQuery();
            _query.ContractorId = 1;
            _query.CompanyId = 1;

            _mockContractorRepository = new Mock<IContractorRepository>();
            _mockContractorRepository.Setup(x => x.GetContractorById(_query.ContractorId, _query.CompanyId)).ReturnsAsync(new Contractor() { Id=1,CompanyId=1});

            _mapper = (new Mock<IMapper>()).Object;
            _logger = (new Mock<ILogger<GetContractorByIdQueryHandler>>()).Object;


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void GetContractorById()
        {
            setUp();

            var handler = new GetContractorByIdQueryHandler(_mockContractorRepository.Object, _mapper);
            var Contractor = await handler.Handle(_query, default);
            Assert.NotNull(Contractor);
            Assert.Equal(_query.ContractorId, Contractor.Id);
            Assert.Equal(_query.CompanyId, Contractor.CompanyId);
        }

        [Fact]
        public async void NullWhenIncorrectContractorId()
        {
            setUp();

            var handler = new GetContractorByIdQueryHandler(_mockContractorRepository.Object, _mapper);
            _query.ContractorId = 2;
            var contractor = await handler.Handle(_query, default);
            Assert.Null(contractor);
        }

        [Fact]
        public async void NullWhenIncorrectCompanyId()
        {
            setUp();

            var handler = new GetContractorByIdQueryHandler(_mockContractorRepository.Object, _mapper);
            _query.CompanyId = 2;
            var contractor = await handler.Handle(_query, default);
            Assert.Null(contractor);
        }
    }
}
