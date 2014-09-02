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
        public XmlController()
        {
            SQLController sqlController = new SQLController();
            this.reports = sqlController.GetTotalIncomeByDate();
        }
        public void ExportXmlReport()
        {
            string path = "../../../XMLReports";
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
                    WriteBook(writer, serviceReport.ServiceName, date, serviceReport.TotalSum.ToString());     
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            Console.WriteLine("Document {0} created.", fileName);
        }

        private static void WriteBook(XmlWriter writer, string service, string date, string totalSum)
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
