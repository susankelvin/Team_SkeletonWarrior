namespace MobileVendors.Data
{
    using System.Data.Entity;
    using MobileVendors.Data.Migrations;
    using MobileVendors.Models;

    public class MobileVendorsDbContext : DbContext, IMobileVendorsDbContext
    {
        public MobileVendorsDbContext() : base("MobileVendorsConnection")
            
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MobileVendorsDbContext, Configuration>());
        }

        public IDbSet<Service> Services { get; set; }

        public IDbSet<Vendor> Vendors { get; set; }

        public IDbSet<Subscription> Subscriptions { get; set; }

        public IDbSet<Store> Stores { get; set; }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Category> Categories { get; set; }
        
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}