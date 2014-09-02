namespace MobileVendors.Controllers
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Data.OleDb;
    using System.Collections.Generic;

    public class Report
    {
        public string Store { get; set; }
        public DateTime Date { get; set; }
        public int ServiceID { get; set; }
        public int Quantity { get; set; }
        public int PeriodInYears { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal Total { get; set; }

        public Report(string store, DateTime date, int serviceID, int quantity, int periodInYears, decimal monthlyFee, decimal total)
        {
            this.Store = store;
            this.Date = date;
            this.ServiceID = serviceID;
            this.Quantity = quantity;
            this.PeriodInYears = periodInYears;
            this.MonthlyFee = monthlyFee;
            this.Total = total;
        }
    }

    public class ExcelImportController
    {
        private static string extractPath;
        private static List<Report> reports = new List<Report>();

        private void GetExcelReports(string path, string sheetName, string extractPath)
        {
            string[] currentDirFiles = Directory.GetFiles(path);

            foreach (var file in currentDirFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                string fileName = fileInfo.Name;
                DirectoryInfo dirInfo = fileInfo.Directory;
                string dirName = dirInfo.Name;
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data Source=" + extractPath + @"\" + dirName + @"\" + fileName +
                @";Extended Properties=""Excel 12.0 XML;HDR=Yes""";
                OleDbConnection dbConn = new OleDbConnection(connectionString);
                dbConn.Open();

                using (dbConn)
                {
                    OleDbCommand cmdReadVendorName = new OleDbCommand("SELECT * FROM [" + sheetName + "]", dbConn);
                    OleDbDataReader reader = cmdReadVendorName.ExecuteReader();
                    while (reader.Read())
                    {
                        int serviceId;
                        bool isId = Int32.TryParse(reader["ServiceID"].ToString(), out serviceId);

                        if (isId)
                        {
                            int quantity = Int32.Parse(reader["Quantity"].ToString());
                            int periodInYears = Int32.Parse(reader["Period (years)"].ToString());
                            decimal monthlyFee = Decimal.Parse(reader["Monthly Fee"].ToString());
                            decimal total = Decimal.Parse(reader["Total"].ToString());
                            DateTime date = DateTime.Parse(dirName);
                            int length = fileName.Length - 4;
                            string storeName = fileName.Substring(0, length);

                            Report report = new Report(storeName, date, serviceId, (int)quantity, periodInYears, monthlyFee, total);
                            reports.Add(report);
                        }
                    }
                }
            }

            string[] currentDirDirectories = Directory.GetDirectories(path);
            foreach (var directory in currentDirDirectories)
            {
                GetExcelReports(directory, sheetName, extractPath);
            }
        }

        public List<Report> GetReports(string zipPath, string sheetName)
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            extractPath = tempDirectory;
            ZipFile.ExtractToDirectory(zipPath, tempDirectory);
            GetExcelReports(tempDirectory, sheetName, extractPath);
            return reports;
        }
    }
}
