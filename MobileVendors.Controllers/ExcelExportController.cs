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

        public void ExportData()
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
            var pairs = new KeyValuePair<string, object>[5];

            foreach (var report in reports)
            {
                var tax = sqliteController.Taxes.All().First(t => t.ServiceName == report.ProductName).Tax;
                
                pairs[0] = new KeyValuePair<string, object>("ServiceName",report.ProductName);
                pairs[3] = new KeyValuePair<string, object>("Incomes",report.TotalIncomes);
                pairs[2] = new KeyValuePair<string, object>("Expenses",report.Expenses);
                pairs[1] = new KeyValuePair<string, object>("Taxes", report.Expenses * tax);
                pairs[4] = new KeyValuePair<string, object>("FinancialResult",report.TotalIncomes - report.Expenses * tax);
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