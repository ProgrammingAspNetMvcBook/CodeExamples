using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Auction.Metadata))]
    public class Auction : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Currency StartingPrice { get; set; }

        public virtual Bid WinningBid { get; private set; }

        public bool IsCompleted
        {
            get { return EndTime <= DateTime.Now; }
        }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public virtual ICollection<WebsiteImage> Images { get; set; }

        public virtual Product Product { get; set; }

        public virtual User Owner { get; set; }


        internal void PostBid(Bid bid)
        {
            Contract.Requires(bid != null);

            // TODO: Support multiple currencies
            if (WinningBid == null || bid.Price.Amount > WinningBid.Price.Amount)
            {
                WinningBid = bid;
            }
            
            Bids.Add(bid);
        }


        public class Metadata
        {
            [InverseProperty("Auction")]
            public object Bids;

            [IsNotEmpty]
            public object Categories;

            [Required, StringLength(500)]
            public object Title;

            [Required]
            public object Description;

            [Required]
            public object StartingPrice;

            [Required]
            public object StartTime;

            [Required]
            public object EndTime;

            [Required]
            public object Owner;
        }
    }
}