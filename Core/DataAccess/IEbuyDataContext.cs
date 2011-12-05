using System.Linq;

namespace Ebuy.DataAccess
{
    public interface IEbuyDataContext
    {
        IQueryable<Product> Products { get; }
        IQueryable<User> Users { get; }
    }
}
