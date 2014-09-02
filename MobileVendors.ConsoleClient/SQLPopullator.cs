namespace MobileVendors.ConsoleClient
{
    using System;
    using MobileVendors.Data;
    using MobileVendors.Models;

    public class SQLPopullator
    {
        private readonly IMobileVendorsData context;

        public SQLPopullator() : this(new MobileVendorsData())
        {
        }

        public SQLPopullator(IMobileVendorsData context)
        {
            this.context = context;
        }

        public void PopullateVendor(string vendorName, string phoneNumber= null, string email=null)
        {
            this.context.Vendors.Add(new Vendor() { VendorName = vendorName, PhoneNumber = phoneNumber, Email = email });
            this.context.SaveChanges();
        }

        public void PopullateStore(string address, int vendorId, int townId)
        {
            this.context.Stores.Add(new Store() { Address = address, VendorId = vendorId, TownId = townId });
        }

        public void PopullateService(string serviceName, decimal price, int categoryId)
        {
            this.context.Services.Add(new Service() { ServiceName = serviceName, Price = price, CategoryId = categoryId });
        }

        public void PopullateTown(string townName)
        {
            this.context.Towns.Add(new Town { TownName = townName });
        }
        
        public void PopullateCategory(string description)
        {
            this.context.Categories.Add(new Category() { Description = description });
        }

        public void PopullateSubsrciption(int quantity, DateTime subscribeDate, int periodInYears, decimal totalIncome, int serviceId, int storeId)
        {
            this.context.Subscriptions.Add(new Subscription() { Quantity = quantity, SubscribeDate = subscribeDate, PeriodInYears = periodInYears, TotalIncome = totalIncome, ServiceId = serviceId, StoreId = storeId });
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}