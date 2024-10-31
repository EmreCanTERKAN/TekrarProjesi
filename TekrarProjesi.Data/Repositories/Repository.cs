using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Data.Context;
using TekrarProjesi.Data.Entities;

namespace TekrarProjesi.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {

        private readonly TekrarAppDbContext _db;
        private readonly DbSet<TEntity> _dbset;
        public Repository(TekrarAppDbContext db)
        {
            _db = db;
            _dbset = _db.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbset.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.IsDeleted = true;
            _dbset.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbset.Find(id);
            Delete(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is null ? _dbset : _dbset.Where(predicate);
        }

        public TEntity GetById(int id)
        {
            return _dbset.Find(id);
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbset.Update(entity);
        }
    }
}
