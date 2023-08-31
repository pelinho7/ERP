using Products.Domain.Entities;
using Products.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Contracts.Persistence
{
    public interface IProductHistoryRepository : IAsyncRepository<ProductHistory>
    {
    }
}
