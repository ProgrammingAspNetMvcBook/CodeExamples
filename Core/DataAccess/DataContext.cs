using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Ebuy.DataAccess
{
    public partial class DataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext()
        {
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.ValidateOnSaveEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
/*
            modelBuilder.Entity<Auction>()
                .HasMany(x => x.Bids)
                .WithOptional()
                .Map(x => x.MapKey("Auction_Id"));
*/

            modelBuilder.Entity<Bid>()
                .HasRequired(x => x.Auction)
                .WithMany()
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Bid>()
                .HasRequired(x => x.User)
                .WithMany()
                .WillCascadeOnDelete(false);
            
/*
            modelBuilder.Entity<User>()
                .HasMany(x => x.Bids)
                .WithOptional()
                .Map(x => x.MapKey("User_Id"));
*/
        }
    }
}
