using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Features.Contractors.Commands.ArchiveContractor;
using Contractors.Application.Features.Contractors.Commands.UpdateContractor;
using Contractors.Application.Features.Contractors.Queries.GetContractorByCode;
using Contractors.Application.Features.Contractors.Queries.GetContractorById;
using Contractors.Application.Features.Contractors.Queries.GetContractorsList;
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
    public class GetContractorsListTest
    {
        Mock<IContractorRepository> _mockContractorRepository;
        IMapper _mapper;
        ILogger<GetContractorsListQuery> _logger;
        GetContractorsListQuery _query;
        List<Contractor> contractors;

        private void setUp()
        {
            _query = new GetContractorsListQuery();
            _query.CompanyId = 1;
            _query.PageNumber = 1;
            _query.PageSize = 2;

            _mockContractorRepository = new Mock<IContractorRepository>();
            contractors = new List<Contractor>();
            contractors.Add(new Contractor() { Id=1, CompanyId=1, Name = "Contractor1A" });
            contractors.Add(new Contractor() { Id = 2, CompanyId = 1, Name="Contractor1B" });
            contractors.Add(new Contractor() { Id = 3, CompanyId = 2, Name = "Contractor2" });
            contractors.Add(new Contractor() { Id = 4, CompanyId = 1, Name = "Contractor2" });

            _mockContractorRepository.Setup(x => x.GetContractorsByCompanyId(_query.CompanyId, _query.ContractorFilter, _query.PageNumber,_query.PageSize)).ReturnsAsync(contractors);
            _mockContractorRepository.Setup(x => x.CountContractors(_query.CompanyId, _query.ContractorFilter)).ReturnsAsync(contractors.Count());

            _mapper = (new Mock<IMapper>()).Object;

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async void GetFirstPageContractorsListWithoutFilters()
        {
            setUp();

            var handler = new GetContractorsListQueryHandler(_mockContractorRepository.Object, _mapper);

            var p = contractors.Where(x=>x.CompanyId == _query.CompanyId).ToList();
            _mockContractorRepository.Setup(x => x.GetContractorsByCompanyId(_query.CompanyId, _query.ContractorFilter, _query.PageNumber, _query.PageSize)).ReturnsAsync(p);
            _mockContractorRepository.Setup(x => x.CountContractors(_query.CompanyId, _query.ContractorFilter)).ReturnsAsync(p.Count());

            var returnedContractors = await handler.Handle(_query, default);
            Assert.NotNull(returnedContractors);
            Assert.NotNull(returnedContractors.Items);
            Assert.Equal(returnedContractors.Items.Count(),p.Count);
            Assert.Equal(returnedContractors.Pagination.Page, _query.PageNumber);
        }

        [Fact]
        public async void GetSecondPageContractorsListWithoutFilters()
        {
            setUp();
            _query.PageNumber = 2;

            var handler = new GetContractorsListQueryHandler(_mockContractorRepository.Object, _mapper);

            var p = contractors.Where(x => x.CompanyId == _query.CompanyId).ToList();
            _mockContractorRepository.Setup(x => x.GetContractorsByCompanyId(_query.CompanyId, _query.ContractorFilter, _query.PageNumber, _query.PageSize)).ReturnsAsync(p);
            _mockContractorRepository.Setup(x => x.CountContractors(_query.CompanyId, _query.ContractorFilter)).ReturnsAsync(p.Count());

            var returnedContractors = await handler.Handle(_query, default);
          
            Assert.NotNull(returnedContractors);
            Assert.NotNull(returnedContractors.Items);
            Assert.Equal(returnedContractors.Items.Count(), p.Count);
            Assert.Equal(returnedContractors.Pagination.Page, _query.PageNumber);
        }

        [Fact]
        public async void GetContractorsListWithFilters()
        {
            setUp();
            _query.ContractorFilter = "Contractor1";

            var handler = new GetContractorsListQueryHandler(_mockContractorRepository.Object, _mapper);

            var p = contractors.Where(x => x.CompanyId == _query.CompanyId && x.Name.Contains(_query.ContractorFilter)).ToList();
            _mockContractorRepository.Setup(x => x.GetContractorsByCompanyId(_query.CompanyId, _query.ContractorFilter, _query.PageNumber, _query.PageSize)).ReturnsAsync(p);
            _mockContractorRepository.Setup(x => x.CountContractors(_query.CompanyId, _query.ContractorFilter)).ReturnsAsync(p.Count());

            var returnedContractors = await handler.Handle(_query, default);
            Console.WriteLine(returnedContractors.Items.Count());
            Assert.NotNull(returnedContractors);
            Assert.NotNull(returnedContractors.Items);
            Assert.Equal(returnedContractors.Items.Count(), p.Count);
            Assert.Equal(returnedContractors.Pagination.Page, _query.PageNumber);
        }
    }
}
