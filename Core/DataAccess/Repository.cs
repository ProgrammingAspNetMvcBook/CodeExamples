using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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


        public void Add<TModel>(TModel instance)
            where TModel : class, IEntity
        {
            Contract.Requires(instance != null);

            _context.Set<TModel>().Add(instance);

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


        public IQueryable<TModel> All<TModel>(int pageIndex = 0, int pageSize = 25, params string[] includePaths)
            where TModel : class, IEntity
        {
            return Query<TModel>(x => true, includePaths).Page(pageIndex, pageSize);
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


        public TModel Single<TModel>(string key, params string[] includePaths)
            where TModel : class, IEntity
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));

            var entity = Single<TModel>(x => x.Key == key, includePaths);
            return entity;
        }

        public TModel Single<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includePaths)
            where TModel : class, IEntity
        {
            Contract.Requires(predicate != null);

            var instance = GetSetWithIncludedPaths<TModel>(includePaths).SingleOrDefault(predicate);
            return instance;
        }


        public IQueryable<TModel> Query<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includePaths)
            where TModel : class, IEntity
        {
            Contract.Requires(predicate != null);

            var items = GetSetWithIncludedPaths<TModel>(includePaths);

            if (predicate != null)
                return items.Where(predicate);

            return items;
        }


        private DbQuery<TModel> GetSetWithIncludedPaths<TModel>(IEnumerable<string> includedPaths) where TModel : class, IEntity
        {
            DbQuery<TModel> items = _context.Set<TModel>();

            foreach (var path in includedPaths ?? Enumerable.Empty<string>())
            {
                items = items.Include(path);
            }

            return items;
        }
    }
}
