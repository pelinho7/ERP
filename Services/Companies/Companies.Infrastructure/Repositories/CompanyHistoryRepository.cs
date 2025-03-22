using Microsoft.EntityFrameworkCore;
using Companies.Appilcation.Features.Companies;
using Companies.Application.Contracts.Persistence;
using Companies.Domain.Common;
using Companies.Domain.Entities;
using Companies.Application.Common;
using Companies.Infrastructure.Persistence;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Azure.Core;

namespace Companies.Infrastructure.Repositories
{
    public class CompanyHistoryRepository : RepositoryBase<CompanyHistory>, ICompanyHistoryRepository
    {
        public CompanyHistoryRepository(CompanyContext dbContext) : base(dbContext)
        {
        }
    }
}
