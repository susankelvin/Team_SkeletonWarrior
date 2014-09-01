using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileVendors.Data.Repositories;
using MobileVendors.Models;

namespace MobileVendors.Data
{
    public class TaxesData
    {
        private readonly TaxesContext context;

        public TaxesData()
        {
            this.context = new TaxesContext();
        }

        public IGenericRepository<ServiceTax> Taxes
        {
            get
            {
                return new GenericRepository<ServiceTax>(context);
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
        
    }
}