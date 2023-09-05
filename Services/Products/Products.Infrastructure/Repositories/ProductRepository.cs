using Microsoft.EntityFrameworkCore;
using Products.Appilcation.Features.Products;
using Products.Application.Contracts.Persistence;
using Products.Application.Features.Products.Queries.GetProductsList;
using Products.Domain.Common;
using Products.Domain.Entities;
using Products.Application.Common;
using Products.Infrastructure.Persistence;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Products.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ProductContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product> GetProductById(int productId, int companyId)
        {
            return  await _dbContext.Products
                                .Where(x => x.CompanyId == companyId && x.Id == productId)
                                .FirstOrDefaultAsync();
        }

        private System.Linq.Expressions.Expression<Func<Product, bool>> getProductsExpression(int companyId
            , string productName)
        {
            var expression = PredicateBuilder.True<Product>();
            expression = expression.And(x => x.CompanyId == companyId);
            expression = expression.And(x => x.Archived == false);
            if (!string.IsNullOrEmpty(productName))
                expression = expression.And(x => x.Name.Contains(productName));
            return expression;
        }

        public async Task<IEnumerable<Product>> GetProductsByCompanyId(int companyId
            , string productName, int pageNumber, int pageSize)
        {
            var expression = getProductsExpression(companyId, productName);
            var productList = await GetAsync(expression, pageNumber, pageSize);

            return productList;
        }

        public async Task<int> CountProducts(int companyId
            , string productName)
        {
            var expression = getProductsExpression(companyId, productName);
            var count = await CountAsync(expression);

            return count;
        }

        public async Task<Product> GetProductByCode(string productCode, int companyId)
        {
            return await _dbContext.Products
                                .Where(x => x.CompanyId == companyId && x.Code == productCode)
                                .FirstOrDefaultAsync();
        }
    }
}
