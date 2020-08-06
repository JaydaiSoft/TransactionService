using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionService.Model.Entity;

namespace TransactionService.Repository
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

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}
