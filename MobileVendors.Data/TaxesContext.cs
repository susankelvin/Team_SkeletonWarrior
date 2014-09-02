namespace MobileVendors.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using MobileVendors.Models;

    public class TaxesContext : DbContext
    {
        public TaxesContext() : base("TaxesConnection")
        {
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<TaxesContext>(null);
        }

        public IDbSet<ServiceTax> Taxes { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}