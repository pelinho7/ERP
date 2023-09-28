using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Contractors.Application.Contracts.Persistence;
using Contractors.Domain.Entities;
using Contractors.Infrastructure.Persistence;
using Contractors.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractors.UnitTest.Infrastructure
{
    [SetUpFixture]
    public class MySetUpClass
    {
        [OneTimeSetUp]
        public void AssemblyInitialize()
        {
            var a = 1;
            // Ta metoda zostanie wykonana przed rozpoczęciem wszystkich testów w zestawie.
            // Możesz umieścić tutaj kod inicjalizacji wspólny dla wszystkich testów.
        }
    }

    public class Repository
    {
        public static async Task<ContractorRepository> CreateRepository()
        {
            var options = new DbContextOptionsBuilder<ContractorContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new ContractorContext(options);
            var repository = new ContractorRepository(context);
     
            int ContractorNumber = 3;

            for (int companyId = 1; companyId < 3; companyId++)
            {
                for (int i = 1; i < ContractorNumber + 1; i++)
                {
                    await repository.AddAsync(new Contractor()
                    {
                        Code = "Code" + i,
                        CompanyId = companyId,
                        AdditionalInformation = "",
                        ApartmentNo = "",
                        BankAccountNumber = "",
                        City = "",
                        Country = "",
                        CountryCode = "",
                        Email = "",
                        Fax = "",
                        Mobile = "",
                        Name = "Name" + i,
                        Note = "",
                        Phone = "",
                        PostalOffice = "",
                        RepFirstName = "",
                        RepLastName = "",
                        Street = "",
                        StreetNo = "",
                        SwiftCode = "",
                        VatId = "",
                        WwwAddress = "",
                        ZipCode = ""
                    });
                }
            }

            await repository.AddAsync(new Contractor()
            {
                Code = "Code1",
                CompanyId = 1,
                Archived = true,
                AdditionalInformation = "",
                ApartmentNo = "",
                BankAccountNumber = "",
                City = "",
                Country = "",
                CountryCode = "",
                Email = "",
                Fax = "",
                Mobile = "",
                Name = "",
                Note = "",
                Phone = "",
                PostalOffice = "",
                RepFirstName = "",
                RepLastName = "",
                Street = "",
                StreetNo = "",
                SwiftCode = "",
                VatId = "",
                WwwAddress = "",
                ZipCode = ""
            });

            await repository.AddAsync(new Contractor()
            {
                Code = "Code11",
                CompanyId = 1,
                Name = "Name11",
                AdditionalInformation = "",
                ApartmentNo = "",
                BankAccountNumber = "",
                City = "",
                Country = "",
                CountryCode = "",
                Email = "",
                Fax = "",
                Mobile = "",
                Note = "",
                Phone = "",
                PostalOffice = "",
                RepFirstName = "",
                RepLastName = "",
                Street = "",
                StreetNo = "",
                SwiftCode = "",
                VatId = "",
                WwwAddress = "",
                ZipCode = ""
            });

            return repository;
        }

    }
}
