using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ebuy.DataAccess
{
    public interface IRepository<TModel> : IDisposable
        where TModel : IEntity
    {
        void Delete(TModel instance);
        void Delete(Expression<Func<TModel, bool>> predicate);
        void DeleteById(long id);

        TModel Find(Expression<Func<TModel, bool>> predicate);
        TModel FindById(long id);

        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate, out int count, int pageIndex = 0, int pageSize = 25);

        void Save(TModel instance);
        void Save(IEnumerable<TModel> instances);
    }

    public interface IKeyedRepository<TModel> : IRepository<TModel>
        where TModel : IKeyedEntity
    {
        void DeleteByKey(string key);
        TModel FindByKey(string key);
    }
}