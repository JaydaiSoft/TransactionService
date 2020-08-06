using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionService.Model.Entity;

namespace TransactionService.Repository
{
    public interface ITransactionContext
    {
        DbSet<Transactions> TransactionEntity { get; set; }
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}
