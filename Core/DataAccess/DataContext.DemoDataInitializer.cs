using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Ebuy.DataAccess
{
    public partial class DataContext
    {
        public class DemoDataInitializer : Initializer
        {
            public DemoDataInitializer(IDatabaseInitializer<DataContext> initializer) 
                : base(initializer)
            {
            }

            protected override void Seed(DataContext context)
            {
                base.Seed(context);

                var frankSinatra = new User()
                {
                    Username = "Frank Sinatra",
                    EmailAddress = "frank@theratpack.com",
                };
                context.Users.Add(frankSinatra);

                var freddieMercury = new User()
                {
                    Username = "Freddie Mercury",
                    EmailAddress = "freddie@queenband.com",
                };
                context.Users.Add(freddieMercury);

                var johnLennon = new User()
                {
                    Username = "John Lennon",
                    EmailAddress = "lenny@thebeatles.com",
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
                int featured = 0;

                foreach (var product in context.Products.Local)
                {
                    var startTime =
                        DateTime.UtcNow
                            .AddDays(rand.Next(-10, -1))
                            .AddHours(rand.Next(1, 24))
                            .AddHours(rand.Next(1, 60));

                    var auction = new Auction()
                    {
                        Categories = product.Categories.ToArray(),
                        Description = product.Description,
                        EndTime = startTime.AddDays(rand.Next(3, 14)),
                        Images = product.Images.ToArray(),
                        Owner = users[rand.Next(users.Length)],
                        Product = product,
                        CurrentPrice = "$1",
                        StartTime = startTime,
                        Title = product.Name,
                    };

                    if (featured < 3)
                        auction.FeatureAuction();

                    context.Auctions.Add(auction);
                }
            }
        }
    }
}
