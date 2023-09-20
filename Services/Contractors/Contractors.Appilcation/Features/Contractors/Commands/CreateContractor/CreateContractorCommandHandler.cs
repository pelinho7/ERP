using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Features.Contractors.Commands.CreateContractor;
using Contractors.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contractors.Application.Features.Contractors.Commands.CreateContractor
{
    public class CreateContractorCommandHandler : IRequestHandler<CreateContractorCommand, ContractorVm>
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IContractorHistoryRepository _contractorHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateContractorCommandHandler> _logger;

        public CreateContractorCommandHandler(IContractorRepository contractorRepository
            , IContractorHistoryRepository contractorHistoryRepository, IMapper mapper
            , ILogger<CreateContractorCommandHandler> logger)
        {
            _contractorRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
            _contractorHistoryRepository = contractorHistoryRepository ?? throw new ArgumentNullException(nameof(contractorHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ContractorVm> Handle(CreateContractorCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Contractor>(request);
            var count = await _contractorRepository.CountContractorsByCode(request.CompanyId, request.Code, null);
            if(count > 0)
            {
                throw new Exception($"Contractor with the code {request.Code} already exists.");
            }
            var newContractor = await _contractorRepository.AddAsync(productEntity);
            
            var contractorHistoryEntity = _mapper.Map<ContractorHistory>(newContractor);
            await _contractorHistoryRepository.AddAsync(contractorHistoryEntity);

            _logger.LogInformation($"Contractor {newContractor.Id} is successfully created.");
            
            return _mapper.Map<ContractorVm>(newContractor);
        }
    }
}
