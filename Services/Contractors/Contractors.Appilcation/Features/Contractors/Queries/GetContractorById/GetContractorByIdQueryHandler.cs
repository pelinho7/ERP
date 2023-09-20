using AutoMapper;
using MediatR;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Application.Contracts.Persistence;
using Contractors.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Contractors.Application.Features.Contractors.Queries.GetContractorById
{
    public class GetContractorByIdQueryHandler : IRequestHandler<GetContractorByIdQuery, ContractorVm>
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IMapper _mapper;

        public GetContractorByIdQueryHandler(IContractorRepository contractorRepository, IMapper mapper)
        {
            _contractorRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ContractorVm> Handle(GetContractorByIdQuery request, CancellationToken cancellationToken)
        {
            var contractor = await _contractorRepository.GetContractorById(request.ContractorId,request.CompanyId);
            return _mapper.Map<ContractorVm>(contractor);
        }
    }
}
