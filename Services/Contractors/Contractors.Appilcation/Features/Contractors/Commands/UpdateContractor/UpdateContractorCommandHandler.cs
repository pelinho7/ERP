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
using Contractors.Appilcation.Features.Contractors;

namespace Contractors.Application.Features.Contractors.Commands.UpdateContractor
{
    public class UpdateContractorCommandHandler : IRequestHandler<UpdateContractorCommand, ContractorVm>
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IContractorHistoryRepository _contractorHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateContractorCommandHandler> _logger;

        public UpdateContractorCommandHandler(IContractorRepository contractorRepository
            , IContractorHistoryRepository contractorHistoryRepository
            , IMapper mapper, ILogger<UpdateContractorCommandHandler> logger)
        {
            _contractorRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
            _contractorHistoryRepository = contractorHistoryRepository ?? throw new ArgumentNullException(nameof(contractorHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ContractorVm> Handle(UpdateContractorCommand request, CancellationToken cancellationToken)
        {
            var contractorToUpdate = await _contractorRepository.GetContractorById(request.Id,request.CompanyId);
            if (contractorToUpdate == null)
            {
                throw new NotFoundException(nameof(Contractor), request.Id);
            }
            
            _mapper.Map(request, contractorToUpdate, typeof(UpdateContractorCommand), typeof(Contractor));

            await _contractorRepository.UpdateAsync(contractorToUpdate);

            var contractorHistoryEntity = _mapper.Map<ContractorHistory>(contractorToUpdate);
            await _contractorHistoryRepository.AddAsync(contractorHistoryEntity);

            _logger.LogInformation($"Contractor {contractorToUpdate.Id} is successfully updated.");

            return _mapper.Map<ContractorVm>(contractorToUpdate);
        }
    }
}
