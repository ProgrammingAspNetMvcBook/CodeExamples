using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Ebuy
{
    public class JsonModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string json = string.Empty;

            var provider = bindingContext.ValueProvider;

            var providerValue = provider.GetValue(bindingContext.ModelName);
            
            if (providerValue != null)
                json = providerValue.AttemptedValue;
            
            // Basic expression to make sure the string starts and ends
            // with JSON object ( {} ) or array ( [] ) characters
            if (Regex.IsMatch(json, @"^(\[.*\]|{.*})$"))
            {
                return new JavaScriptSerializer().Deserialize(json, bindingContext.ModelType);
            }
            
            return base.BindModel(controllerContext, bindingContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum |
     AttributeTargets.Interface | AttributeTargets.Parameter |
     AttributeTargets.Struct | AttributeTargets.Property,
     AllowMultiple = false, Inherited = false)]
    public class JsonModelBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }
    }
}