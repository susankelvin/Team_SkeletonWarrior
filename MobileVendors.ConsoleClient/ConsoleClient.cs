namespace MobileVendors.ConsoleClient
{
    using System;
    using System.Linq;
    using MobileVendors.Data;
    using MobileVendors.MongoToSQL;

    internal class ConsoleClient
    {
        private static void Main()
        {
            //CreateJsonReports();

            MongoToSqlExport();
        }

        private static void CreateJsonReports()
        {
            var data = new MobileVendorsData();
            JsonReportCreator jrc = new JsonReportCreator(data);
            jrc.CreateReport();
        }

        private static void MongoToSqlExport()
        {
            MongoToSqlExporter exporter = new MongoToSqlExporter();

            exporter.ExportVendors();
            exporter.ExportTowns();
            exporter.ExportCategories();
            exporter.ExportStore();
            exporter.ExportServices();
        }
    }
}