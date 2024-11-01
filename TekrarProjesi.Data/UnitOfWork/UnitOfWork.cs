using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Data.Context;

namespace TekrarProjesi.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TekrarAppDbContext _db;
        private IDbContextTransaction _transaction;

        public UnitOfWork(TekrarAppDbContext db)
        {
            _db = db;
        }
        public async Task BeginTransaction()
        {
           _transaction = await _db.Database.BeginTransactionAsync();
        }
        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
