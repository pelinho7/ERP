using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Products.Application.Contracts.Persistence;
using Products.Domain.Entities;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.UnitTest.Infrastructure
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
        public static async Task<ProductRepository> CreateRepository()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new ProductContext(options);
            var repository = new ProductRepository(context);
     
            int productNumber = 3;

            for (int companyId = 1; companyId < 3; companyId++)
            {
                for (int i = 1; i < productNumber + 1; i++)
                {
                    await repository.AddAsync(new Product()
                    {
                        Code = "Code" + i,
                        CompanyId = companyId,
                        EAN = "ean",
                        PurchaseCurrency = "PLN",
                        SaleCurrency = "PLN",
                        Unit = "kg",
                        Description = "",
                        Name = "Name" + i,
                        PKWiU = ""
                    });
                }
            }

            await repository.AddAsync(new Product()
            {
                Code = "Code1",
                CompanyId = 1,
                EAN = "ean",
                PurchaseCurrency = "PLN",
                SaleCurrency = "PLN",
                Unit = "kg",
                Description = "",
                Name = "Name1",
                PKWiU = "",
                Archived = true,
            });

            await repository.AddAsync(new Product()
            {
                Code = "Code11",
                CompanyId = 1,
                EAN = "ean",
                PurchaseCurrency = "PLN",
                SaleCurrency = "PLN",
                Unit = "kg",
                Description = "",
                Name = "Name11",
                PKWiU = "",
            });

            return repository;
        }

    }
}
