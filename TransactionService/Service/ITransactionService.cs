using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionServices.Model.ViewModel;

namespace TransactionServices.Service
{
    public interface ITransactionService
    {
        Task<TransactionResponsModel> GetAllTransactionAsync();
    }
}
