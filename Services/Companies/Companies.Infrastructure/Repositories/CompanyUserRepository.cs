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
    public class CompanyUserRepository : RepositoryBase<CompanyUser>, ICompanyUserRepository
    {
        public CompanyUserRepository(CompanyContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Company>> GetUserCompanies(int userId)
        {
            return await _dbContext.CompanyUsers
            .Include(x => x.Company)
            .Where(x => x.UserId == userId && x.Archived == false && x.Company.Archived==false)
            .Select(x => x.Company)
            .ToListAsync();
        }

        public async Task<List<CompanyUser>> GetCompanyUsers(int companyId)
        {
            return await _dbContext.CompanyUsers.AsNoTracking()
                .Where(x => x.CompanyId == companyId && x.Archived == false )
                .ToListAsync();
        }

        public async Task<CompanyUser> GetCompanyUserById(int id)
        {
            return await _dbContext.CompanyUsers.AsNoTracking()
                .Where(x => x.Id == id && x.Archived == false)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CompanyUser>> GetCompanyAdmins(int companyId)
        {
            return await _dbContext.CompanyUsers.AsNoTracking()
                .Where(x => x.CompanyId == companyId && x.Archived == false && x.Administrator == true)
                .ToListAsync();
        }
    }
}
