using MediatR;
using Contractors.Appilcation.Features.Contractors;
using System;
using System.Collections.Generic;

namespace Contractors.Application.Features.Contractors.Queries.GetContractorsList
{
    public class GetContractorsListQuery : IRequest<PagedList<ContractorVm>>
    {
        public GetContractorsListQuery() { }
        public GetContractorsListQuery(int companyId, string filter, int pageNumber, int pageSize)
        {
            CompanyId = companyId;
            ContractorFilter = filter;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int CompanyId { get; set; }
        public string ContractorFilter { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        
    }
}
