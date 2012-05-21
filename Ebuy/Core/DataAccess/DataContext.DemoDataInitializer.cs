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
            readonly Random rand = new Random();

            public DemoDataInitializer(IDatabaseInitializer<DataContext> initializer) 
                : base(initializer)
            {
            }

            protected override void Seed(DataContext context)
            {
                base.Seed(context);

                context.Users.Add(new User {
                    Username = "Frank Sinatra",
                    EmailAddress = "frank@theratpack.com",
                });

                context.Users.Add(new User {
                    Username = "Freddie Mercury",
                    EmailAddress = "freddie@queenband.com",
                });

                context.Users.Add(new User {
                    Username = "John Lennon",
                    EmailAddress = "lenny@thebeatles.com",
                });


                var videoGameSystems = context.Categories.Local.Single(x => x.Name == "Video Game Systems");

                context.Auctions.Add(new Auction
                {
                    Categories = new[] { videoGameSystems },
                    Title = "Xbox 360 Elite",
                    Description = "The Xbox 360 Elite gaming system is the ultimate in gaming",
                    Images = new WebsiteImage[] { "~/Content/images/products/xbox360elite.jpg" },
                });

                context.Auctions.Add(new Auction
                {
                    Categories = new[] { videoGameSystems },
                    Title = "Sony PSP Go",
                    Description = "The smallest and mightiest PSP system yet.",
                    Images = new WebsiteImage[] { "~/Content/images/products/psp.jpg" },
                });

                context.Auctions.Add(new Auction
                {
                    Categories = new[] { videoGameSystems },
                    Title = "Xbox 360 Kinect Sensor with Game Bundle",
                    Description = "You are the controller with Kinect for Xbox 360!",
                    Images = new WebsiteImage[] { "~/Content/images/products/kinect.jpg" },
                });

                context.Auctions.Add(new Auction
                {
                    Categories = new[] { videoGameSystems },
                    Title = "Sony Playstation 3 120GB Slim Console",
                    Description = "The fourth generation of hardware released for the PlayStation 3 entertainment platform, the PlayStation 3 120GB system is the next stage in the evolution of Sony's console gaming powerhouse.",
                    Images = new WebsiteImage[] { "~/Content/images/products/ps3.jpg" },
                });

                context.Auctions.Add(new Auction
                {
                    Categories = new[] { videoGameSystems },
                    Title = "Nintendo Wii Console Black",
                    Description = "Wii Sports Resort takes the inclusive, fun and intuitive controls of the original Wii Sports to the next level, introducing a whole new set of entertaining and physically immersive activities.",
                    Images = new WebsiteImage[] { "~/Content/images/products/wii.jpg" },
                });



                var sports = context.Categories.Local.Single(x => x.Name == "Sporting Goods");

                context.Auctions.Add(new Auction
                {
                    Categories = new Collection<Category> { sports },
                    Title = "Burton Mayhem snow board",
                    Description = "Burton Mayhem snow board: 159cm wide",
                    Images = new WebsiteImage[] { "~/Content/images/products/burtonMayhem.jpg" },
                });



                var collectibles = context.Categories.Local.Single(x => x.Name == "Collectibles");

                context.Auctions.Add(new Auction
                {
                    Categories = new Collection<Category> { collectibles },
                    Title = "Lock of John Lennon's hair",
                    Description = "Lock of John Lennon's hair",
                    Images = new WebsiteImage[] { "~/Content/images/products/lockOfHair.jpg" },
                });



                int featured = 0;
                var users = context.Users.Local.ToArray();

                foreach (var auction in context.Auctions.Local)
                {
                    auction.StartTime = DateTime.UtcNow
                        .AddDays(rand.Next(-10, -1))
                        .AddHours(rand.Next(1, 24))
                        .AddHours(rand.Next(1, 60));

                    auction.EndTime = auction.StartTime.AddDays(rand.Next(3, 14));
                    auction.Owner = users[rand.Next(users.Length)];
                    auction.CurrentPrice = "$" + rand.Next(1, 100);

                    if (featured++ < 3)
                        auction.FeatureAuction();
                }
            }
        }
    }
}
