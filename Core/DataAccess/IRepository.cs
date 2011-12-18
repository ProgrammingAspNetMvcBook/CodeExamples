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
        void DeleteByKey(string key);

        TModel Find(Expression<Func<TModel, bool>> predicate);
        TModel FindById(long id);
        TModel FindByKey(string key);

        IQueryable<TModel> Query(int pageIndex = 0, int pageSize = 25);
        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate, out int count, int pageIndex, int pageSize);

        void Save(TModel instance);
        void Save(IEnumerable<TModel> instances);
    }
}