using MediatR;
using Companies.Appilcation.Features.Companies;
using System;
using System.Collections.Generic;

namespace Companies.Application.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<CompanyVm>
    {
        public GetCompanyByIdQuery() { }
        public GetCompanyByIdQuery(int companyId, int userId)
        {
            this.CompanyId = companyId;
            this.UserId = userId;
        }

        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
}
