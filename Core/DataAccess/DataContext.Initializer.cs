using System.Data.Entity;
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


                context.Users.Add(new User()
                {
                    DisplayName = "Uber Administrator",
                    EmailAddress = "admin@ebuy.com",
                    FullName = "Administrator",
                });


                context.Categories.Add(new Category("Collectibles"));

                context.Categories.Add(
                    new Category("Electronics") {
                        SubCategories = new[] {
                            new Category("Cameras & Photography"),
                            new Category("Computers & Networking"),
                            new Category("TV, Audio, and Video"),
                            new Category("Video Games & Systems") {
                                    SubCategories = new [] {
                                            new Category("Video Game Systems")
                                        }
                                },
                        }
                    });

                context.Categories.Add(
                    new Category("Home & Outdoors")
                    {
                        SubCategories = new[] {
                            new Category("Home & Garden"),
                            new Category("Sporting Goods"),
                            new Category("Toys & Hobbies"),
                        }
                    });
            }
        }
    }
}
