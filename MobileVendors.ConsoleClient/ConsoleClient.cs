namespace MobileVendors.ConsoleClient
{
    using System;
    using MobileVendors.Controllers;
    using MobileVendors.Data;
    using MySqlDataAccessModel;

    internal class ConsoleClient
    {
        private static void Main()
        {
            MongoToSqlExport();
            //ExcelReportsImportToSql();
            CreateJsonReports();
            //CreateExcelReports();
            //CreateXmlReports();
            //CreateMySqlReports();
        }

        private static void MongoToSqlExport()
        {
            MongoToSqlController exporter = new MongoToSqlController();
            exporter.ExportData();
        }

        private static void ExcelReportsImportToSql()
        {
            string zipPath = @"..\..\SampleReports.zip";
            string sheetName = "Sheet1$";
            ExcelImportController excelImport = new ExcelImportController();
            excelImport.GetReports(zipPath, sheetName);
        }

        private static void CreateJsonReports()
        {
            var data = new MobileVendorsData();
            JsonReportController jrc = new JsonReportController(data);
            jrc.CreateReport();
        }

        private static void CreateExcelReports()
        {
            var excelController = new ExcelExportController("financial-report.xlsx");
            excelController.ExportData();
        }

        private static void CreateXmlReports()
        {
            XmlController xmlController = new XmlController();
            xmlController.ExportXmlReport();
        }

        private static void CreateMySqlReports()
        {
            var data = new MobileVendorsData();
            var mysql = new MySqlController();
            mysql.UploadReports(data);
        }
    }
}