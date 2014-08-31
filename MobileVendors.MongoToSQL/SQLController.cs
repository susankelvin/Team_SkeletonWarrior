using MobileVendors.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileVendors.MongoToSQL.Models;
using MobileVendors.Models;

namespace MobileVendors.MongoToSQL
{
    class SQLController
    {
        IMobileVendorsDbContext context;
        public SQLController()
        {
            init();
        }

        public void init()
        {
            context = new MobileVendorsDbContext();
        }

        public void PopulateVendors(List<MongoDBVendor> vendors)
        {
            foreach (var vendor in vendors)
            {
                Vendor vendorSQL = new Vendor()
                {
                    VendorName = vendor.VendorName,
                    PhoneNumber = vendor.PhoneNumber,
                    Email = vendor.Email
                };
                this.context.Vendors.Add(vendorSQL);
                this.context.SaveChanges();
            }  
        }

        public void PopulateTowns(List<MongoDBTown> towns)
        {
            foreach (var town in towns)
            {
                Town townSQL = new Town()
                {
                    TownName = town.TownName
                };

                this.context.Towns.Add(townSQL);
                this.context.SaveChanges();
            }
        }

        public void PopulateCategories(List<MongoDBCategory> categories)
        {
            foreach (var category in categories)
            {
                Category categorySQL = new Category()
                {
                    Description = category.Description
                };

                this.context.Categories.Add(categorySQL);
                this.context.SaveChanges();
            }
        }

        public void PopulateStore(List<MongoDBStore> stores)
        {
            foreach (var store in stores)
            {
                var currentStoreTownId = findTownIdByStore(store);
                var currentStoreVendorId = findVendorIdByStore(store);

                Store storeSQL = new Store()
                {
                    Address = store.Address,
                    TownId = currentStoreTownId,
                    VendorId = currentStoreVendorId
                };
                this.context.Stores.Add(storeSQL);
                this.context.SaveChanges();
            }
        }

        public void PopulateService(List<MongoDBService> services)
        {
            foreach (var service in services)
            {
                var currentServiceCategoryId = findCategoryIdByService(service);

                Service serviceSQl = new Service()
                {
                    ServiceName = service.ServiceName,
                    Price = service.Price,
                    CategoryId = currentServiceCategoryId
                };

                this.context.Services.Add(serviceSQl);
                this.context.SaveChanges();
            }
        }

        private int findTownIdByStore(MongoDBStore store)
        {
            var towns = from t in this.context.Towns
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

        public int findVendorIdByStore(MongoDBStore store)
        {
            var vendors = from v in this.context.Vendors
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

        public int findCategoryIdByService(MongoDBService service)
        {
            var categories = from c in this.context.Categories
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


        public void PopulateSubsrciption(int quantity, DateTime subscribeDate, int periodInYears, decimal totalIncome, int serviceId, int storeId)
        {
            this.context.Subscriptions.Add(new Subscription() { Quantity = quantity, SubscribeDate = subscribeDate, PeriodInYears = periodInYears, TotalIncome = totalIncome, ServiceId = serviceId, StoreId = storeId });
        }

    }
}
