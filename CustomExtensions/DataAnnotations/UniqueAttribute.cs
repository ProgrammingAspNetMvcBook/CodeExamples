using System;
using System.Collections.Generic;

namespace CustomExtensions.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class UniqueAttribute : Attribute
    {
        public IEnumerable<string> KeyFields { get; private set; }

        public UniqueAttribute(string keyFields)
        {
            KeyFields = keyFields.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
