using MediatR;
using Contractors.Appilcation.Features.Contractors;
using System;
using System.Collections.Generic;

namespace Contractors.Application.Features.Contractors.Queries.GetContractorById
{
    public class GetContractorByIdQuery : IRequest<ContractorVm>
    {
        public GetContractorByIdQuery() { }
        public GetContractorByIdQuery(int companyId, int contractorId)
        {
            this.CompanyId = companyId;
            this.ContractorId = contractorId;
        }

        public int CompanyId { get; set; }
        public int ContractorId { get; set; }
    }
}
