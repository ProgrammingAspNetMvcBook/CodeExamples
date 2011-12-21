using System;

namespace Ebuy
{
    public class Payment
    {
        public Guid Id
        {
            get
            {
                if (_id == null)
                    _id = Guid.NewGuid();

                return _id.Value;
            }
            private set { _id = value; }
        }
        private Guid? _id;

        public Currency Amount { get; private set; }

        public Auction Auction { get; private set; }

        public DateTime Timestamp { get; private set; }

        public User User { get; set; }


        public Payment(User user, Auction auction, Currency amount)
        {
            User = user;
            Auction = auction;
            Amount = amount;
            Timestamp = Clock.Now;
        }

        private Payment()
        {
        }
    }
}