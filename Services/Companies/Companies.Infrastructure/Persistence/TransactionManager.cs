using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Companies.Appilcation.Contracts.Persistence;

namespace Companies.Infrastructure.Persistence
{
    public class TransactionManager : ITransactionManager
    {
        private readonly CompanyContext _dbContext; // Lub inna implementacja dostępu do danych
        private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction _transaction;

        public TransactionManager(CompanyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
        }
    }
}
