using System;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Ebuy.DataAccess
{
    public class Repository<TModel> : IRepository<TModel> 
        where TModel : Entity
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

        public void Delete(string key)
        {
            var item = Find(key);
            Delete(item);
        }

        public void Delete(TModel instance)
        {
            if (instance != null)
                DataSource.Remove(instance);
        }

        public void Delete(Expression<Func<TModel, bool>> predicate)
        {
            var items = Find(predicate);
        }

        public TModel Find(string key)
        {
            return Find(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }

        public TModel Find(Expression<Func<TModel, bool>> predicate)
        {
            var instance = DataSource.SingleOrDefault(predicate);
            return instance;
        }

        public IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate)
        {
            IQueryable<TModel> items = DataSource;

            if (predicate != null)
                items = items.Where(predicate);

            return items;
        }

        public IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate, out int count, int pageIndex = 0, int pageSize = 25)
        {
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

            if(instance.Id == 0)
                entry.State = EntityState.Added;
            else
                entry.State = EntityState.Modified;

            if (_isSharedContext == false)
                _context.SaveChanges();
        }
    }
}
