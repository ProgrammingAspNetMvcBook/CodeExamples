using System.Web.Mvc;
using Ebuy.DataAccess;
using Ebuy.Web.Extensions;

namespace Ebuy.Web.ModelBinding
{
    public class UserModelBinder : IModelBinder
    {
        private readonly IRepository _repository;

        public UserModelBinder(IRepository repository)
        {
            _repository = repository;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var context = controllerContext.HttpContext;
            var identity = context.User.Identity;

            var user = context.CurrentUser();

            if (user == null && context.User.Identity.IsAuthenticated)
                user = _repository.Single<User>(identity.Name);

            return user;
        }
    }
}
