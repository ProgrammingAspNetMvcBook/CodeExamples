using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ebuy.Website.Areas.Admin.Models
{
    public class UserViewModel
    {
        public virtual ICollection<Auction> Selling { get; private set; }

        //        public virtual ICollection<Bid> Bids { get; private set; }

        public virtual string DisplayName
        {
            get { return _displayName ?? FullName; }
            set { _displayName = value; }
        }
        private string _displayName;

        public string EmailAddress { get; set; }

        public virtual string FullName { get; set; }

        public virtual ICollection<Payment> Payments { get; private set; }

        public virtual ICollection<Review> Reviews { get; private set; }

        public virtual ICollection<Auction> WatchedAuctions { get; private set; }
    }
}