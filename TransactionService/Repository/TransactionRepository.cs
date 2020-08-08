using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionServices.Model.Entity;
using TransactionServices.Model.ViewModel;

namespace TransactionServices.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private TransactionContext context;
        private readonly IMapper _mapper;
        public TransactionRepository(TransactionContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }

        public async Task<List<Transactions>> GetAllTransactions()
        {
            return await context.TransactionEntity.ToListAsync();
        }

        public async Task<List<Transactions>> GetAllTransactions(TransactionFilter transactionFilter)
        {
            IQueryable<Transactions> result = context.TransactionEntity;
            if (!string.IsNullOrEmpty(transactionFilter.CurrencyCode))
            {
                result = result.Where(q => q.CurrencyCode == transactionFilter.CurrencyCode);
            }
            if (!string.IsNullOrEmpty(transactionFilter.TransactionStatus))
            {
                result = result.Where(q => q.Status == transactionFilter.TransactionStatus);
            }
            result = result.Where(q => q.TransactionDate >= transactionFilter.TransDateFrom && q.TransactionDate <= transactionFilter.TransDateTO);

            return await result.ToListAsync();
        }

        public async Task<int> UploadTransaction(List<Transactions> transactions)
        {
            await context.TransactionEntity.AddRangeAsync(transactions);
            var success = await context.SaveChangesAsync();
            return success;
        }

        public async Task<string[]> GetCurrency()
        {
            return await context.TransactionEntity.Select(q => q.CurrencyCode).Distinct().OrderBy(o => o).ToArrayAsync();
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
