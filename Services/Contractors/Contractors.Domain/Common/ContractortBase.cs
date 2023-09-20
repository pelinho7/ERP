using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.Domain.Common
{
    public abstract class ContractorBase : EntityBase
    {
        [Column(TypeName = "varchar(30)")]
        public string Code { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string RepFirstName { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string RepLastName { get; set; }
        [Column(TypeName = "varchar(2)")]
        public string CountryCode { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public bool ActiveTaxpayer { get; set; }
        [Column(TypeName = "varchar(13)")]
        public string VatId { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Street { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string StreetNo { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string ApartmentNo { get; set; }
        [Column(TypeName = "varchar(6)")]
        public string ZipCode { get; set; }
        [Column(TypeName = "varchar(120)")]
        public string PostalOffice { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string Country { get; set; }
        [Column(TypeName = "varchar(120)")]
        public string City { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Phone { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string email { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Mobile { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Fax { get; set; }
        public string WwwAddress { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }
        public int PaymentForm { get; set; }
        [Column(TypeName = "varchar(34)")]
        public string BankAccountNumber { get; set; }
        public string SwiftCode { get; set; }
        public bool SplitPayment { get; set; }
        public string AdditionalInformation { get; set; }
        public string Note { get; set; }
        public bool Archived { get; set; }
    }
}
