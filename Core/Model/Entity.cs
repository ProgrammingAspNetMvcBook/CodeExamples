using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    public interface IEntity
    {
        /// <summary>
        /// The entity's persistent (database-generated) identifier
        /// </summary>
        /// <remarks>
        /// This property should be considered internal application logic
        /// and is not for public consumption
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        long Id { get; }

        /// <summary>
        /// The entity's unique (and URL-safe) public identifier
        /// </summary>
        /// <remarks>
        /// This is the identifier that should be exposed via the web, etc.
        /// </remarks>
        string Key { get; }
    }

    public abstract class Entity : IEntity
    {
        public virtual long Id { get; set; }

        [Unique, StringLength(50)]
        public virtual string Key
        {
            get { return _key = _key ?? GenerateKey(); }
            set { _key = value; }
        }
        private string _key;

        protected virtual string GenerateKey()
        {
            return KeyGenerator.Generate();
        }
    }
}