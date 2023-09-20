using System;

namespace Contractors.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
    }
}
