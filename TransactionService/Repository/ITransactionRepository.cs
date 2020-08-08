using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionServices.Model.Entity;
using TransactionServices.Model.ViewModel;

namespace TransactionServices.Repository
{
    public interface ITransactionRepository
    {
        Task<List<Transactions>> GetAllTransactions();
        Task<List<Transactions>> GetAllTransactions(TransactionFilter transactionFilter);
        Task<int> UploadTransaction(List<Transactions> transactions);
        Task<string[]> GetCurrency();
        void Commit();
    }
}
