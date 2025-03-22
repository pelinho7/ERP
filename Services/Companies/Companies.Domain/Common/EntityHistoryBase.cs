using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Domain.Common
{
    public class EntityHistoryBase
    {
        public int Id { get; set; }
        public int CreatedModifiedBy { get; set; }
        public DateTime CreatedModifiedDate { get; set; }
    }
}
