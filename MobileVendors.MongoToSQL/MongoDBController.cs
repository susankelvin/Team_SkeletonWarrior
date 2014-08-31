using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileVendors.MongoToSQL.Models;

namespace MobileVendors.MongoToSQL
{
    public class MongoDBController
    {
        MongoDatabase database;
        public MongoDBController()
        {
            init();
           // 
        }

        public void init()
        {
            //MongoDB
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();

            database = server.GetDatabase("MobileVendors");
        }

        public List<MongoDBVendor> getDistinctVendors()
        {
            var collection = database.GetCollection<MongoDBStore>("Stores");
            List<MongoDBVendor> distinctVendors = new List<MongoDBVendor>();

            var stores = 
                (from s in collection.AsQueryable<MongoDBStore>()
                 select s);

            foreach (var item in stores)
            {
                MongoDBVendor vendor = new MongoDBVendor();
                vendor.VendorName = item.Vendor.VendorName;
                vendor.PhoneNumber = item.Vendor.PhoneNumber;
                vendor.Email = item.Vendor.Email;

                Boolean isVendorDistinct = !distinctVendors.Any(x => x.VendorName.Equals(vendor.VendorName));
                if (isVendorDistinct)
                {
                    distinctVendors.Add(vendor);
                }
            }

            return distinctVendors;
        }

        public List<MongoDBTown> getDistinctTowns()
        {
            var collection = database.GetCollection<MongoDBStore>("Stores");
            List<MongoDBTown> distinctTowns = new List<MongoDBTown>();

            var stores =
                (from s in collection.AsQueryable<MongoDBStore>()
                 select s);

            foreach (var item in stores)
            {
                MongoDBTown town = new MongoDBTown();
                town.TownName = item.Town.TownName;

                Boolean isTownDistinct = !distinctTowns.Any(x => x.TownName.Equals(town.TownName));
                if (isTownDistinct)
                {
                    distinctTowns.Add(town);
                }
            }

            return distinctTowns;
        }

        public List<MongoDBCategory> getDistinctCategories()
        {
            var collection = database.GetCollection<MongoDBService>("Services");
            List<MongoDBCategory> distinctCategories = new List<MongoDBCategory>();

            var services =
                (from s in collection.AsQueryable<MongoDBService>()
                 select s);

            foreach (var item in services)
            {
                MongoDBCategory category = new MongoDBCategory();
                category.Description = item.Category.Description;
                //todo
                Boolean isTownDistinct = !distinctCategories.Any(x => x.Description.Equals(category.Description));
                if (isTownDistinct)
                {
                    distinctCategories.Add(category);
                }
            }

            return distinctCategories;
        }

        public List<MongoDBStore> getStores()
        {
            var collection = database.GetCollection<MongoDBStore>("Stores");

            var stores =
                (from s in collection.AsQueryable<MongoDBStore>()
                 select s);

            return stores.ToList<MongoDBStore>();
        }

        public List<MongoDBService> getServices()
        {
            var collection = database.GetCollection<MongoDBService>("Services");

            var services =
                (from s in collection.AsQueryable<MongoDBService>()
                 select s);

            return services.ToList<MongoDBService>();
        }
    }
}
