using System.Data.Entity;
using CustomExtensions.DataAnnotations;

namespace Ebuy.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }


        public class Initializer
            //: DropCreateDatabaseIfModelChanges<EbuyDataContext>
            : DropCreateDatabaseAlways<DataContext> 
        {
            protected override void Seed(DataContext context)
            {
                new UniqueConstraintApplier().ApplyUniqueConstraints(context);

                base.Seed(context);
            }
        }
    }
}
