namespace MobileVendors.ConsoleClient
{
    using System;
    using System.Linq;
    using MobileVendors.Controllers;
    using MobileVendors.Data;

    internal class ConsoleClient
    {
        private static void Main()
        {
            //CreateJsonReports();
            //MongoToSqlExport();
            ExportFromSQLite();
        }

        private static void CreateJsonReports()
        {
            var data = new MobileVendorsData();
            JsonReportController jrc = new JsonReportController(data);
            jrc.CreateReport();
        }

        private static void MongoToSqlExport()
        {
            MongoToSqlController exporter = new MongoToSqlController();

            exporter.ExportVendors();
            exporter.ExportTowns();
            exporter.ExportCategories();
            exporter.ExportStore();
            exporter.ExportServices();
        }

        private static void ExportFromSQLite()
        {
            var sqliteController = new TaxesData();
            var taxes = sqliteController.Taxes.All();
            foreach (var tax in taxes)
            {
                Console.WriteLine("Service name: {0} | Service tax: {1}",
                    tax.ServiceName, tax.Tax);
            }
        }
    }
}