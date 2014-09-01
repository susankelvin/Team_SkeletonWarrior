namespace MobileVendors.ConsoleClient
{
    using System;
    using System.Linq;

    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.Tables;
    using MigraDoc.Rendering;

    using MobileVendors.Data;

    public class PdfReportGenerator
    {
        private readonly IMobileVendorsData data;
        private Document document;
        private Table table;

        public PdfReportGenerator()
            : this(new MobileVendorsData())
        {
        }

        public PdfReportGenerator(IMobileVendorsData data)
        {
            this.data = data;
        }

        public void GeneratePdfExport(DateTime startDate, DateTime endDate, string path = "")
        {
            var fileName = string.Format("{0}{1}.{2}.{3}-{4}.{5}.{6}.pdf", path, startDate.Day, startDate.Month, startDate.Year, endDate.Day, endDate.Month, endDate.Year);

            this.CreateDocument();
            this.CreateStyles();
            this.CreateTable();
            this.FillData(startDate, endDate);
            this.RenderDocument(fileName);
        }

        private void CreateDocument()
        {
            this.document = new Document();

            this.document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(2);
            this.document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(2);
            this.document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(2);
            this.document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(2);
        }

        private void CreateStyles()
        {
            var heading = this.document.AddStyle("Heading", "Normal");
            heading.Font.Size = 12;
            heading.Font.Bold = true;
        }

        private void CreateTable()
        {
            this.table = this.document.AddSection().AddTable();

            this.table.Rows.Height = Unit.FromCentimeter(0.7);
            this.table.Rows.VerticalAlignment = VerticalAlignment.Center;

            this.table.AddColumn(Unit.FromCentimeter(3.5));
            this.table.AddColumn(Unit.FromCentimeter(2.7));
            this.table.AddColumn(Unit.FromCentimeter(1.5));
            this.table.AddColumn(Unit.FromCentimeter(1.9));
            this.table.AddColumn(Unit.FromCentimeter(5.4));
            this.table.AddColumn(Unit.FromCentimeter(2.1));

            var row = this.table.AddRow();
            row.Cells[0].MergeRight = 5;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Height = Unit.FromCentimeter(1.3);
            row.Cells[0].AddParagraph().AddFormattedText("Agregated Subsciptions Report", "Heading");
        }

        private void FillData(DateTime from, DateTime to)
        {
            var reportData = this.data.Subscriptions
                .SearchFor(s => s.SubscribeDate >= from && s.SubscribeDate < to)
                .Select(s => new
                {
                    Date = s.SubscribeDate,
                    Service = s.Service.ServiceName,
                    Price = s.Service.Price,
                    Period = s.PeriodInYears,
                    Quantity = s.Quantity,
                    Store = s.Store.Vendor.VendorName + " " + s.Store.Town.TownName + " " + s.Store.Address,
                    Sum = s.TotalIncome
                })
                .GroupBy(s => s.Date);

            var grandTotal = 0m;
            Row row = default(Row);

            foreach (var reports in reportData)
            {
                var date = reports.Key;
                var currentDate = string.Format("Date: {0}.{1}.{2}",
                    date.Day < 10 ? "0" + date.Day : date.Day.ToString(),
                    date.Month < 10 ? "0" + date.Month : date.Month.ToString(),
                    date.Year);

                var total = 0m;

                row = this.table.AddRow();
                row.Cells[0].MergeRight = 5;
                row.Format.Alignment = ParagraphAlignment.Left;
                row.Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph(currentDate);

                row = this.table.AddRow();
                row.Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph().AddFormattedText("Service", TextFormat.Bold);
                row.Cells[1].AddParagraph().AddFormattedText("Monthly Fee", TextFormat.Bold);
                row.Cells[2].AddParagraph().AddFormattedText("Period", TextFormat.Bold);
                row.Cells[3].AddParagraph().AddFormattedText("Quantity", TextFormat.Bold);
                row.Cells[4].AddParagraph().AddFormattedText("Shop", TextFormat.Bold);
                row.Cells[5].AddParagraph().AddFormattedText("Sum", TextFormat.Bold);

                foreach (var report in reports)
                {
                    row = this.table.AddRow();
                    row.Cells[0].AddParagraph(report.Service);
                    row.Cells[1].AddParagraph(report.Price.ToString());
                    row.Cells[2].AddParagraph(report.Period.ToString());
                    row.Cells[3].AddParagraph(report.Quantity.ToString());
                    row.Cells[4].AddParagraph(report.Store);
                    row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                    row.Cells[5].AddParagraph(report.Sum.ToString());

                    total += report.Sum;
                }

                row = this.table.AddRow();
                row.Format.Alignment = ParagraphAlignment.Right;
                row.Cells[0].MergeRight = 4;
                row.Cells[0].AddParagraph("Total:");
                row.Cells[5].AddParagraph(total.ToString());

                grandTotal += total;
            }

            row = this.table.AddRow();
            row.Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 4;
            row.Cells[0].AddParagraph().AddFormattedText("Grand Total:", TextFormat.Bold);
            row.Cells[5].AddParagraph().AddFormattedText(grandTotal.ToString(), TextFormat.Bold);

            this.table.SetEdge(0, 0, this.table.Columns.Count, this.table.Rows.Count, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);
            this.table.SetEdge(0, 0, this.table.Columns.Count, this.table.Rows.Count, Edge.Interior, BorderStyle.Single, 0.5, Colors.Black);
        }

        private void RenderDocument(string fileName)
        {
            var renderer = new PdfDocumentRenderer(true);
            renderer.Document = this.document;
            renderer.RenderDocument();
            renderer.Save(fileName);
        }
    }
}
