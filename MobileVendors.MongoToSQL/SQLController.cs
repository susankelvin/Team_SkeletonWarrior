namespace MobileVendors.MongoToSQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MobileVendors.Data;
    using MobileVendors.Models;
    using MobileVendors.MongoToSQL.Models;

    internal class SQLController
    {
        private readonly IMobileVendorsData data;

        public SQLController()
        {
            this.data = new MobileVendorsData();
        }

        public void PopullateVendors(ICollection<MongoDBVendor> vendors)
        {
            foreach (var vendor in vendors)
            {
                Vendor vendorSQL = new Vendor()
                {
                    VendorName = vendor.VendorName,
                    PhoneNumber = vendor.PhoneNumber,
                    Email = vendor.Email
                };
                this.data.Vendors.Add(vendorSQL);
            }
            this.data.SaveChanges();
        }

        public void PopullateTowns(ICollection<MongoDBTown> towns)
        {
            foreach (var town in towns)
            {
                Town townSQL = new Town()
                {
                    TownName = town.TownName
                };

                this.data.Towns.Add(townSQL);
            }
            this.data.SaveChanges();
        }

        public void PopullateCategories(ICollection<MongoDBCategory> categories)
        {
            foreach (var category in categories)
            {
                Category categorySQL = new Category()
                {
                    Description = category.Description
                };

                this.data.Categories.Add(categorySQL);
            }
            this.data.SaveChanges();
        }

        public void PopullateStores(ICollection<MongoDBStore> stores)
        {
            foreach (var store in stores)
            {
                var currentStoreTownId = this.FidTownIdByStore(store);
                var currentStoreVendorId = this.FindVendorIdByStore(store);

                Store storeSQL = new Store()
                {
                    Address = store.Address,
                    TownId = currentStoreTownId,
                    VendorId = currentStoreVendorId
                };
                this.data.Stores.Add(storeSQL);
            }
            this.data.SaveChanges();
        }

        public void PopullateServices(ICollection<MongoDBService> services)
        {
            foreach (var service in services)
            {
                var currentServiceCategoryId = this.FindCategoryIdByService(service);

                Service serviceSQl = new Service()
                {
                    ServiceName = service.ServiceName,
                    Price = service.Price,
                    CategoryId = currentServiceCategoryId
                };

                this.data.Services.Add(serviceSQl);
            }
            this.data.SaveChanges();
        }

        public void PopullateSubsrciptions(int quantity, DateTime subscribeDate, int periodInYears, decimal totalIncome, int serviceId, int storeId)
        {
            this.data.Subscriptions.Add(new Subscription() { Quantity = quantity, SubscribeDate = subscribeDate, PeriodInYears = periodInYears, TotalIncome = totalIncome, ServiceId = serviceId, StoreId = storeId });
            this.data.SaveChanges();
        }

        private int FidTownIdByStore(MongoDBStore store)
        {
            var towns = from t in this.data.Towns.All()
                        select new
                        {
                            t.Id,
                            t.TownName
                        };
            foreach (var town in towns)
            {
                if (town.TownName.Equals(store.Town.TownName))
                {
                    return town.Id;
                }
            }
            throw new Exception("No such town in the database");
        }

        private int FindVendorIdByStore(MongoDBStore store)
        {
            var vendors = from v in this.data.Vendors.All()
                          select new
                          {
                              v.Id,
                              v.VendorName
                          };
            foreach (var vendor in vendors)
            {
                if (vendor.VendorName.Equals(store.Vendor.VendorName))
                {
                    return vendor.Id;
                }
            }
            throw new Exception("No such vendor in the database");
        }

        private int FindCategoryIdByService(MongoDBService service)
        {
            var categories = from c in this.data.Categories.All()
                             select new
                             {
                                 c.Id,
                                 c.Description
                             };
            foreach (var category in categories)
            {
                if (category.Description.Equals(service.Category.Description))
                {
                    return category.Id;
                }
            }
            throw new Exception("No such category in the database");
        }
    }
}