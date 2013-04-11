using System.Data.Entity;

namespace Ebuy.DataAccess
{
    public class EbuyDataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
		public DbSet<Bid> Bids { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<User> Users { get; set; }

        public EbuyDataContext()
        {
#if(DEBUG)
			Database.SetInitializer(new EbuyInitializer());
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