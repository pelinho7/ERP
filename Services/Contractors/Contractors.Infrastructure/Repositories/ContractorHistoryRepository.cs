using Microsoft.EntityFrameworkCore;
using Contractors.Application.Contracts.Persistence;
using Contractors.Domain.Entities;
using Contractors.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contractors.Infrastructure.Repositories
{
    public class ContractorHistoryRepository : RepositoryBase<ContractorHistory>,IContractorHistoryRepository
    {
        public ContractorHistoryRepository(ContractorContext dbContext) : base(dbContext)
        {
        }
    }
}
