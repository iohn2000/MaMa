using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SyncFusionPdfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Draw a header : https://www.syncfusion.com/kb/9789/how-to-draw-multiline-text-in-pdf-using-c-and-vb-net
            //Draw PdfLightTable.
            //PdfGraphics graphics = page.Graphics;
            //PdfFont font2 = new PdfStandardFont(PdfFontFamily.Helvetica, 14);

            RechenAufgabenPdf p = new RechenAufgabenPdf();
            p.SavePdf();
        }


    }

    public class RechenAufgabenPdf
    {
        PdfDocument doc = new PdfDocument();
        

        public void SavePdf()
        {
            doc = DrawPdf();

            //Save the document.
            var fileStream = File.Create("out.pdf");
            doc.Save(fileStream);
            doc.Close(true);
        }
        public PdfDocument DrawPdf()
        {
            //Create a new PDF document.
            
            PdfMargins marg = new PdfMargins();
            marg.All = 15f;
            doc.PageSettings.Size = PdfPageSize.A4;
            doc.PageSettings.Margins = marg;
            PdfPage page = doc.Pages.Add();

            var pdf = CreateTable();
            var data = FillData();
            //Assign data source.
            pdf.DataSource = data;

            pdf.Draw(page, new PointF(0, 0));


            return doc;
        }

        public DataTable FillData()
        {
            // Initialize DataTable to assign as DateSource to the light table.
            DataTable table = new DataTable();
            //Include columns to the DataTable.
            table.Columns.Add("1");
            table.Columns.Add("2");
            table.Columns.Add("3");

            //Include rows to the DataTable.

            for (int i = 0; i < 55; i++)
            {
                table.Rows.Add(new string[] { "399   / 0,6  = 665", "59,52 / 0,16 = 372", "142   x 9,67 = 1373,14" });
                table.Rows.Add(new string[] { "822,6 / 0,18 = 4570", "810,9 / 0,05 = 16218" });
                table.Rows.Add(new string[] { "4392  / 0,09 = 48800", "28,9  x 132  = 3814,8" });
                table.Rows.Add(new string[] { "0,907 x 580  = 526,060", "0,609 x 798  = 485,982" });
            }
            return table;
        }

        public PdfLightTable CreateTable()
        {

            PdfLightTable pdfLightTable = new PdfLightTable();
            PdfCellStyle myStyle = new PdfCellStyle();

            PdfFont font1 = new PdfStandardFont(PdfFontFamily.Courier, 12);
            myStyle.Font = font1;
            myStyle.BorderPen = new PdfPen(Color.Transparent);

            pdfLightTable.Style.DefaultStyle = myStyle;

            pdfLightTable.BeginRowLayout += PdfLightTable_BeginRowLayout;
            return pdfLightTable;
        }

        private static void PdfLightTable_BeginRowLayout(object sender, BeginRowLayoutEventArgs args)
        {
            args.MinimalHeight = 30f;
        }
    }
}
