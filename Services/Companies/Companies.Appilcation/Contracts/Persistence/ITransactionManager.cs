using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Contracts.Persistence
{
    public interface ITransactionManager
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
