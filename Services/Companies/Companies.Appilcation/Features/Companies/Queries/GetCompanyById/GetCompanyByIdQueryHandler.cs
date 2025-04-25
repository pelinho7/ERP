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

namespace Companies.Application.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQueryHandler :CompanyCommandHandlerQueryBase, IRequestHandler<GetCompanyByIdQuery, CompanyVm>
    {
        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
            :base(companyRepository, mapper) { }

        public async Task<CompanyVm> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyById(request.CompanyId);
            //check if current user is admin for company 
            return _mapper.Map<CompanyVm>(company);
        }
    }
}
