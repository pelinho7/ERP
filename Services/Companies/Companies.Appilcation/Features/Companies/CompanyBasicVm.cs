using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.Companies
{
    public class CompanyBasicVm
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(2)")]
        public string CountryCode { get; set; }
        [Column(TypeName = "varchar(13)")]
        public string VatId { get; set; }
    }
}
