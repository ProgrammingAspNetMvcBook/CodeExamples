using System.Data.Entity;

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
            modelBuilder.Entity<Bid>()
                .HasRequired(x => x.Auction)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}