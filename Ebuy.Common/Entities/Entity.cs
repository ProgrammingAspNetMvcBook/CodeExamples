using System;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    public interface IEntity
    {
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

            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
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