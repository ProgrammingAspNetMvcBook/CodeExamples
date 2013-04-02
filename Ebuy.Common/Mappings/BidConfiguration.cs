using System.Data.Entity.ModelConfiguration;

namespace Ebuy.Mappings
{
	public class BidConfiguration : EntityTypeConfiguration<Bid>
	{
		public BidConfiguration()
		{
			HasRequired(x => x.Auction)
				.WithMany()
				.WillCascadeOnDelete(false);
		}
	}
}