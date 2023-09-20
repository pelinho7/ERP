using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Application.Contracts.Persistence;
using Contractors.Application.Exceptions;
using Contractors.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contractors.Application.Features.Contractors.Commands.ArchiveContractor
{
    public class ArchiveContractorCommandHandler : IRequestHandler<ArchiveContractorCommand, int>
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IContractorHistoryRepository _contractorHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ArchiveContractorCommandHandler> _logger;

        public ArchiveContractorCommandHandler(IContractorRepository contractorRepository
            , IContractorHistoryRepository contractorHistoryRepository
            , IMapper mapper, ILogger<ArchiveContractorCommandHandler> logger)
        {
            _contractorRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
            _contractorHistoryRepository = contractorHistoryRepository ?? throw new ArgumentNullException(nameof(contractorHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(ArchiveContractorCommand request, CancellationToken cancellationToken)
        {
            var contractorToArchive = await _contractorRepository.GetContractorById(request.Id, request.CompanyId);
            if (contractorToArchive == null)
            {
                throw new NotFoundException(nameof(Contractor), request.Id);
            }
            
            _mapper.Map(request, contractorToArchive, typeof(ArchiveContractorCommand), typeof(Contractor));

            await _contractorRepository.UpdateAsync(contractorToArchive);

            var contractorHistoryEntity = _mapper.Map<ContractorHistory>(contractorToArchive);
            await _contractorHistoryRepository.AddAsync(contractorHistoryEntity);

            _logger.LogInformation($"Contractor {contractorToArchive.Id} is successfully archived.");

            return contractorToArchive.Id;
        }
    }
}
