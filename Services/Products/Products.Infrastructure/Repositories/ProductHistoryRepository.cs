using Microsoft.EntityFrameworkCore;
using Products.Application.Contracts.Persistence;
using Products.Domain.Entities;
using Products.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Infrastructure.Repositories
{
    public class ProductHistoryRepository : RepositoryBase<ProductHistory>,IProductHistoryRepository
    {
        public ProductHistoryRepository(ProductContext dbContext) : base(dbContext)
        {
        }
    }
}
