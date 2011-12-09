using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Auction.Metadata))]
    public class Auction : Entity
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public Bid WinningBid { get; private set; }

        public bool IsCompleted
        {
            get { return EndTime <= DateTime.Now; }
        }

        public virtual IEnumerable<Bid> Bids { get; private set; }
        public virtual Product Product { get; private set; }
        public virtual User User { get; private set; }


        public class Metadata : EntityMetadata
        {
            [Required]
            public object StartTime { get; set; }

            [Required]
            public object EndTime { get; set; }
        }
    }
}