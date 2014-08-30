namespace MobileVendors.Data
{
    using MobileVendors.Data.Repositories;
    using MobileVendors.Models;

    public interface IMobileVendorsData
    {
        IGenericRepository<Service> Services { get; }

        IGenericRepository<Vendor> Vendors { get; }

        IGenericRepository<Subscription> Subscriptions { get; }

        IGenericRepository<Store> Stores { get; }

        IGenericRepository<Category> Categories { get; }
        
        IGenericRepository<Town> Towns { get; }

        void SaveChanges();


    }
}