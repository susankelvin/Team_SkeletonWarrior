namespace MobileVendors.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vendor
    {
        private ICollection<Store> stores;

        public Vendor()
        {
            this.stores = new HashSet<Store>();
        }

        public int Id { get; set; }

        public string VendorName { get; set; }
        
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public decimal? Expenses { get; set; }

        public virtual ICollection<Store> Stores
        {
            get
            {
                return this.stores;
            }
            set
            {
                this.stores = value;
            }
        }
    }
}