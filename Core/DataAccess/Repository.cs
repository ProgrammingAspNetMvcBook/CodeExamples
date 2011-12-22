using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Ebuy.DataAccess
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
        private readonly bool _isSharedContext;


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


        public IQueryable<TModel> All<TModel>(int pageIndex = 0, int pageSize = 25)
            where TModel : class, IEntity
        {
            return _context.Set<TModel>().Page(pageIndex, pageSize);
        }

        public void Dispose()
        {
            // If this is a shared (or null) context then
            // we're not responsible for disposing it
            if (_isSharedContext || _context == null)
                return;
            
            _context.Dispose();
        }

        public void Delete<TModel>(string key) 
            where TModel : class, IEntity
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));

            var entity = Single<TModel>(key);
            Delete(entity);
        }

        public void Delete<TModel>(TModel instance)
            where TModel : class, IEntity
        {
            Contract.Requires(instance != null);

            if (instance != null)
                _context.Set<TModel>().Remove(instance);
        }

        public void Delete<TModel>(Expression<Func<TModel, bool>> predicate)
            where TModel : class, IEntity
        {
            Contract.Requires(predicate != null);

            TModel entity = Single(predicate);

            Delete(entity);
        }

        public TModel Single<TModel>(string key)
            where TModel : class, IEntity
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));

            var entity = Single<TModel>(x => x.Key == key);
            return entity;
        }

        public TModel Single<TModel>(Expression<Func<TModel, bool>> predicate)
            where TModel : class, IEntity
        {
            Contract.Requires(predicate != null);

            var instance = _context.Set<TModel>().SingleOrDefault(predicate);
            return instance;
        }

        public IQueryable<TModel> Query<TModel>(Expression<Func<TModel, bool>> predicate)
            where TModel : class, IEntity
        {
            Contract.Requires(predicate != null);

            IQueryable<TModel> items = _context.Set<TModel>();

            if (predicate != null)
                items = items.Where(predicate);

            return items;
        }

        public void Add<TModel>(TModel instance)
            where TModel : class, IEntity
        {
            Contract.Requires(instance != null);

            if (instance.Id == default(long))
                _context.Entry(instance).State = EntityState.Added;
            else
                return;

            if (_isSharedContext == false)
                _context.SaveChanges();
        }

        public void Add<TModel>(IEnumerable<TModel> instances)
            where TModel : class, IEntity
        {
            Contract.Requires(instances != null);

            foreach (var instance in instances)
            {
                Add(instance);
            }
        }
    }
}
