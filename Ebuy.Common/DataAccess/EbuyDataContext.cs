using System.Data.Entity;
using Ebuy.Mappings;

namespace Ebuy.DataAccess
{
    public class EbuyDataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }

        public EbuyDataContext()
        {
#if(DEBUG)
            var initializer = new DropCreateDatabaseIfModelChanges<EbuyDataContext>();
            Database.SetInitializer(initializer);
#endif
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			modelBuilder.Configurations.Add(new BidConfiguration());
        }
    }
}