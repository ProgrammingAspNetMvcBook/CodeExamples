using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using CustomExtensions.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Auction.Metadata))]
    public class Auction : Entity
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual Currency StartingPrice { get; set; }

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


        public Auction()
        {
            Bids = new Collection<Bid>();
            Categories = new Collection<Category>();
            Images = new Collection<WebsiteImage>();
        }


        public void PostBid(User user, Currency bidAmount)
        {
            PostBid(user, bidAmount, DateTime.UtcNow);
        }

        protected internal void PostBid(User user, Currency bidAmount, DateTime timestamp)
        {
            Contract.Requires(user != null);
            Contract.Requires(bidAmount != null);

            var bid = new Bid(user, this, bidAmount, timestamp);

            Bids.Add(bid);

            // TODO: Support multiple currencies
            if (WinningBid == null || bidAmount.Amount > WinningBid.Price.Amount)
            {
                WinningBid = bid;
            }
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