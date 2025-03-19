using Companies.Domain.Entities;
using Companies.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Application.Contracts.Persistence
{
    public interface ICompanyRepository : IAsyncRepository<Company>
    {
        Task<Company> GetCompanyById(int companyId);
        //Task<Product> GetProductById(int productId,int companyId);
        //Task<Product> GetProductByCode(string productCode, int companyId);
        //Task<int> CountProducts(int companyId, string productName);
        //Task<int> CountProductsByCode(int companyId, string productCode, int? productId);
        //Task<IEnumerable<Product>> GetAllProductsByCompanyId(int companyId);
    }
}
