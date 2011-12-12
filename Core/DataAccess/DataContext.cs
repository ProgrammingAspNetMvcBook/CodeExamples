using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Xml;
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

#if(DEBUG)
                WriteEdmx(context);
#endif

                base.Seed(context);
            }

            protected void WriteEdmx(DataContext context, XmlWriter writer = null)
            {
                if (writer == null)
                {
                    var filename = GetType().Name + ".edmx";
                    writer = new XmlTextWriter(filename, Encoding.Default);
                }

                EdmxWriter.WriteEdmx(context, writer);
            }
        }
    }
}
