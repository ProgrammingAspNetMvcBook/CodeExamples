using System;

namespace CustomExtensions.Routing
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class RouteAttribute : Attribute
    {
        /// <summary>
        /// JSON object containing route data part constraints
        /// </summary>
        public string Constraints { get; set; }

        /// <summary>
        /// JSON object containing route data part defaults
        /// </summary>
        public string Defaults { get; set; }

        /// <summary>
        /// Url routing pattern, including route data part placeholders
        /// </summary>
        public string Pattern { get; set; }


        public RouteAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}