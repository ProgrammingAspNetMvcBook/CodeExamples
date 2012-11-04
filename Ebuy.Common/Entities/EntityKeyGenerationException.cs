using System;

namespace Ebuy
{
    public class EntityKeyGenerationException : Exception
    {
        public Type EntityType { get; set; }

        public EntityKeyGenerationException(Type entityType, string message)
            : base(message)
        {
            EntityType = entityType;
        }
    }
}