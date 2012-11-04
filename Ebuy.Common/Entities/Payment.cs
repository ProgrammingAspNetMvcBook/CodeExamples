using System;

namespace Ebuy
{
    public class Payment : Entity<Guid>
    {
        public Currency Amount { get; private set; }

        public Auction Auction { get; private set; }

        public DateTime Timestamp { get; private set; }

        public User User { get; set; }


        public Payment(User user, Auction auction, Currency amount)
        {
            User = user;
            Auction = auction;
            Amount = amount;
            Timestamp = DateTime.Now;
        }

        private Payment()
        {
        }
    }
}