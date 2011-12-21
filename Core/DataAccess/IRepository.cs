using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ebuy.DataAccess
{
    public interface IRepository : IDisposable
    {
        IQueryable<TModel> All<TModel>(int pageIndex = 0, int pageSize = 25) where TModel : class, IEntity;

        void Add<TModel>(TModel instance) where TModel : class, IEntity;
        void Add<TModel>(IEnumerable<TModel> instances) where TModel : class, IEntity;

        void Delete<TModel>(string key) where TModel : class, IEntity;
        void Delete<TModel>(TModel instance) where TModel : class, IEntity;
        void Delete<TModel>(Expression<Func<TModel, bool>> predicate) where TModel : class, IEntity;

        TModel Single<TModel>(string key) where TModel : class, IEntity;
        TModel Single<TModel>(Expression<Func<TModel, bool>> predicate) where TModel : class, IEntity;

        IQueryable<TModel> Query<TModel>(Expression<Func<TModel, bool>> predicate) where TModel : class, IEntity;
    }
}