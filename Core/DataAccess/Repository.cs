using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Ebuy.DataAccess
{
    public class Repository<TModel> : IRepository<TModel> 
        where TModel : class, IEntity
    {
        private readonly DataContext _context;
        private readonly bool _isSharedContext;

        protected DbSet<TModel> DataSource
        {
            get { return _context.Set<TModel>(); }
        }

        public IQueryable<TModel> Items
        {
            get { return DataSource.AsQueryable(); }
        }


        public Repository() 
            : this(new DataContext(), false)
        {
        }

        public Repository(DataContext context, bool isSharedContext = true)
        {
            Contract.Requires(context != null);

            _context = context;
            _isSharedContext = isSharedContext;
        }


        public void Dispose()
        {
            // If this is a shared (or null) context then
            // we're not responsible for disposing it
            if (_isSharedContext || _context == null)
                return;
            
            _context.Dispose();
        }

        public void DeleteById(long id)
        {
            var item = FindById(id);
            Delete(item);
        }

        public void Delete(TModel instance)
        {
            Contract.Requires(instance != null);

            if (instance != null)
                DataSource.Remove(instance);
        }

        public void Delete(Expression<Func<TModel, bool>> predicate)
        {
            Contract.Requires(predicate != null);

            var entity = Find(predicate);
            Delete(entity);
        }

        public TModel FindById(long id)
        {
            Contract.Requires(id > 0);

            return Find(x => x.Id == id);
        }

/*
        public TModel FindByKey(string key)
        {
            return Find(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }
*/

        public TModel Find(Expression<Func<TModel, bool>> predicate)
        {
            Contract.Requires(predicate != null);

            var instance = DataSource.SingleOrDefault(predicate);
            return instance;
        }

        public IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate)
        {
            Contract.Requires(predicate != null);

            IQueryable<TModel> items = DataSource;

            if (predicate != null)
                items = items.Where(predicate);

            return items;
        }

        public IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate, out int count, int pageIndex = 0, int pageSize = 25)
        {
            Contract.Requires(predicate != null);
            Contract.Requires(pageIndex >= 0);
            Contract.Requires(pageSize >= 0);

            var items = Query(predicate);

            int skip = pageIndex * pageSize;

            if (skip > 0)
                items = items.Skip(skip);

            items = items.Take(pageSize);

            count = items.Count();

            return items;
        }

        public void Save(TModel instance)
        {
            Contract.Requires(instance != null);

            var entry = _context.Entry(instance);

            if(instance.Id == default(long))
                entry.State = EntityState.Added;
            else
                entry.State = EntityState.Modified;

            if (_isSharedContext == false)
                _context.SaveChanges();
        }

        public void Save(IEnumerable<TModel> instances)
        {
            Contract.Requires(instances != null);

            foreach (var instance in instances)
            {
                Save(instance);
            }
        }
    }
}
