using AutoMapper;
using MediatR;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Application.Common;
using Contractors.Application.Contracts.Persistence;
using Contractors.Domain.Common;
using Contractors.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Contractors.Application.Features.Contractors.Queries.CheckContractorCodeNotTaken
{
    public class CheckContractorCodeNotTakenQueryHandler : IRequestHandler<CheckContractorCodeNotTakenQuery, bool>
    {
        private readonly IContractorRepository _contractorCodeRepository;
        private readonly IMapper _mapper;

        public CheckContractorCodeNotTakenQueryHandler(IContractorRepository contractorRepository, IMapper mapper)
        {
            _contractorCodeRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(CheckContractorCodeNotTakenQuery request, CancellationToken cancellationToken)
        {
            var count = await _contractorCodeRepository.CountContractorsByCode(request.CompanyId,request.ContractorCode,request.ContractorId);
            return count==0;
        }
    }
}
