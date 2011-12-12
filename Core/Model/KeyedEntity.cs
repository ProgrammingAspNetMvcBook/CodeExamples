using System.ComponentModel.DataAnnotations;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    public interface IKeyedEntity : IEntity
    {
        string Key { get; }
    }

    public abstract class KeyedEntity : Entity, IKeyedEntity
    {
        /// <summary>
        /// A unique (URL-safe) identifier
        /// </summary>
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