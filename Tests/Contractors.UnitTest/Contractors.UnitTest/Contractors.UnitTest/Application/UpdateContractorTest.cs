using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Exceptions;
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
    public class UpdateContractorTest
    {
        Mock<IContractorRepository> _mockContractorRepository;
        IContractorHistoryRepository _ContractorHistoryRepository;
        IMapper _mapper;
        ILogger<UpdateContractorCommandHandler> _logger;
        UpdateContractorCommand _command;

        private void setUp()
        {
            _command = new UpdateContractorCommand();
            _command.Id = 1;
            _command.CompanyId = 1;
            _command.Code = "Code";
            _command.Name = "Name";
            _command.Type = 1;

            _mockContractorRepository = new Mock<IContractorRepository>();
            _mockContractorRepository.Setup(x => x.UpdateAsync(It.IsAny<Contractor>()));
            _mockContractorRepository.Setup(x => x.GetContractorById(_command.Id, _command.CompanyId)).ReturnsAsync(new Contractor());

            _ContractorHistoryRepository = (new Mock<IContractorHistoryRepository>()).Object;
            _mapper = (new Mock<IMapper>()).Object;
            _logger = (new Mock<ILogger<UpdateContractorCommandHandler>>()).Object;


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void UpdateContractor()
        {
            setUp();

            var handler = new UpdateContractorCommandHandler(_mockContractorRepository.Object, _ContractorHistoryRepository, _mapper, _logger);
            var newContractor = await handler.Handle(_command, default);
            Assert.NotNull(newContractor);
            Assert.Equal(newContractor.Id, _command.Id);
        }

        [Fact]
        public async void ThrowErrorWhenContractorNotExist()
        {
            setUp();
            _mockContractorRepository.Setup(x => x.GetContractorById(_command.Id, _command.CompanyId)).ReturnsAsync((Contractor)null);

            var handler = new UpdateContractorCommandHandler(_mockContractorRepository.Object, _ContractorHistoryRepository, _mapper, _logger);
            await Assert.ThrowsAsync(typeof(NotFoundException), (async () => {
                var newContractor = await handler.Handle(_command, default);
            }));
        }
    }
}
