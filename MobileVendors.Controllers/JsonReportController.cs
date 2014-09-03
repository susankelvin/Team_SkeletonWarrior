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
                                        Id = s.ServiceId,
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

                var groupsByVendor = sub.GroupBy(v => v.VendorName).ToList();
                var counter = 1;
                foreach (var vendor in groupsByVendor)
                {
                    product["vendor-name"] = vendor.Key;

                    int totalQuantity = 0;
                    decimal totalIncome = 0;
                    var vendorSub = sub.Where(s => s.VendorName == vendor.Key);
                    
                    foreach (var item in vendorSub)
                    {
                        totalQuantity += item.Quantity;
                        totalIncome += item.TotalIncome;
                    }
                    product["total-quantity-sold"] = totalQuantity;
                    product["total-incomes"] = totalIncome * totalQuantity;
                    this.WriteReportToFile(product.ToString(), first.Id.ToString() + counter);
                    counter++;
                }
            }
        }

        public void WriteReportToFile(string json, string id)
        {
            string filePath = string.Format("{0}{1}.json", @"..\..\Json-Reports\", id);
            File.WriteAllText(filePath, json);
        }
    }
}