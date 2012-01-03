using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Management;
using CustomExtensions.DataAnnotations;

namespace Ebuy.DataAccess
{
    public partial class DataContext
    {
        public class Initializer : IDatabaseInitializer<DataContext>
        {
            private readonly IDatabaseInitializer<DataContext> _initializer;

            public Initializer(IDatabaseInitializer<DataContext> initializer)
            {
                _initializer = initializer;
            }

            public void InitializeDatabase(DataContext context)
            {
                _initializer.InitializeDatabase(context);

                Seed(context);
                context.SaveChanges();
            }

            protected virtual void Seed(DataContext context)
            {
                var conn = new SqlConnectionStringBuilder(context.Database.Connection.ConnectionString);
                SqlServices.Install(conn.InitialCatalog, SqlFeatures.Membership | SqlFeatures.RoleManager, conn.ConnectionString);

                // Apply the custom UniqueAttribute to set unique constraints
                // on columns with the attribute defined
                new UniqueConstraintApplier().ApplyUniqueConstraints(context);

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
