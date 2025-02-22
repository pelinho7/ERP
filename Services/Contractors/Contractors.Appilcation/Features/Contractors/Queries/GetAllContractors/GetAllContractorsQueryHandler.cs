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

namespace Contractors.Application.Features.Contractors.Queries.GetAllContractors
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllContractorsQuery, List<ContractorVm>>
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IContractorRepository contractorRepository, IMapper mapper)
        {
            _contractorRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ContractorVm>> Handle(GetAllContractorsQuery request, CancellationToken cancellationToken)
        {
            var products = await _contractorRepository.GetAllContractorsByCompanyId(request.CompanyId);
            return _mapper.Map<List<ContractorVm>>(products);
        }
    }
}
