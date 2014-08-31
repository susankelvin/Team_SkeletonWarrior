using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileVendors.MongoToSQL.Models;

namespace MobileVendors.MongoToSQL
{
    public class MongoToSqlExporter
    {
        MongoDBController mongoController;
        SQLController sqlController;

        public MongoToSqlExporter()
        {
            mongoController = new MongoDBController();
            sqlController = new SQLController();
        }

        public void ExportVendors()
        {
            List<MongoDBVendor> vendorsFromMongo = mongoController.getDistinctVendors();
            sqlController.PopulateVendors(vendorsFromMongo);
        }

        public void ExportTowns()
        {
            List<MongoDBTown> townsFromMongo = mongoController.getDistinctTowns();
            sqlController.PopulateTowns(townsFromMongo);
        }

        public void ExportCategories()
        {
            List<MongoDBCategory> categoriesFromMongo = mongoController.getDistinctCategories();
            sqlController.PopulateCategories(categoriesFromMongo);
        }

        public void ExportStore()
        {
            List<MongoDBStore> storesFromMongo = mongoController.getStores();
            sqlController.PopulateStore(storesFromMongo);
        }

        public void ExportServices()
        {
            List<MongoDBService> servicesFromMongo = mongoController.getServices();
            sqlController.PopulateService(servicesFromMongo);
        }
    }
}
