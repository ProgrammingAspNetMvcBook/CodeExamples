using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Xml;
using CustomExtensions.DataAnnotations;

namespace Ebuy.DataAccess
{
    public partial class DataContext
    {
        public class Initializer
#if(DEBUG)
            : DropCreateDatabaseAlways<DataContext>
#else
            : CreateDatabaseIfNotExists<DataContext>
#endif
        {
            protected override void Seed(DataContext context)
            {
                // Apply the custom UniqueAttribute to set unique constraints
                // on columns with the attribute defined
                new UniqueConstraintApplier().ApplyUniqueConstraints(context);

                base.Seed(context);


                var frankSinatra = new User()
                                       {
                                           DisplayName = "Old Blue Eyes",
                                           EmailAddress = "frank@theratpack.com",
                                           FullName = "Frank Sinatra",
                                       };
                context.Users.Add(frankSinatra);

                var freddieMercury = new User()
                                       {
                                           DisplayName = "Freddie Mercury",
                                           EmailAddress = "freddie@queenband.com",
                                           FullName = "Farrokh Bulsara",
                                       };
                context.Users.Add(freddieMercury);

                var johnLennon = new User()
                                       {
                                           DisplayName = "The Reverend Fred Gherkin",
                                           EmailAddress = "lenny@thebeatles.com",
                                           FullName = "John Lennon",
                                       };
                context.Users.Add(johnLennon);


                var electronics = new Category() { Name = "Electronics" };
                var sports = new Category() { Name = "Sports and Recreation" };
                var collectibles = new Category() { Name = "Collectibles" };

                context.Products.Add(new Product()
                {
                    Categories = new Collection<Category>() { electronics },
                    Description = "The Xbox 360 Elite gaming system is the ultimate in gaming",
                    Images = new  WebsiteImage[] { "~/Content/images/products/xbox360elite.jpg" },
                    Name = "Xbox 360 Elite",
                    Reviews = new Collection<Review> {
                                    new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                    new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                                    new Review { User = freddieMercury, Description = "I love mine so much I want another!", Rating = 4 },
                                }
                });

                context.Products.Add(new Product()
                              {
                                  Categories = new Collection<Category>() { electronics },
                                  Name = "Sony PSP Go",
                                  Description = "The smallest and mightiest PSP system yet.",
                                  Images = new WebsiteImage[] { "~/Content/images/products/psp.jpg" },
                                  Reviews = new Collection<Review>
                                                {
                                                    new Review
                                                        {
                                                            User = frankSinatra,
                                                            Description = "It's really awesome!",
                                                            Rating = 4.5
                                                        },
                                                    new Review
                                                        {
                                                            User = johnLennon,
                                                            Description = "It's quite horrible",
                                                            Rating = 2
                                                        },
                                                   
                                                }

                              });

                context.Products.Add(new Product()
                {
                    Categories = new Collection<Category>() { electronics },
                    Name = "Xbox 360 Kinect Sensor with Game Bundle",
                    Description = "You are the controller with Kinect for Xbox 360!",
                    Images = new WebsiteImage[] { "~/Content/images/products/kinect.jpg" },
                    Reviews = new Collection<Review>
                                                {
                                                    new Review
                                                        {
                                                            User = frankSinatra,
                                                            Description = "It's really awesome!",
                                                            Rating = 4.5
                                                        },
                                                    new Review
                                                        {
                                                            User = johnLennon,
                                                            Description = "It's quite horrible",
                                                            Rating = 2
                                                        },
                                                   
                                                }

                });

                context.Products.Add(new Product()
                {
                    Categories = new Collection<Category>() { electronics },
                    Name = "Sony Playstation 3 120GB Slim Console",
                    Description = "The fourth generation of hardware released for the PlayStation 3 entertainment platform, the PlayStation 3 120GB system is the next stage in the evolution of Sony's console gaming powerhouse.",
                    Images = new WebsiteImage[] { "~/Content/images/products/ps3.jpg" },
                    Reviews = new Collection<Review>
                                                {
                                                    new Review
                                                        {
                                                            User = frankSinatra,
                                                            Description = "It's really awesome!",
                                                            Rating = 4.5
                                                        },
                                                    new Review
                                                        {
                                                            User = johnLennon,
                                                            Description = "It's quite horrible",
                                                            Rating = 2
                                                        },
                                                   
                                                }

                });

                context.Products.Add(new Product()
                {
                    Categories = new Collection<Category>() { electronics },
                    Name = "Nintendo Wii Console Black",
                    Description = "Wii Sports Resort takes the inclusive, fun and intuitive controls of the original Wii Sports to the next level, introducing a whole new set of entertaining and physically immersive activities.",
                    Images = new WebsiteImage[] { "~/Content/images/products/wii.jpg" },
                    Reviews = new Collection<Review>
                                                {
                                                    new Review
                                                        {
                                                            User = frankSinatra,
                                                            Description = "It's really awesome!",
                                                            Rating = 4.5
                                                        },
                                                    new Review
                                                        {
                                                            User = johnLennon,
                                                            Description = "It's quite horrible",
                                                            Rating = 2
                                                        },
                                                   
                                                }

                });

                context.Products.Add(new Product()
                {
                    Categories = new Collection<Category>() { collectibles },
                    Name = "Lock of John Lennon's hair",
                    Description = "Lock of John Lennon's hair",
                    Images = new WebsiteImage[] { "~/Content/images/products/lockOfHair.jpg" },
                    Reviews = new Collection<Review> { }
                });


                var rand = new Random();
                var users = context.Users.Local.ToArray();

                foreach (var product in context.Products.Local)
                {
                    var startTime = new DateTime(2011, rand.Next(1, 12), rand.Next(1, 28), rand.Next(0, 24), rand.Next(0, 60), 0);

                    var auction = new Auction() {
                        Bids = new Collection<Bid>(),
                        Categories = product.Categories.ToArray(),
                        Description = product.Description,
                        EndTime = startTime.AddDays(rand.Next(3, 14)),
                        Images = product.Images.ToArray(),
                        Owner = users[rand.Next(users.Length)],
                        Product = product,
                        StartingPrice = "$1",
                        StartTime = startTime,
                        Title = product.Name,
                    };


                    var nonOwners = users.Except(new [] {auction.Owner}).ToArray();
                    var lastBid = new Bid { Price = auction.StartingPrice, Timestamp = auction.StartTime };

                    for(int x = 0; x < rand.Next(4, 20); x++)
                    {
                        var user = nonOwners[rand.Next(nonOwners.Length)];
                        user.Bid(auction, 
                                 new Currency { 
                                     Amount = lastBid.Price.Amount + rand.Next(1, 20),
                                     Code = lastBid.Price.Code,
                                 }, 
                                 lastBid.Timestamp.AddMinutes(rand.Next(1, 400)));
                    }

                    context.Auctions.Add(auction);
                }
            }
        }
    }
}
