using System;
using System.ComponentModel.DataAnnotations;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    public interface IEntity
    {
        /// <summary>
        /// The entity's unique (and URL-safe) public identifier
        /// </summary>
        /// <remarks>
        /// This is the identifier that should be exposed via the web, etc.
        /// </remarks>
        string Key { get; }
    }

    public abstract class Entity<TId> : IEntity, IEquatable<Entity<TId>>
        where TId : struct
    {
        [Key]
        public virtual TId Id
        {
            get
            {
                if (_id == null && typeof(TId) == typeof(Guid))
                    _id = Guid.NewGuid();

                return _id == null ? default(TId) : (TId)_id;
            }
            protected set { _id = value; }
        }
        private object _id;

        [Unique, StringLength(50)]
        public virtual string Key
        {
            get { return _key = _key ?? GenerateKey(); }
            protected set { _key = value; }
        }
        private string _key;


        protected virtual string GenerateKey()
        {
            return KeyGenerator.Generate();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Entity<TId>)) return false;
            return Equals((Entity<TId>)obj);
        }

        public bool Equals(Entity<TId> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;

            if (default(TId).Equals(Id) || default(TId).Equals(other.Id))
                return Equals(other._key, _key);

            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (default(TId).Equals(Id))
                    return Key.GetHashCode() * 397;

                return Id.GetHashCode();
            }
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }
    }
}