namespace MobileVendors.Data
{
    using MobileVendors.Data.Repositories;
    using MobileVendors.Models;

    public interface IMobileVendorsData
    {
        IGenericRepository<Service> Services { get; }
    }
}