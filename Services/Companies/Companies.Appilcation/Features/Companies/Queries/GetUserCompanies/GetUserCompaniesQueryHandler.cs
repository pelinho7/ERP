using AutoMapper;
using MediatR;
using Companies.Appilcation.Features.Companies;
using Companies.Application.Contracts.Persistence;
using Companies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Companies.Application.Features.Companies.Queries.GetUserCompanies
{
    public class GetUserCompaniesQueryHandler : IRequestHandler<GetUserCompaniesQuery, List<CompanyBasicVm>>
    {
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IMapper _mapper;

        public GetUserCompaniesQueryHandler(ICompanyUserRepository companyUserRepository, IMapper mapper)
        {
            _companyUserRepository = companyUserRepository ?? throw new ArgumentNullException(nameof(companyUserRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<List<CompanyBasicVm>> Handle(GetUserCompaniesQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyUserRepository.GetUserCompanies(request.UserId);
            //check if current user is admin for company 
            return _mapper.Map<List<CompanyBasicVm>>(company);
        }
    }
}
