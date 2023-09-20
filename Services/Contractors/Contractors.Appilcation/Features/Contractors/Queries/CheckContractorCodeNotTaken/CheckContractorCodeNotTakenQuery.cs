using MediatR;
using Contractors.Appilcation.Features.Contractors;
using System;
using System.Collections.Generic;

namespace Contractors.Application.Features.Contractors.Queries.CheckContractorCodeNotTaken
{
    public class CheckContractorCodeNotTakenQuery : IRequest<bool>
    {
        public CheckContractorCodeNotTakenQuery(int companyId, string contractorCode, int? contractorId)
        {
            CompanyId = companyId;
            ContractorCode = contractorCode;
            ContractorId = contractorId;
        }

        public int CompanyId { get; set; }
        public string ContractorCode { get; set; }
        public int? ContractorId { get; set; }
    }
}
