using MediatR;
using Companies.Appilcation.Features.Companies;
using System;
using System.Collections.Generic;
using Companies.Appilcation.Features.CompanyUsers;

namespace Companies.Application.Features.CompanyUsers.Queries.GetCompanyUsers
{
    public class GetCompanyUsersQuery : IRequest<List<CompanyUserVm>>
    {
        public GetCompanyUsersQuery() { }
        public GetCompanyUsersQuery(int companyId, int userId)
        {
            this.CompanyId = companyId;
            this.UserId = userId;
        }

        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
}
