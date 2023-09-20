using Contractors.Domain.Entities;
using Contractors.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.Application.Contracts.Persistence
{
    public interface IContractorHistoryRepository : IAsyncRepository<ContractorHistory>
    {
    }
}
