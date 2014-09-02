namespace MobileVendors.Data
{
    using System;
    using System.Linq;

    using MobileVendors.Data.Repositories;
    using MobileVendors.Models;

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
                return new GenericRepository<ServiceTax>(this.context);
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}