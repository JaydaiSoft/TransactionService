using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionServices.Model.Entity;

namespace TransactionServices.Repository
{
    public interface ITransactionRepository
    {
        Task<List<Transactions>> GetAllTransactions();
        Task<List<Transactions>> GetAllTransactions(string KeySearch, string KeyValue, DateTime? transDateFrom, DateTime? transDateTO);
        Task<int> UploadTransaction(List<Transactions> transactions);
        Task<string[]> GetCurrency();
        void Commit();
    }
}
