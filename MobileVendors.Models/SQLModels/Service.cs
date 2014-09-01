namespace MobileVendors.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Service
    {
        private ICollection<Subscription> subscriptions;

        public Service()
        {
            this.subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }        

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

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