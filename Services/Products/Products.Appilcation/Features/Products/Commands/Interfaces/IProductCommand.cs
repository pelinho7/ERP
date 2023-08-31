using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Appilcation.Features.Products.Commands.Interfaces
{
    public interface IProductCommand
    {
        int CompanyId { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
