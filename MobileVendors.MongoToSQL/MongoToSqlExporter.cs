namespace MobileVendors.MongoToSQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MobileVendors.MongoToSQL.Models;
    
    public class MongoToSqlExporter
    {
        private readonly MongoDBController mongoController;

        private readonly SQLController sqlController;

        public MongoToSqlExporter()
        {
            this.mongoController = new MongoDBController();
            this.sqlController = new SQLController();
        }

        public void ExportVendors()
        {
            ICollection<MongoDBVendor> vendorsFromMongo = this.mongoController.GetDistinctVendors();
            this.sqlController.PopullateVendors(vendorsFromMongo);
        }

        public void ExportTowns()
        {
            ICollection<MongoDBTown> townsFromMongo = this.mongoController.GetDistinctTowns();
            this.sqlController.PopullateTowns(townsFromMongo);
        }

        public void ExportCategories()
        {
            ICollection<MongoDBCategory> categoriesFromMongo = this.mongoController.GetDistinctCategories();
            this.sqlController.PopullateCategories(categoriesFromMongo);
        }

        public void ExportStore()
        {
            ICollection<MongoDBStore> storesFromMongo = this.mongoController.GetStores();
            this.sqlController.PopullateStores(storesFromMongo);
        }

        public void ExportServices()
        {
            ICollection<MongoDBService> servicesFromMongo = this.mongoController.GetServices();
            this.sqlController.PopullateServices(servicesFromMongo);
        }
    }
}