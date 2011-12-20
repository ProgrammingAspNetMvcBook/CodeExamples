using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Ebuy.DataAccess;
using Ebuy.Website.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DataAccess), "Start")]

namespace Ebuy.Website.App_Start
{
    public static class DataAccess
    {
        public static void Start() 
        {
#if(DEBUG)
            Database.SetInitializer(new DemoDataContextInitializer());
#else
            Database.SetInitializer(new DataContext.Initializer());
#endif
        }

        class DemoDataContextInitializer : DataContext.Initializer
        {
            protected override void Seed(DataContext context)
            {
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


                var videoGameSystems = context.Categories.Local.Single(x => x.Name == "Video Game Systems");
                var sports = context.Categories.Local.Single(x => x.Name == "Sporting Goods");
                var collectibles = context.Categories.Local.Single(x => x.Name == "Collectibles");


                context.Products.Add(new Product
                {
                    Categories = new[] { videoGameSystems },
                    Name = "Xbox 360 Elite",
                    Description = "The Xbox 360 Elite gaming system is the ultimate in gaming",
                    Images = new WebsiteImage[] { "~/Content/images/products/xbox360elite.jpg" },
                    Reviews = new[] {
                                new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                                new Review { User = freddieMercury, Description = "I love mine so much I want another!", Rating = 4 },
                            }
                });

                context.Products.Add(new Product
                {
                    Categories = new[] { videoGameSystems },
                    Name = "Sony PSP Go",
                    Description = "The smallest and mightiest PSP system yet.",
                    Images = new WebsiteImage[] { "~/Content/images/products/psp.jpg" },
                    Reviews = new[] {
                                new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                            }
                });

                context.Products.Add(new Product
                {
                    Categories = new[] { videoGameSystems },
                    Name = "Xbox 360 Kinect Sensor with Game Bundle",
                    Description = "You are the controller with Kinect for Xbox 360!",
                    Images = new WebsiteImage[] { "~/Content/images/products/kinect.jpg" },
                    Reviews = new[] {
                                new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                            }
                });

                context.Products.Add(new Product
                {
                    Categories = new[] { videoGameSystems },
                    Name = "Sony Playstation 3 120GB Slim Console",
                    Description = "The fourth generation of hardware released for the PlayStation 3 entertainment platform, the PlayStation 3 120GB system is the next stage in the evolution of Sony's console gaming powerhouse.",
                    Images = new WebsiteImage[] { "~/Content/images/products/ps3.jpg" },
                    Reviews = new[] {
                                new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                            }
                });

                context.Products.Add(new Product
                {
                    Categories = new[] { videoGameSystems },
                    Name = "Nintendo Wii Console Black",
                    Description = "Wii Sports Resort takes the inclusive, fun and intuitive controls of the original Wii Sports to the next level, introducing a whole new set of entertaining and physically immersive activities.",
                    Images = new WebsiteImage[] { "~/Content/images/products/wii.jpg" },
                    Reviews = new[] {
                                new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                            }
                });

                context.Products.Add(new Product
                {
                    Categories = new Collection<Category> { sports },
                    Name = "Burton Mayhem snow board",
                    Description = "Burton Mayhem snow board: 159cm wide",
                    Images = new WebsiteImage[] { "~/Content/images/products/burtonMayhem.jpg" },
                    Reviews = new[] {
                                new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                            }
                });

                context.Products.Add(new Product
                {
                    Categories = new Collection<Category> { collectibles },
                    Name = "Lock of John Lennon's hair",
                    Description = "Lock of John Lennon's hair",
                    Images = new WebsiteImage[] { "~/Content/images/products/lockOfHair.jpg" },
                    Reviews = new[] {
                                new Review { User = frankSinatra, Description = "It's really awesome!", Rating = 4.5 },
                                new Review { User = johnLennon, Description = "It's quite horrible", Rating = 2 },
                            }
                });


                var rand = new Random();
                var users = context.Users.Local.ToArray();

                foreach (var product in context.Products.Local)
                {
                    var startTime = new DateTime(2011, rand.Next(1, 12), rand.Next(1, 28), rand.Next(0, 24), rand.Next(0, 60), 0);

                    var auction = new Auction()
                    {
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


                    var nonOwners = users.Except(new[] { auction.Owner }).ToArray();
                    var lastBid = new Bid { Price = auction.StartingPrice, Timestamp = auction.StartTime };

                    for (int x = 0; x < rand.Next(4, 20); x++)
                    {
                        var user = nonOwners[rand.Next(nonOwners.Length)];
                        var amount = new Currency(string.Format("${0}", lastBid.Price.Amount + rand.Next(1, 20)));
                        user.Bid(auction, amount);
                    }

                    context.Auctions.Add(auction);
                }
            }
        }
    }
}
