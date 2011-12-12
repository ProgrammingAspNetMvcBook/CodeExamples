namespace Ebuy
{
    public interface IEntity
    {
        long Id { get; }
    }

    public abstract class Entity : IEntity
    {
        public virtual long Id { get; set; }
    }
}