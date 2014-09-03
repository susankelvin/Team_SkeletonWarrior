namespace MobileVendors.Controllers
{
    using System;
    using System.Linq;
    using System.IO;
    using System.IO.Compression;
    using System.Data.OleDb;
    using MobileVendors.Models;
    using MobileVendors.Data;

    public class ExcelImportController
    {
        private readonly IMobileVendorsData data = new MobileVendorsData();

        private static string extractPath;

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
                            var day = dirName.Substring(0, 2);
                            var month = dirName.Substring(3, 2);
                            var dateAsString = string.Format("{0}.{1}.2014", month, day);
                           
                            DateTime date = date = DateTime.Parse(dateAsString);
                            var dash = fileName.IndexOf('-');
                            var vendor = fileName.Substring(0, dash);
                            var town = fileName.Substring(dash + 1, fileName.Length - vendor.Length - 5);
                            var storeId = this.data.Stores.All()
                                              .Where(s => s.Vendor.VendorName.Substring(0, 3) == vendor.Substring(0, 3) &&
                                                          s.Town.TownName == town).First().Id;
                            var report = new Subscription()
                            {
                                Quantity = quantity,
                                PeriodInYears = periodInYears,
                                TotalIncome = monthlyFee,
                                ServiceId = serviceId,
                                SubscribeDate = date,
                                StoreId = storeId
                            };
                            data.Subscriptions.Add(report);
                            data.SaveChanges();
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

        public void GetReports(string zipPath, string sheetName)
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            extractPath = tempDirectory;
            ZipFile.ExtractToDirectory(zipPath, tempDirectory);
            GetExcelReports(tempDirectory, sheetName, extractPath);
        }
    }
}