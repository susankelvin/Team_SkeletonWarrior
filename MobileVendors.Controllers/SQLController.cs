namespace MobileVendors.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MobileVendors.Data;
    using MobileVendors.Models;
    using MobileVendors.Models.MongoDBModels;

    internal class SQLController
    {
        private readonly IMobileVendorsData data;

        public SQLController()
        {
            this.data = new MobileVendorsData();
        }

        public void PopulateVendors(ICollection<MongoDBVendor> vendors)
        {
            var xmlController = new XmlController();

            var expensesPerVendor = xmlController.ImportXmlReport();

            foreach (var vendor in vendors)
            {
                var epxense = expensesPerVendor.FirstOrDefault(e => e.VendorName.Substring(0, 3) == vendor.VendorName.Substring(0, 3));
                decimal? expenses = null;
                if (epxense != null)
                {
                    var expense = epxense.ExpensesPerMonth;
                    expenses = 0;
                    foreach (var exp in expense)
                    {
                        expenses += Decimal.Parse(exp.Value);
                    }
                }

                Vendor vendorSQL = new Vendor()
                {
                    VendorName = vendor.VendorName,
                    PhoneNumber = vendor.PhoneNumber,
                    Email = vendor.Email,
                    Expenses = expenses
                };
                this.data.Vendors.Add(vendorSQL);
            }
            this.data.SaveChanges();
        }

        public void PopulateTowns(ICollection<MongoDBTown> towns)
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

        public void PopulateCategories(ICollection<MongoDBCategory> categories)
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

        public void PopulateStores(ICollection<MongoDBStore> stores)
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

        public void PopulateServices(ICollection<MongoDBService> services)
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

        public List<ServicesReport> GetTotalIncomeByDate()
        {
            var subsciptions = from sb in this.data.Subscriptions.All()
                               join s in this.data.Services.All() on sb.ServiceId equals s.Id
                               group sb by new { s.ServiceName, sb.SubscribeDate }
                               into sbs select new
                               {
                                   Date = sbs.Key.SubscribeDate,
                                   TotalSum = sbs.Sum(x => x.TotalIncome * x.Quantity * x.PeriodInYears),
                                   Name = sbs.Key.ServiceName
                               };

            List<ServicesReport> servicesReports = new List<ServicesReport>();

            foreach (var subsciption in subsciptions)
            {
                ServicesReport servicesReport = new ServicesReport();

                servicesReport.ServiceName = subsciption.Name;
                servicesReport.Date = subsciption.Date;
                servicesReport.TotalSum = subsciption.TotalSum;

                servicesReports.Add(servicesReport);
            }

            return servicesReports;
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