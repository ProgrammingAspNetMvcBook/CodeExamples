using System.Data.Entity;

namespace Ebuy.DataAccess
{
    public class EbuyDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class EbuyDataContextInitializer : DropCreateDatabaseIfModelChanges<EbuyDataContext>
    {
    }
}
