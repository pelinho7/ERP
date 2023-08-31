using Products.Appilcation.Features.Products.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Appilcation.Features.Products.Commands
{
    public interface IUpdateProductCommand:IProductCommand
    {
        int? LastModifiedBy { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
