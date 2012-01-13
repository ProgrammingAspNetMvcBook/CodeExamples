using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Ebuy.DataAccess
{
    public partial class DataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceError("Property: {0} Error: {1}",
                                         validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bid>()
                .HasRequired(x => x.Auction)
                .WithMany()
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Bid>()
                .HasRequired(x => x.User)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
