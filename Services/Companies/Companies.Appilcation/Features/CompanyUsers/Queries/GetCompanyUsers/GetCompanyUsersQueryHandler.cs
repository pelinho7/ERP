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
using Companies.Appilcation.Features.CompanyUsers;

namespace Companies.Application.Features.CompanyUsers.Queries.GetCompanyUsers
{
    public class GetCompanyUsersQueryHandler :CompanyUserCommandHandlerQueryBase, IRequestHandler<GetCompanyUsersQuery, List<CompanyUserVm>>
    {
        public GetCompanyUsersQueryHandler(ICompanyUserRepository companyUserRepository
            , IMapper mapper)
            :base(companyUserRepository, mapper) { }

        public async Task<List<CompanyUserVm>> Handle(GetCompanyUsersQuery request, CancellationToken cancellationToken)
        {
            var companyUsers = await _companyUserRepository.GetCompanyUsers(request.CompanyId);
            //check if current user is admin for company 
            var userPermissions = companyUsers
                .Where(x => x.UserId == request.UserId && x.Archived == false)
                .FirstOrDefault();
            if (userPermissions == null || !userPermissions.Administrator)
            {
                throw new System.Security.Authentication.AuthenticationException("No administrator rights.");
            }

            return _mapper.Map<List<CompanyUserVm>>(companyUsers);
        }
    }
}
