using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    public abstract class Entity
    {
        [Key]
        internal virtual int Id { get; private set; }

        /// <summary>
        /// A unique (URL-safe) identifier
        /// </summary>
        public virtual string Key
        {
            get { return _key = _key ?? GenerateKey(); }
            private set { _key = value; }
        }
        private string _key;


        protected virtual string GenerateKey()
        {
            return KeyGenerator.Generate();
        }

    }

    /// <summary>
    /// Base class for Entity Metadata classes that
    /// holds the validation logic for each entity type
    /// </summary>
    public abstract class EntityMetadata
    {
        [Required]
        public object Key { get; set; }
    }
}