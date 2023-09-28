using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Features.Contractors.Commands.CreateContractor;
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
    public class CreateContractorTest
    {
        Mock<IContractorRepository> _mockContractorRepository;
        IContractorHistoryRepository _ContractorHistoryRepository;
        IMapper _mapper;
        ILogger<CreateContractorCommandHandler> _logger;
        CreateContractorCommand _command;

        private void setUp()
        {
            _mockContractorRepository = new Mock<IContractorRepository>();
            _mockContractorRepository.Setup(x => x.AddAsync(It.IsAny<Contractor>())).ReturnsAsync(new Contractor() {Id=1 });
            _mockContractorRepository.Setup(x => x.CountContractorsByCode(0,"",null)).ReturnsAsync(0);

            _ContractorHistoryRepository = (new Mock<IContractorHistoryRepository>()).Object;
            _mapper = (new Mock<IMapper>()).Object;
            _logger = (new Mock<ILogger<CreateContractorCommandHandler>>()).Object;


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _command = new CreateContractorCommand();
            _command.CompanyId = 1;
            _command.Code = "Code";
            _command.Name = "Name";
            _command.Type = 1;

            _command.CreatedBy = 1;
            _command.LastModifiedBy = 1;
            _command.CreatedDate = DateTime.Now;
        }

        [Fact]
        public async void CreateContractor()
        {
            setUp();

            var handler = new CreateContractorCommandHandler(_mockContractorRepository.Object, _ContractorHistoryRepository, _mapper, _logger);
            var newContractor = await handler.Handle(_command, default);
            Assert.NotNull(newContractor);
            Assert.NotEqual(newContractor.Id, 0);
        }

        [Fact]
        public async void ThrowErrorWhenCodeExists()
        {
            setUp();
            
            _mockContractorRepository.Setup(x => x.CountContractorsByCode(_command.CompanyId, _command.Code, null)).ReturnsAsync(1);
            var handler = new CreateContractorCommandHandler(_mockContractorRepository.Object, _ContractorHistoryRepository, _mapper, _logger);

            await Assert.ThrowsAsync(typeof(Exception), (async () => {
                var newContractor = await handler.Handle(_command, default);
            }));
        }
    }
}
