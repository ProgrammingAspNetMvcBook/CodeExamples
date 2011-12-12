using System;
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
    }
}