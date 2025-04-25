using MediatR;
using Companies.Appilcation.Features.Companies;
using System;
using System.Collections.Generic;

namespace Companies.Application.Features.Companies.Queries.GetUserCompanies
{
    public class GetUserCompaniesQuery : IRequest<List<CompanyBasicVm>>
    {
        public GetUserCompaniesQuery() { }
        public GetUserCompaniesQuery(int userId)
        {
            this.UserId = userId;
        }

        public int UserId { get; set; }
    }
}
