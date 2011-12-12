using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CustomExtensions.DataAnnotations
{
    [Serializable]
    public class IsNotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return IsValid(value as IEnumerable);
        }

        public bool IsValid(IEnumerable collection)
        {
            return collection != null 
                && collection.Cast<object>().Any();
        }
    }
}