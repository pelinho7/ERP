using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Companies.Appilcation.Features.Companies;
using Companies.Application.Contracts.Persistence;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Companies.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyVm>
    {
        private readonly ICompanyRepository _companyRepository;
        //private readonly IProductHistoryRepository _productHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository
            //, IProductHistoryRepository productHistoryRepository
            , IMapper mapper
            , ILogger<CreateCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            //_productHistoryRepository = productHistoryRepository ?? throw new ArgumentNullException(nameof(productHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CompanyVm> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyEntity = _mapper.Map<Company>(request);

            var newCompany = await _companyRepository.AddAsync(companyEntity);
            
            //var productHistoryEntity = _mapper.Map<ProductHistory>(newCompany);
            //await _productHistoryRepository.AddAsync(productHistoryEntity);

            _logger.LogInformation($"Company {newCompany.Id} is successfully created.");
            
            return _mapper.Map<CompanyVm>(newCompany);
        }
    }
}
