using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.Models
{
    public class Town
    {
        private ICollection<Store> stores;

        public Town()
        {
            this.stores = new HashSet<Store>();
        }

        public int Id { get; set; }

        public string TownName { get; set; }

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