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

namespace Contractors.Application.Features.Contractors.Queries.GetContractorsList
{
    public class GetContractorsListQueryHandler : IRequestHandler<GetContractorsListQuery, PagedList<ContractorVm>>
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IMapper _mapper;

        public GetContractorsListQueryHandler(IContractorRepository contractorRepository, IMapper mapper)
        {
            _contractorRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedList<ContractorVm>> Handle(GetContractorsListQuery request, CancellationToken cancellationToken)
        {
            var contractorList = await _contractorRepository.GetContractorsByCompanyId(request.CompanyId
                , request.ContractorFilter, request.PageNumber, request.PageSize);
            var count = await _contractorRepository.CountContractors(request.CompanyId
                , request.ContractorFilter);
            var items = _mapper.Map<List<ContractorVm>>(contractorList);

            return new PagedList<ContractorVm>(items, count, request.PageNumber, request.PageSize);
        }
    }
}
