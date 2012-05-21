using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomExtensions.Controllers
{
    public class ControllerAction
    {
        public MethodInfo Action { get; set; }

        public IEnumerable<Attribute> ActionAttributes { get; set; }
        
        public Type Controller { get; set; }
        
        public IEnumerable<Attribute> ControllerAttributes { get; set; }
        
        public string ControllerShortName { get; set; }

        public IEnumerable<Attribute> Attributes
        {
            get { return ActionAttributes.Union(ControllerAttributes); }
        }
        

        public ControllerAction(
                Type controller, MethodInfo action, 
                IEnumerable<Attribute> controllerAttributes,
                IEnumerable<Attribute> actionAttributes
            )
        {
            Controller = controller;
            Action = action;
            ControllerAttributes = controllerAttributes ?? Enumerable.Empty<Attribute>();
            ActionAttributes = actionAttributes ?? Enumerable.Empty<Attribute>();

            var endOfControllerName = controller.Name.Length - "Controller".Length;
            ControllerShortName = controller.Name.Substring(0, endOfControllerName);
        }
    }
}