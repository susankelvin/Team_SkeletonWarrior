namespace MobileVendors.ConsoleClient
{
    using System;

    using MobileVendors.Controllers;
    using MobileVendors.Data;

    internal class ConsoleClient
    {
        private static void Main()
        {
            //MongoToSqlExport();
            CreateJsonReports();
            CreateExcelReports();
        }

        private static void MongoToSqlExport()
        {
            MongoToSqlController exporter = new MongoToSqlController();
            exporter.ExportData();
        }

        private static void CreateJsonReports()
        {
            var data = new MobileVendorsData();
            JsonReportController jrc = new JsonReportController(data);
            jrc.CreateReport();
        }

        private static void CreateExcelReports()
        {
            var excelController = new ExcelController("financial-report.xlsx");
            excelController.ExportData();
        }
    }
}