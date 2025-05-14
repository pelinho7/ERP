using Companies.Domain.Entities;
using Companies.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Application.Contracts.Persistence
{
    public interface ICompanyUserRepository : IAsyncRepository<CompanyUser>
    {
        Task<List<Company>> GetUserCompanies(int userId);
        Task<List<CompanyUser>> GetCompanyUsers(int companyId);
        Task<CompanyUser> GetCompanyUserById(int id);
        Task<List<CompanyUser>> GetCompanyAdmins(int companyId);
        Task<CompanyUser> GetCompanyUserByUserIdCompanyId(int userId, int companyId);
    }
}
