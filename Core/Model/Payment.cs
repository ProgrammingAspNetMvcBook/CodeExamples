using System;

namespace Ebuy
{
    public class Payment
    {
        public DateTime Timestamp { get; set; }

        public virtual Currency Amount { get; set; }
        public virtual Auction Auction { get; set; }
    }
}