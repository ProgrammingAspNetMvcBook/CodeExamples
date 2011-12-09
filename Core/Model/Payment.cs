using System;

namespace Ebuy
{
    public class Payment
    {
        public DateTime Timestamp { get; private set; }

        public virtual Currency Amount { get; private set; }
        public virtual Auction Auction { get; private set; }
    }
}