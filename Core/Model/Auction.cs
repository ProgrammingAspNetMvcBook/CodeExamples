using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ebuy
{
    [MetadataType(typeof(Auction.Metadata))]
    public class Auction : Entity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsCompleted
        {
            get { return EndTime <= DateTime.Now; }
        }

        public virtual IEnumerable<Bid> Bids { get; set; }
        public virtual Product Product { get; set; }
        public virtual User Owner { get; set; }


        public class Metadata
        {
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