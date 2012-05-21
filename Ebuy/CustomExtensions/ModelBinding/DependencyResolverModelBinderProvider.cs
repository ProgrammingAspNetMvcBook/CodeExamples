using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace Ebuy.Web.ModelBinding
{
    public class DependencyResolverModelBinderProvider<TModelBinder> : IModelBinderProvider
        where TModelBinder : IModelBinder
    {
        private readonly Func<Type, bool> _appliesToType;

        public DependencyResolverModelBinderProvider(Func<Type, bool> appliesToType)
        {
            Contract.Requires(_appliesToType != null);

            _appliesToType = appliesToType;
        }

        public IModelBinder GetBinder(Type modelType)
        {
            if (_appliesToType(modelType))
                return DependencyResolver.Current.GetService<TModelBinder>();
            else
                return null;
        }
    }
}