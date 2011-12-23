using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace CustomExtensions.DataAnnotations
{
    public class UniqueConstraintApplier
    {
        private const string UniqueConstraintQuery = "ALTER TABLE [{0}] ADD CONSTRAINT [{0}_{1}_unique] UNIQUE ([{1}])";

        public void ApplyUniqueConstraints(DbContext context)
        {
            var modelTypes =
                from dbContextProperties in context.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                let propertyTypeGenericArguments = dbContextProperties.PropertyType.GetGenericArguments()
                where propertyTypeGenericArguments.Count() == 1
                select propertyTypeGenericArguments.Single();

            var modelsWithUniqueProperties =
                from modelType in modelTypes
                from property in modelType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                from uniqueAttribute in property.GetCustomAttributes(true).OfType<UniqueAttribute>()
                let propertyName = property.Name

                group propertyName by modelType into uniquePropertiesByModel

                select new {
                        Model = uniquePropertiesByModel.Key,
                        Properties = (IEnumerable<string>) uniquePropertiesByModel
                    };

            foreach (var model in modelsWithUniqueProperties)
            {
                foreach (var property in model.Properties)
                {
                    string tableName = GetTableName(model.Model);
                    string query = string.Format(UniqueConstraintQuery, tableName, property);
                    context.Database.ExecuteSqlCommand(query);
                }
            }
        }

        private string GetTableName(Type model)
        {
            var modelName = model.Name;

            if (modelName.EndsWith("y"))
                modelName = modelName.Substring(0, modelName.Length - 1) + "ie";

            return modelName + "s";
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueAttribute : RequiredAttribute
    {
    }
}
