namespace MobileVendors.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using MobileVendors.Models;

    public interface IMobileVendorsDbContext
    {
        IDbSet<Service> Services { get; set; }

        IDbSet<Vendor> Vendors { get; set; }

        IDbSet<Subscription> Subscriptions { get; set; }

        IDbSet<Store> Stores { get; set; }

        IDbSet<Town> Towns { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}