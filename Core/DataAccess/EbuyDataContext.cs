using System.Collections.Generic;
using System.Data.Entity;

namespace Ebuy.DataAccess
{
    public class EbuyDataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        public class Initializer : DropCreateDatabaseAlways<EbuyDataContext> //DropCreateDatabaseIfModelChanges<EbuyDataContext>
        {
            private const string UniqueConstraintQuery = "ALTER TABLE [{0}] ADD CONSTRAINT [{0}_{1}_unique] UNIQUE ([{1}])";

            protected override void Seed(EbuyDataContext context)
            {
                var uniqueColumns = new[] {
                        new { Table = "Users", Columns = new[] { "EmailAddress" } }
                    };

                foreach (var uniqueColumn in uniqueColumns)
                {
                    ApplyUniqueConstraint(context, uniqueColumn.Table, uniqueColumn.Columns);
                }

                base.Seed(context);
            }

            private static void ApplyUniqueConstraint(EbuyDataContext context, string tableName, IEnumerable<string> columns)
            {
                foreach (var column in columns)
                {
                    string query = string.Format(UniqueConstraintQuery, tableName, column);
                    context.Database.ExecuteSqlCommand(query);
                }
            }
        }
    }
}
