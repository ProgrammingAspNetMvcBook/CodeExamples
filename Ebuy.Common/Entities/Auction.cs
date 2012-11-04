using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Ebuy
{
    [MetadataType(typeof(Auction.Metadata))]
    public class Auction : Entity<Guid>
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual Currency StartPrice { get; set; }
        public virtual Currency CurrentPrice { get; set; }

        public Guid? WinningBidId { get; set; }
        public virtual Bid WinningBid { get; private set; }

        public bool IsCompleted
        {
            get { return EndTime <= DateTime.Now; }
        }

        public virtual bool IsFeaturedAuction { get; private set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<WebsiteImage> Images { get; set; }

        public long OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public virtual CurrencyCode CurrencyCode
        {
            get
            {
                return (CurrentPrice != null) ? CurrentPrice.Code : null;
            }
        }

        public Auction()
        {
            Bids = new Collection<Bid>();
            Categories = new Collection<Category>();
            Images = new Collection<WebsiteImage>();
        }

        public void FeatureAuction()
        {
            IsFeaturedAuction = true;
        }

        public Bid PostBid(User user, double bidAmount)
        {
            return PostBid(user, new Currency(CurrencyCode, bidAmount));
        }

        public Bid PostBid(User user, Currency bidAmount)
        {
            Contract.Requires(user != null);

            if (bidAmount.Code != CurrencyCode)
                throw new InvalidBidException(bidAmount, WinningBid);

            if (bidAmount.Value <= CurrentPrice.Value)
                throw new InvalidBidException(bidAmount, WinningBid);

            var bid = new Bid(user, this, bidAmount);

            CurrentPrice = bidAmount;
            WinningBidId = bid.Id;
            
            Bids.Add(bid);

            return bid;
        }


        public class Metadata
        {
            [InverseProperty("Auction")]
            public object Bids;

            public object Categories;

            [Required]
            public object CurrentPrice;

            [Required]
            public object Description;

            [Required]
            public object EndTime;

            [InverseProperty("Selling")]
            public object Owner;

            [Required]
            [ForeignKey("Owner")]
            public object OwnerId;

            [Required]
            public object StartTime;

            [Required, StringLength(500)]
            public object Title;

            [ForeignKey("WinningBid")]
            public object WinningBidId;
        }
    }

    public class InvalidBidException : Exception
    {
        public Currency BidAmount { get; set; }
        public Bid WinningBid { get; set; }

        public InvalidBidException(Currency bidAmount, Bid winningBid = null)
        {
            BidAmount = bidAmount;
            WinningBid = winningBid;
        }
    }


    public static class AuctionExtensions
    {

        public static IEnumerable<Auction> Active(this IEnumerable<Auction> auctions)
        {
            return auctions.Where(x => x.IsCompleted == false);
        }

        public static IEnumerable<Auction> Featured(this IEnumerable<Auction> auctions)
        {
            return auctions.Where(x => x.IsFeaturedAuction);
        }

    }

}