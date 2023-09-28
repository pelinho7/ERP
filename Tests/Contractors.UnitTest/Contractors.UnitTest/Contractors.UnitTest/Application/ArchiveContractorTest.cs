using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Exceptions;
using Contractors.Application.Features.Contractors.Commands.ArchiveContractor;
using Contractors.Application.Features.Contractors.Commands.CreateContractor;
using Contractors.Application.Features.Contractors.Commands.UpdateContractor;
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
    public class ArchiveContractorTest
    {
        Mock<IContractorRepository> _mockContractorRepository;
        IContractorHistoryRepository _ContractorHistoryRepository;
        IMapper _mapper;
        ILogger<ArchiveContractorCommandHandler> _logger;
        ArchiveContractorCommand _command;

        private void setUp()
        {
            _command = new ArchiveContractorCommand();
            _command.Id = 1;
            _command.CompanyId = 1;
            _command.Archived = true;

            _mockContractorRepository = new Mock<IContractorRepository>();
            _mockContractorRepository.Setup(x => x.UpdateAsync(It.IsAny<Contractor>()));
            _mockContractorRepository.Setup(x => x.GetContractorById(_command.Id, _command.CompanyId)).ReturnsAsync(new Contractor());

            _ContractorHistoryRepository = (new Mock<IContractorHistoryRepository>()).Object;
            _mapper = (new Mock<IMapper>()).Object;
            _logger = (new Mock<ILogger<ArchiveContractorCommandHandler>>()).Object;


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void ArchiveContractor()
        {
            setUp();

            var handler = new ArchiveContractorCommandHandler(_mockContractorRepository.Object, _ContractorHistoryRepository, _mapper, _logger);
            var ContractorId = await handler.Handle(_command, default);
            Assert.Equal(ContractorId, _command.Id);
        }

        [Fact]
        public async void ThrowErrorWhenContractorNotExist()
        {
            setUp();
            _mockContractorRepository.Setup(x => x.GetContractorById(_command.Id, _command.CompanyId)).ReturnsAsync((Contractor)null);

            var handler = new ArchiveContractorCommandHandler(_mockContractorRepository.Object, _ContractorHistoryRepository, _mapper, _logger);
            await Assert.ThrowsAsync(typeof(NotFoundException), (async () =>
            {
                var newContractor = await handler.Handle(_command, default);
            }));
        }
    }
}
