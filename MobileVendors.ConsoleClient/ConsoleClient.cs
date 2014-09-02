namespace MobileVendors.ConsoleClient
{
    using System;
    using System.Collections.Generic;

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

        private static List<Report> ExcelReportsImport()
        {
            string zipPath = @"..\..\..\SampleReports.zip";
            string sheetName = "Sheet1$";
            ExcelImportController excelImport = new ExcelImportController();
            List<Report> reports = excelImport.GetReports(zipPath, sheetName);
            return reports;
        }
    }
}