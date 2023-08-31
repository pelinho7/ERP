using System;

namespace Products.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public int CompanyId { get; set; }
    }
}
