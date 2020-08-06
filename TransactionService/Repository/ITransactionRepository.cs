using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionService.Model.Entity;

namespace TransactionService.Repository
{
    public interface ITransactionRepository
    {
        Task<List<Transactions>> GetAllTransactions();
        void Commit();
    }
}
