using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.Appilcation.Features.Contractors.Commands
{
    public abstract class ContractorCommandBase
    {
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string RepFirstName { get; set; }
        public string RepLastName { get; set; }
        public string CountryCode { get; set; }
        public int Status { get; set; }
        public string VatId { get; set; }
        public int Type { get; set; }
        public bool SplitPayment { get; set; }
        public bool ActiveTaxpayer { get; set; }
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
        public string AdditionalInformation { get; set; }
        public string Note { get; set; }
        public decimal Discount { get; set; }
        public int PaymentForm { get; set; }
    }
}
