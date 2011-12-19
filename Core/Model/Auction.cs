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

        public virtual Bid WinningBid { get; set; }

        public bool IsCompleted
        {
            get { return EndTime <= DateTime.Now; }
        }

        [IsNotEmpty]
        public virtual ICollection<Category> Categories { get; set; }

        [InverseProperty("Auction")]
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
//                WinningBid = bid;
            }
            
            Bids.Add(bid);
        }


        public class Metadata
        {
            [Required, StringLength(500)]
            public object Title { get; set; }

            [Required]
            public object Description { get; set; }

            [Required]
            public object StartingPrice { get; set; }

            [Required]
            public object StartTime { get; set; }

            [Required]
            public object EndTime { get; set; }

            [Required]
            public object Product { get; set; }

            [Required]
            public object Owner { get; set; }
        }
    }
}