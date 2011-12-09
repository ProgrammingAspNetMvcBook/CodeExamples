using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ebuy.DataAccess
{
    public interface IRepository<TModel> : IDisposable
        where TModel : Entity
    {
        void Delete(string key);
        void Delete(TModel instance);
        void Delete(Expression<Func<TModel, bool>> predicate);

        TModel Find(string key);
        TModel Find(Expression<Func<TModel, bool>> predicate);

        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate, out int count, int pageIndex = 0, int pageSize = 25);

        void Save(TModel instance);
    }
}