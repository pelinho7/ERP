using Products.Domain.Entities;
using Products.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Contracts.Persistence
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCompanyId(int companyId, string productName, int pageNumber, int pageSize);
        Task<Product> GetProductById(int productId,int companyId);
        Task<Product> GetProductByCode(string productCode, int companyId);
        Task<int> CountProducts(int companyId, string productName);
    }
}
