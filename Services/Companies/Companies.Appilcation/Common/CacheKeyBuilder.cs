using Companies.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Common
{
    public class CacheKeyBuilder
    {
        public static string Build(Type type, int? id, int? userId, int? companyId)
        {
            string key = $"{typeof(CompanyUser)}";
            if (id.HasValue)
                key += $"_{id.Value}";
            if (userId.HasValue)
                key += $"_{userId.Value}";
            if (companyId.HasValue)
                key += $"_{companyId.Value}";

            return key;
        }
    }
}
