using System;

namespace Products.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
    }
}
