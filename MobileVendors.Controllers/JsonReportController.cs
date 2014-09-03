namespace MobileVendors.Controllers
{
    using System;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    using MobileVendors.Data;
    

    public class JsonReportController
    {
        private readonly IMobileVendorsData data;

        public JsonReportController() : this(new MobileVendorsData())
        {
        }

        public JsonReportController(IMobileVendorsData data)
        {
            this.data = data;
        }

        public void CreateReport()
        { 
            var subscriptions = this.data.Subscriptions.All()
                                    .Select(s => new
                                    {
                                        Id = s.Id,
                                        ProductName = s.Service.ServiceName,
                                        VendorName = s.Store.Vendor.VendorName,
                                        TotalIncome = s.TotalIncome,
                                        Quantity = s.Quantity
                                    })
                                    .GroupBy(s => s.ProductName)                
                                    .ToList();

            foreach (var sub in subscriptions)
            {
                var first = sub.First();
                JObject product = new JObject();
                product["product-id"] = first.Id;
                product["product-name"] = first.ProductName;
                product["vendor-name"] = first.VendorName;

                int totalQuantity = 0;
                decimal totalIncome = 0;

                foreach (var item in sub)
                {
                    totalQuantity += item.Quantity;
                    totalIncome += item.TotalIncome;
                }

                product["total-quantity-sold"] = totalQuantity;
                product["total-incomes"] = totalIncome;
                
                this.WriteReportToFile(product.ToString(), first.Id.ToString());
            }
        }

        public void WriteReportToFile(string json, string id)
        {
            string filePath = string.Format("{0}{1}.json", @"..\..\Json-Reports\", id);
            File.WriteAllText(filePath, json);
        }
    }
}