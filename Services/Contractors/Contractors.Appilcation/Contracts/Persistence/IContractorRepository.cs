using Contractors.Domain.Entities;
using Contractors.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.Application.Contracts.Persistence
{
    public interface IContractorRepository : IAsyncRepository<Contractor>
    {
        Task<IEnumerable<Contractor>> GetContractorsByCompanyId(int companyId, string contractorName, int pageNumber, int pageSize);
        Task<Contractor> GetContractorById(int contractorId, int companyId);
        Task<Contractor> GetContractorByCode(string contractorCode, int companyId);
        Task<int> CountContractors(int companyId, string contractorName);
        Task<int> CountContractorsByCode(int companyId, string contractorCode, int? contractorId);
        Task<IEnumerable<Contractor>> GetAllContractorsByCompanyId(int companyId);
    }
}
