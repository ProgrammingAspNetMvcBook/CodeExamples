using System.Collections.Generic;
using System.Data.Entity;
using CustomExtensions.DataAnnotations;

namespace Ebuy.DataAccess
{
    public class EbuyDataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }


        public class Initializer : DropCreateDatabaseAlways<EbuyDataContext> //DropCreateDatabaseIfModelChanges<EbuyDataContext>
        {
            protected override void Seed(EbuyDataContext context)
            {
                new UniqueConstraintApplier().ApplyUniqueConstraints(context);

                base.Seed(context);
            }
        }
    }
}
