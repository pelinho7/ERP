using MediatR;
using Contractors.Appilcation.Features.Contractors;
using System;
using System.Collections.Generic;

namespace Contractors.Application.Features.Contractors.Queries.GetContractorsList
{
    public class GetContractorsListQuery : IRequest<PagedList<ContractorVm>>
    {
        public GetContractorsListQuery() { }
        public GetContractorsListQuery(int companyId, string contractorName, int pageNumber, int pageSize)
        {
            CompanyId = companyId;
            ContractorName = contractorName;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int CompanyId { get; set; }
        public string ContractorName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        
    }
}
