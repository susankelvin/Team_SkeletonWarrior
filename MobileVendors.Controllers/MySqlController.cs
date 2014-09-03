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
            using (mySqlContext)
            {
                reports = mySqlContext.Reports.ToList();
            }

            return reports;
        }

        private IEnumerable<Report> CreateReports(IMobileVendorsData data)
        {
            List<Report> reports = new List<Report>();

            var reportsByProductName = data.Subscriptions.All().
                Select(a => new
                {
                    ProductID = a.Id,
                    ProductName = a.Service.ServiceName,
                    VendorName = a.Store.Address,
                    TotalIncomes = a.TotalIncome,
                    TotalQuantity = a.Quantity,

                    // TODO: Include expenses.
                }).
                GroupBy(a => a.ProductName);

            foreach (var group in reportsByProductName)
            {
                Report report = new Report();
                report.ProductName = group.Key;
                var firstFromGroup = group.First();
                report.ProductID = firstFromGroup.ProductID;
                report.ProductName = firstFromGroup.ProductName;
                report.VendorName = firstFromGroup.VendorName;

                report.TotalQuantity = group.Sum(a => a.TotalQuantity);
                decimal incomes = group.Sum(a => a.TotalIncomes);
                report.TotalIncomes = (long)incomes;

                // TODO: Include expenses.
                // report.Expenses = ???
                reports.Add(report);
            }

            return reports;
        }

        private void SaveReports(IEnumerable<Report> reports)
        {
            var mySqlContext = new ReportsModel();
            using (mySqlContext)
            {
                mySqlContext.Add(reports);
                mySqlContext.SaveChanges();
            }
        }
    }
}
