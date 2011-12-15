using System;
using System.Collections.ObjectModel;
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
            //: DropCreateDatabaseIfModelChanges<DataContext>
            : DropCreateDatabaseAlways<DataContext> 
        {
            protected override void Seed(DataContext context)
            {
                // Apply the custom UniqueAttribute to set unique constraints
                // on columns with the attribute defined
                new UniqueConstraintApplier().ApplyUniqueConstraints(context);

#if(DEBUG)
                // Save a copy of the EDMX to disk for debug purposes
                WriteEdmx(context);
#endif

                base.Seed(context);


                var frankSinatra = new User()
                                       {
                                           DisplayName = "Old Blue Eyes",
                                           EmailAddress = "frank@theratpack.com",
                                           FullName = "Frank Sinatra",
                                       };
                
                var freddieMercury = new User()
                                       {
                                           DisplayName = "Freddie Mercury",
                                           EmailAddress = "freddie@queenband.com",
                                           FullName = "Farrokh Bulsara",
                                       };

                var johnLennon = new User()
                                       {
                                           DisplayName = "The Reverend Fred Gherkin",
                                           EmailAddress = "lenny@thebeatles.com",
                                           FullName = "John Lennon",
                                       };


                var electronics = new Category() { Name = "Electronics" };


                var xbox360Elite = new Product() {
                            Categories = new Collection<Category>() { electronics },
                            Description = "The Xbox 360 Elite gaming system is the ultimate in gaming",
                            ImageUrl = "http://nettechies.files.wordpress.com/2010/08/xbox-360-elite.jpg",
                            Name = "Xbox 360 Elite",
                            Reviews = new Collection<Review> {
                                    new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                    new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                                    new Review { User = freddieMercury, Description = "I love mine so much I want another!", Rating = 4 },
                                }
                        };


                DateTime startTime = DateTime.Parse("2011-08-01 12:32:00");

                context.Auctions.Add(
                    new Auction() {
                        Owner = johnLennon,
                        Product = xbox360Elite,
                        StartTime = startTime,
                        EndTime = startTime.AddDays(7),
                        StartingPrice = "$20",
                        Bids = new Collection<Bid>() {
                                new Bid { User = freddieMercury, Price = "$20",     Timestamp = startTime.AddHours(2) },
                                new Bid { User = frankSinatra,   Price = "$21.5",   Timestamp = startTime.AddHours(4) },
                                new Bid { User = freddieMercury, Price = "$24.51",  Timestamp = startTime.AddHours(5) },
                                new Bid { User = frankSinatra,   Price = "$32.81",  Timestamp = startTime.AddHours(6) },
                                new Bid { User = freddieMercury, Price = "$57.01",  Timestamp = startTime.AddHours(8) },
                                new Bid { User = frankSinatra,   Price = "$82.84",  Timestamp = startTime.AddHours(10) },
                                new Bid { User = freddieMercury, Price = "$102.01", Timestamp = startTime.AddHours(13) },
                            }
                    });
            }

            protected void WriteEdmx(DataContext context, XmlWriter writer = null)
            {
                try
                {
                    if (writer == null)
                    {
                        var filename = GetType().Name + ".edmx";
                        writer = new XmlTextWriter(filename, Encoding.Default);
                    }

                    EdmxWriter.WriteEdmx(context, writer);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
