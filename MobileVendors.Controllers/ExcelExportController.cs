namespace MobileVendors.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Linq;
    using MobileVendors.Data;

    public class ExcelExportController
    {
        private readonly OleDbConnection connection;

        public ExcelExportController(string fileName)
        {
            string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\{0};Extended Properties=\"Excel 12.0 XML;HDR=Yes;\"", fileName);
            this.connection = new OleDbConnection(connectionString);
        }

        public void GenerateReport()
        {
            this.CreateTable();
            this.InsertData();
        }

        private void CreateTable()
        {
            string tableName = "[FinancialResults]";
            string columnNames = "[ServiceName] nvarchar(50), [Incomes] money, [Expenses] money, [Taxes] money, [FinancialResult] money";
            this.connection.Open();
            try
            {
                var command = new OleDbCommand(string.Format("CREATE TABLE {0} ({1})", tableName, columnNames), this.connection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
           
            this.connection.Close();
        }

        private void InsertData()
        {
            var sqliteController = new TaxesData();
            var mySqlController = new MySqlController();
            var reports = mySqlController.GetReports();
            var groupedReportsByVendor = reports.GroupBy(v => v.VendorName);

            var pairs = new KeyValuePair<string, object>[5];

            foreach (var report in groupedReportsByVendor)
            {
                var vendorName = report.Key;
                var totalIncome = reports.Where(r => r.VendorName == vendorName).Sum(inc => inc.TotalIncomes);
                var tax = sqliteController.Taxes.All().Where(t => t.ServiceName == vendorName); 
                var taxes = tax.Sum(t => t.Tax) / tax.Count();
                var expenses = reports.First(r => r.VendorName == vendorName).Expenses;
                
                pairs[0] = new KeyValuePair<string, object>("VendorName",report.Key);
                pairs[3] = new KeyValuePair<string, object>("Incomes",totalIncome);
                pairs[2] = new KeyValuePair<string, object>("Expenses",expenses);
                pairs[1] = new KeyValuePair<string, object>("Taxes", taxes);
                pairs[4] = new KeyValuePair<string, object>("FinancialResult",totalIncome / (1 + taxes) - expenses);
                InsertRow(pairs);
            }
        }

        private void InsertRow(KeyValuePair<string, object>[]keys)
        {
            var values = keys.Select(key => string.Format("@{0}", key.Key));
            var command = new OleDbCommand(string.Format(
                "INSERT INTO [FinancialResults$]({0}) VALUES ({1})",
                string.Join(", ", keys.Select(x => x.Key)), string.Join(", ", values)),
                this.connection);

            this.connection.Open();
            
            foreach (var key in keys)
            { 
                var parameter = command.CreateParameter();
                parameter.ParameterName = string.Format("@{0}", key.Key);
                parameter.Value = key.Value;
                command.Parameters.Add(parameter);
            }
            command.Connection = this.connection;
            command.ExecuteNonQuery();

            this.connection.Close();
        }
    }
}