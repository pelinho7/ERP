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
    }
}
