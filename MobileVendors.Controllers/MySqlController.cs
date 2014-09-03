namespace MobileVendors.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MobileVendors.Data;
    using MySqlDataAccessModel;

    public class MySqlController
    {
        public MySqlController()
        {
        }

        public void UploadReports(IMobileVendorsData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "Cannot be null");
            }

            var reports = this.CreateReports(data);
            this.SaveReports(reports);
        }

        public IEnumerable<Report> GetReports()
        {
            List<Report> reports;
            var mySqlContext = new ReportsModel();
            
            reports = mySqlContext.Reports.ToList();
            
            return reports;
        }

        private IEnumerable<Report> CreateReports(IMobileVendorsData data)
        {
            List<Report> reports = new List<Report>();

            var reportsByProductName = data.Subscriptions
                                           .All()
                                           .Select(s => new
                                           {
                                               Id = s.ServiceId,
                                               ProductName = s.Service.ServiceName,
                                               VendorName = s.Store.Vendor.VendorName,
                                               TotalIncome = s.TotalIncome,
                                               Quantity = s.Quantity
                                               //TO DO Include Expenses...
                                           })
                                           .GroupBy(s => s.ProductName)
                                           .ToList();

            foreach (var group in reportsByProductName)
            {
                Report report = new Report();
                var firstFromGroup = group.First();
                report.ProductID = firstFromGroup.Id;
                report.ProductName = firstFromGroup.ProductName;
                var groupsByVendor = group.GroupBy(v => v.VendorName).ToList();

                foreach (var vendor in groupsByVendor)
                {
                    report.VendorName = vendor.Key;

                    int totalQuantity = 0;
                    decimal totalIncome = 0;
                    var vendorSub = group.Where(s => s.VendorName == vendor.Key);

                    foreach (var item in vendorSub)
                    {
                        totalQuantity += item.Quantity;
                        totalIncome += item.TotalIncome;
                    }

                    report.TotalQuantity = totalQuantity;
                    report.TotalIncomes = (long)totalIncome * totalQuantity;
                    
                    // TODO: Include expenses.
                    // report.Expenses = ???
                    reports.Add(report);
                }
            }

            return reports;
        }

        private void SaveReports(IEnumerable<Report> reports)
        {
            var mySqlContext = new ReportsModel();
            
            mySqlContext.Add(reports);
            mySqlContext.SaveChanges();
        }
    }
}