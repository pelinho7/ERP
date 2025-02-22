using Contractors.Appilcation.Features.Contractors;
using MediatR;
using Contractors.Appilcation.Features.Contractors;
using System;
using System.Collections.Generic;

namespace Contractors.Application.Features.Contractors.Queries.GetAllContractors
{
    public class GetAllContractorsQuery : IRequest<List<ContractorVm>>
    {
        public GetAllContractorsQuery() { }
        public GetAllContractorsQuery(int companyId)
        {
            CompanyId = companyId;
        }

        public int CompanyId { get; set; }        
    }
}
