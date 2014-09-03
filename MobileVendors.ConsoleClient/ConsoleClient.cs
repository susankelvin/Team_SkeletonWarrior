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
            //ExcelReportsImportToSql();
            //CreateJsonReports();
            //CreateXmlReports();
            CreatePdfReports();
            //ImportMySqlReports();
            //CreateExcelReports();
        }

        private static void MongoToSqlExport()
        {
            MongoToSqlController exporter = new MongoToSqlController();
            exporter.TransferData();
        }

        private static void ExcelReportsImportToSql()
        {
            string zipPath = @"..\..\SampleReports.zip";
            string sheetName = "Sheet1$";
            ExcelImportController excelImport = new ExcelImportController();
            excelImport.ImportExcelReportsToSql(zipPath, sheetName);
        }

        private static void CreateJsonReports()
        {
            var data = new MobileVendorsData();
            JsonReportController jrc = new JsonReportController(data);
            jrc.CreateReports();
        }

        private static void CreatePdfReports()
        {
            var controller = new PdfReportController();
            controller.GeneratePdfExport(new DateTime(2014, 08, 29), new DateTime(2014, 09, 4), @"..\..\Sales.pdf");
        }

        private static void ImportMySqlReports()
        {
            var data = new MobileVendorsData();
            var mysql = new MySqlController();
            mysql.UploadReports(data);
        }

        private static void CreateExcelReports()
        {
            var excelController = new ExcelExportController("financial-report.xlsx");
            excelController.GenerateReport();
        }

        private static void CreateXmlReports()
        {
            XmlController xmlController = new XmlController();
            xmlController.ExportXmlReport();
        }
    }
}