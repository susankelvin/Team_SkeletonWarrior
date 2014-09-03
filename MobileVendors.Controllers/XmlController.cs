using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MobileVendors.Models;
using System.Globalization;
using System.IO;

namespace MobileVendors.Controllers
{
    public class XmlController
    {
        private List<ServicesReport> reports;

        private string path;

        public XmlController()
        {
            this.path = "../../../XMLReports";
            //SQLController sqlController = new SQLController();
            //this.reports = sqlController.GetTotalIncomeByDate();
        }
        public void ExportXmlReport()
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo dir = Directory.CreateDirectory(path);
            }

            string fileName = path + "/Sales-For-Each-Service-report.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            using (XmlTextWriter writer = new XmlTextWriter(fileName, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("sales");

                foreach (ServicesReport serviceReport in this.reports)
                {
                    String date = serviceReport.Date.ToString("yyyy-MM-dd");
                    WriteSale(writer, serviceReport.ServiceName, date, serviceReport.TotalSum.ToString());     
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            Console.WriteLine("Document {0} created.", fileName);
        }

        public List<StoreExpenses> ImportXmlReport()
        {
            List<StoreExpenses> storesExpenses = new List<StoreExpenses>();

            using (XmlReader reader = XmlReader.Create(this.path + "/Expenses-Per-Store-Report.xml"))
            {
                StoreExpenses storeExpenses = null;

                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element))
                    {
                        if (reader.Name == "store")
                        {
                            storeExpenses = new StoreExpenses();
                            storeExpenses.StoreName = reader.GetAttribute("name");
                        }
                        else if (reader.Name == "expenses")
                        {
                            KeyValuePair<String, String> expenses = new KeyValuePair<string, string>(reader.GetAttribute("month"), reader.ReadElementString());

                            storeExpenses.ExpensesPerMonth.Add(expenses);

                            storesExpenses.Add(storeExpenses);
                        }
                    }
                }
            }

            return storesExpenses;
        }

        private static void WriteSale(XmlWriter writer, string service, string date, string totalSum)
        {
            writer.WriteStartElement("sale");
            writer.WriteAttributeString("service", service);
            writer.WriteStartElement("summary","");
            writer.WriteAttributeString("date", date);
            writer.WriteAttributeString("total-sum", totalSum);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}
