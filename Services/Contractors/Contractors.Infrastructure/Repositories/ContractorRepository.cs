using Microsoft.EntityFrameworkCore;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Application.Contracts.Persistence;
using Contractors.Domain.Common;
using Contractors.Domain.Entities;
using Contractors.Application.Common;
using Contractors.Infrastructure.Persistence;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Azure.Core;

namespace Contractors.Infrastructure.Repositories
{
    public class ContractorRepository : RepositoryBase<Contractor>, IContractorRepository
    {
        public ContractorRepository(ContractorContext dbContext) : base(dbContext)
        {
        }

        public async Task<Contractor> GetContractorById(int productId, int companyId)
        {
            return await _dbContext.Contractors
                                .Where(x => x.CompanyId == companyId && x.Id == productId)
                                .FirstOrDefaultAsync();
        }

        private System.Linq.Expressions.Expression<Func<Contractor, bool>> getContractorsExpression(int companyId
            , string contractorFilter)
        {
            var expression = PredicateBuilder.True<Contractor>();
            expression = expression.And(x => x.CompanyId == companyId);
            expression = expression.And(x => x.Archived == false);
            if (!string.IsNullOrEmpty(contractorFilter))
            {
                expression = expression.And(x => x.Name.Contains(contractorFilter)
                    || x.Code.Contains(contractorFilter)
                    || x.VatId.Contains(contractorFilter));
            }
            return expression;
        }

        public async Task<IEnumerable<Contractor>> GetContractorsByCompanyId(int companyId, string contractorFilter, int pageNumber, int pageSize)
        {
            var expression = getContractorsExpression(companyId, contractorFilter);
            var contractorList = await GetAsync(expression, pageNumber, pageSize);

            return contractorList;
        }

        public async Task<Contractor> GetContractorByCode(string contractorCode, int companyId)
        {
            return await _dbContext.Contractors
                    .Where(x => x.CompanyId == companyId && x.Code == contractorCode)
                    .FirstOrDefaultAsync();
        }

        public async Task<int> CountContractors(int companyId, string contractorName)
        {
            var expression = getContractorsExpression(companyId, contractorName);
            var count = await CountAsync(expression);

            return count;
        }

        public async Task<int> CountContractorsByCode(int companyId, string contractorCode, int? contractorId)
        {
            var expression = PredicateBuilder.True<Contractor>();
            expression = expression.And(x => x.CompanyId == companyId);
            expression = expression.And(x => x.Archived == false);
            expression = expression.And(x => x.Code.ToUpper() == contractorCode.ToUpper());
            if (contractorId.HasValue)
                expression = expression.And(x => x.Id != contractorId.Value);
            var count = await CountAsync(expression);
            return count;
        }
    }
}
