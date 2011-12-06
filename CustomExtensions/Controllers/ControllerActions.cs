using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;

namespace CustomExtensions.Controllers
{
    public class ControllerActions : List<ControllerAction>
    {
        private static readonly Lazy<ControllerActions> CurrentActionsThunk = 
            new Lazy<ControllerActions>(DiscoverControllerActions);

        public static ControllerActions Current
        {
            get { return _current = _current ?? CurrentActionsThunk.Value; }
            set { _current = value; }
        }
        private static ControllerActions _current;


        public ControllerActions() : this(null)
        {
        }

        public ControllerActions(IEnumerable<ControllerAction> controllerActions)
            : base(controllerActions ?? Enumerable.Empty<ControllerAction>())
        {
        }


        public static ControllerActions DiscoverControllerActions()
        {
            var referencedAssemblies = 
                BuildManager
                    .GetReferencedAssemblies()
                    .Cast<Assembly>();

            var controllerActions =
                from assembly in referencedAssemblies
                from controller in assembly.GetExportedTypes()
                where typeof (IController).IsAssignableFrom(controller)
                let controllerAttributes = controller.GetCustomAttributes(true).Cast<Attribute>()
                from action in controller.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                let actionAttributes = action.GetCustomAttributes(true).Cast<Attribute>()
                select new ControllerAction(controller, action, controllerAttributes, actionAttributes);

            return new ControllerActions(controllerActions);
        }
    }
}
