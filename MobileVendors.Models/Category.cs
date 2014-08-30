using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.Models
{
    public class Category
    {
        private ICollection<Service> products;

        public Category()
        {
            this.products = new HashSet<Service>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Service> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
            }
        }
    }
}