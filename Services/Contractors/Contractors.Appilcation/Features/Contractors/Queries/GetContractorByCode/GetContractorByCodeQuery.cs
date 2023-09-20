using MediatR;
using Contractors.Appilcation.Features.Contractors;
using System;
using System.Collections.Generic;

namespace Contractors.Application.Features.Contractors.Queries.GetContractorByCode
{
    public class GetContractorByCodeQuery : IRequest<ContractorVm>
    {
        public GetContractorByCodeQuery() { }
        public GetContractorByCodeQuery(int companyId, string contractorCode)
        {
            this.CompanyId = companyId;
            this.ContractorCode = contractorCode;
        }

        public int CompanyId { get; set; }
        public string ContractorCode { get; set; }
    }
}
