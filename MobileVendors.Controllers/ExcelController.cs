namespace MobileVendors.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Linq;

    using MobileVendors.Data;

    public class ExcelController
    {
        private readonly OleDbConnection connection;

        public ExcelController(string fileName)
        {
            string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\{0};Extended Properties=\"Excel 12.0 XML;HDR=Yes;\"", fileName);
            this.connection = new OleDbConnection(connectionString);
        }

        public void ExportData()
        {
            this.CreateTable();
            var data = this.GetData();
            this.InsertData(data);
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

        private KeyValuePair<string, object>[] GetData()
        {
            var pairs = new KeyValuePair<string, object>[5];
            var sqliteController = new TaxesData();
            var tax = sqliteController.Taxes.All().First();
            pairs[0] = new KeyValuePair<string, object>("ServiceName",tax.ServiceName);
            pairs[3] = new KeyValuePair<string, object>("Incomes",150);
            pairs[2] = new KeyValuePair<string, object>("Expenses",50.00);
            pairs[1] = new KeyValuePair<string, object>("Taxes", 50.00 * tax.Tax);
            pairs[4] = new KeyValuePair<string, object>("FinancialResult",75.00);
            return pairs;
        }

        private void InsertData(KeyValuePair<string, object>[]keys)
        {
            var values = keys.Select(key => string.Format("@{0}", key.Key));
            var command = new OleDbCommand(string.Format("INSERT INTO [FinancialResults$]({0}) VALUES ({1})",
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