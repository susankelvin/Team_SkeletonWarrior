namespace MobileVendors.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MobileVendors.Models.MongoDBModels;

    public class MongoToSqlController
    {
        private readonly MongoDBController mongoController;

        private readonly SQLController sqlController;

        public MongoToSqlController()
        {
            this.mongoController = new MongoDBController();
            this.sqlController = new SQLController();
        }

        public void ExportData()
        {
            this.ExportVendors();
            this.ExportTowns();
            this.ExportCategories();
            this.ExportStore();
            this.ExportServices();
        }

        private void ExportVendors()
        {
            ICollection<MongoDBVendor> vendorsFromMongo = this.mongoController.GetDistinctVendors();
            this.sqlController.PopulateVendors(vendorsFromMongo);
        }

        private void ExportTowns()
        {
            ICollection<MongoDBTown> townsFromMongo = this.mongoController.GetDistinctTowns();
            this.sqlController.PopulateTowns(townsFromMongo);
        }

        private void ExportCategories()
        {
            ICollection<MongoDBCategory> categoriesFromMongo = this.mongoController.GetDistinctCategories();
            this.sqlController.PopulateCategories(categoriesFromMongo);
        }

        private void ExportStore()
        {
            ICollection<MongoDBStore> storesFromMongo = this.mongoController.GetStores();
            this.sqlController.PopulateStores(storesFromMongo);
        }

        private void ExportServices()
        {
            ICollection<MongoDBService> servicesFromMongo = this.mongoController.GetServices();
            this.sqlController.PopulateServices(servicesFromMongo);
        }
    }
}