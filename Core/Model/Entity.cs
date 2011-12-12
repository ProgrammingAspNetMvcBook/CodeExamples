using System.ComponentModel.DataAnnotations;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    public interface IEntity
    {
        long Id { get; }
    }

    public abstract class Entity : IEntity
    {
        public virtual long Id { get; set; }

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