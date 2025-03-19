using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.Companies.Commands
{
    public abstract class CompanyCommandBase
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string VatId { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string ApartmentNo { get; set; }
        public string ZipCode { get; set; }
        public string PostalOffice { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string WwwAddress { get; set; }
        public string BankAccountNumber { get; set; }
        public string SwiftCode { get; set; }
    }
}
