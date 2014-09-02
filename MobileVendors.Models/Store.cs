using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.Models
{
    public class Store
    {
        private ICollection<Subscription> subscriptions;

        public Store()
        {
            this.subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }

        public string Address { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public virtual ICollection<Subscription> Subscriptions
        {
            get
            {
                return this.subscriptions;
            }
            set
            {
                this.subscriptions = value;
            }
        }
    }
}