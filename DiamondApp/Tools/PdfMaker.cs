using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PdfSharp;
using PdfSharp.Charting;
using PdfSharp.Pdf;
using PdfSharp.Fonts;
using PdfSharp.Internal;
using PdfSharp.Drawing;
using PdfSharp.Forms;
using PdfSharp.SharpZipLib;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.RtfRendering;
using DiamondApp.Tools;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using DiamondApp.DataGridObjectClasses;
using DiamondApp.EntityModel;

namespace DiamondApp.Tools
{
    public class PdfMaker : ObservableObject
    {
        private DiamondDBEntities _ctx;
        private float orderNettoSum = 0;
        private float orderBruttoSum = 0;



        public PdfMaker()
        {
            _ctx = new DiamondDBEntities();
        }

        public bool savePdf(Document document, string path)
        {
            bool isSucces = false;
            try
            {
                //save document
                PdfDocumentRenderer renderDocument = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
                renderDocument.Document = document;
                renderDocument.RenderDocument();
                renderDocument.Save(path);
                isSucces = true;
                MessageBox.Show("Wygenerowano PDF !");
            }
            catch (Exception ex)
            {
                isSucces = false;
                MessageBox.Show("Nie udało się wygenerować PDF");
            }

            return isSucces;
        }

        public void createTable(Document document, int propId)
        {
            Table table = document.LastSection.AddTable();
            table.Borders.Visible = true;
            table.Format.Font.Size = 10;
            table.Format.Font.Name = "Calibri";

            //Create table columns
            Column column = table.AddColumn("3.750cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("3.75cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("3.75cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("1.2cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("1.2cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("1.2cm"); //1.275
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("2.475cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("2.475cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            table.Rows.Height = 3;

            Row row = table.AddRow();

            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[1].Shading.Color = Colors.LightGray;
            row.Cells[2].Shading.Color = Colors.LightGray;
            row.Cells[3].Shading.Color = Colors.LightGray;
            row.Cells[4].Shading.Color = Colors.LightGray;
            row.Cells[5].Shading.Color = Colors.LightGray;
            row.Cells[6].Shading.Color = Colors.LightGray;
            row.Cells[7].Shading.Color = Colors.LightGray;

            row.Cells[0].Format.Font.Bold = true;
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[3].Format.Font.Bold = true;
            row.Cells[4].Format.Font.Bold = true;
            row.Cells[5].Format.Font.Bold = true;
            row.Cells[6].Format.Font.Bold = true;
            row.Cells[7].Format.Font.Bold = true;

            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[7].Format.Alignment = ParagraphAlignment.Center;

            row.Cells[0].AddParagraph("WYNAJEM");
            row.Cells[1].AddParagraph("CENA JEDNOSTKOWA BRUTTO");
            row.Cells[2].AddParagraph("CENA JEDNOSTKOWA NETTO");
            row.Cells[3].AddParagraph("VAT");
            row.Cells[4].AddParagraph("ILOŚĆ");
            row.Cells[5].AddParagraph("DNI");
            row.Cells[6].AddParagraph("WARTOŚĆ NETTO");
            row.Cells[7].AddParagraph("WARTOŚĆ BRUTTO");

            createRowForHallEquipment(document, table, column, propId, row);

            row = table.AddRow();
            row.Cells[0].MergeRight = 7;
            row.Cells[0].Shading.Color = Colors.Gray;
            row.Cells[0].AddParagraph("USLUGI GASTRONOMICZNE:");
            row.Cells[0].Format.Font.Color = Colors.White;

            row = table.AddRow();

            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[1].Shading.Color = Colors.LightGray;
            row.Cells[2].Shading.Color = Colors.LightGray;
            row.Cells[3].Shading.Color = Colors.LightGray;
            row.Cells[4].Shading.Color = Colors.LightGray;
            row.Cells[5].Shading.Color = Colors.LightGray;
            row.Cells[6].Shading.Color = Colors.LightGray;
            row.Cells[7].Shading.Color = Colors.LightGray;

            row.Cells[0].Format.Font.Bold = true;
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[3].Format.Font.Bold = true;
            row.Cells[4].Format.Font.Bold = true;
            row.Cells[5].Format.Font.Bold = true;
            row.Cells[6].Format.Font.Bold = true;
            row.Cells[7].Format.Font.Bold = true;

            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[7].Format.Alignment = ParagraphAlignment.Center;

            row.Cells[0].AddParagraph("RODZAJ USŁUGI");
            row.Cells[1].AddParagraph("CENA JEDNOSTKOWA BRUTTO");
            row.Cells[2].AddParagraph("CENA JEDNOSTKOWA NETTO");
            row.Cells[3].AddParagraph("VAT");
            row.Cells[4].AddParagraph("ILOŚĆ");
            row.Cells[5].AddParagraph("DNI");
            row.Cells[6].AddParagraph("WARTOŚĆ NETTO");
            row.Cells[7].AddParagraph("WARTOŚĆ BRUTTO");

            createRowForLunch(document, table, column, propId,row);

            row = table.AddRow();
            row.Cells[0].MergeRight = 7;
            row.Cells[0].Shading.Color = Colors.Gray;
            row.Cells[0].AddParagraph("USLUGI NOCLEGOWE ze śniadaniem:");
            row.Cells[0].Format.Font.Color = Colors.White;


            row = table.AddRow();

            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[1].Shading.Color = Colors.LightGray;
            row.Cells[2].Shading.Color = Colors.LightGray;
            row.Cells[3].Shading.Color = Colors.LightGray;
            row.Cells[4].Shading.Color = Colors.LightGray;
            row.Cells[5].Shading.Color = Colors.LightGray;
            row.Cells[6].Shading.Color = Colors.LightGray;
            row.Cells[7].Shading.Color = Colors.LightGray;

            row.Cells[0].Format.Font.Bold = true;
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[3].Format.Font.Bold = true;
            row.Cells[4].Format.Font.Bold = true;
            row.Cells[5].Format.Font.Bold = true;
            row.Cells[6].Format.Font.Bold = true;
            row.Cells[7].Format.Font.Bold = true;

            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[7].Format.Alignment = ParagraphAlignment.Center;

            row.Cells[0].AddParagraph("RODZAJ POKOJU");
            row.Cells[1].AddParagraph("CENA JEDNOSTKOWA BRUTTO");
            row.Cells[2].AddParagraph("CENA JEDNOSTKOWA NETTO");
            row.Cells[3].AddParagraph("VAT");
            row.Cells[4].AddParagraph("ILOŚĆ");
            row.Cells[5].AddParagraph("DNI");
            row.Cells[6].AddParagraph("WARTOŚĆ NETTO");
            row.Cells[7].AddParagraph("WARTOŚĆ BRUTTO");

            createRowForAccomodation(document,table, column, propId, row);

            row = table.AddRow();
            row.Cells[0].MergeRight = 7;
            row.Cells[0].Shading.Color = Colors.Gray;
            row.Cells[0].AddParagraph("USLUGI DODATKOWE:");
            row.Cells[0].Format.Font.Color = Colors.White;

            row = table.AddRow();

            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[1].Shading.Color = Colors.LightGray;
            row.Cells[2].Shading.Color = Colors.LightGray;
            row.Cells[3].Shading.Color = Colors.LightGray;
            row.Cells[4].Shading.Color = Colors.LightGray;
            row.Cells[5].Shading.Color = Colors.LightGray;
            row.Cells[6].Shading.Color = Colors.LightGray;
            row.Cells[7].Shading.Color = Colors.LightGray;

            row.Cells[0].Format.Font.Bold = true;
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[3].Format.Font.Bold = true;
            row.Cells[4].Format.Font.Bold = true;
            row.Cells[5].Format.Font.Bold = true;
            row.Cells[6].Format.Font.Bold = true;
            row.Cells[7].Format.Font.Bold = true;

            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[7].Format.Alignment = ParagraphAlignment.Center;

            row.Cells[0].AddParagraph("RODZAJ USŁUGI");
            row.Cells[1].AddParagraph("CENA JEDNOSTKOWA BRUTTO");
            row.Cells[2].AddParagraph("CENA JEDNOSTKOWA NETTO");
            row.Cells[3].AddParagraph("VAT");
            row.Cells[4].AddParagraph("ILOŚĆ");
            row.Cells[5].AddParagraph("DNI");
            row.Cells[6].AddParagraph("WARTOŚĆ NETTO");
            row.Cells[7].AddParagraph("WARTOŚĆ BRUTTO");

            createRowForExtra(document, table, column, propId, row);

            createTableForPaymanet(document, propId);


        }

        public void createTableForPaymanet(Document document, int propId)
        {
            try
            {
                Table table = document.LastSection.AddTable();
                table.Borders.Visible = true;
                table.Format.Font.Size = 10;
                table.Format.Font.Name = "Calibri";

                //Create table columns
                Column column = table.AddColumn("5.42cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                column = table.AddColumn("5.42cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                column = table.AddColumn("3.96cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                column = table.AddColumn("2.5cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                column = table.AddColumn("2.5cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                table.Rows.Height = 3;

                Row row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[3].Shading.Color = Colors.LightGray;
                row.Cells[4].Shading.Color = Colors.LightGray;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[0].AddParagraph("FORMA PŁATNOŚCI:");
                row.Cells[1].AddParagraph(PaymentForm(propId));
                row.Cells[2].AddParagraph("WARTOŚĆ ZAMÓWIENIA:");
                row.Cells[2].Format.Font.Bold = true;
                row.Cells[3].AddParagraph(Convert.ToDecimal(orderNettoSum.ToString()).ToString("#,##0.00") + " zł");
                row.Cells[4].AddParagraph(Convert.ToDecimal(orderBruttoSum.ToString()).ToString("#,##0.00") + " zł");
               
                row = table.AddRow();
                row.Cells[2].MergeDown = 3;
                row.Cells[3].MergeDown = 3;
                row.Cells[4].MergeDown = 3;
                row.Cells[2].MergeRight = 2;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("NAZWA USŁUGI NA FAKTURZE:");
                row.Cells[1].AddParagraph(InvoiceServiceName(propId));
                row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[2].Format.Font.Bold = true;
                row.Cells[2].Format.Font.Size = 6;
                row.Cells[2].AddParagraph("W przypadku zlecenia podpis osoby upoważnionej, data i pieczęć firmy:");

                row = table.AddRow();
                row.Cells[2].MergeRight = 2;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("ZAMÓWIENIA INDYWIDUALNE:");
                row.Cells[1].AddParagraph(IndividualOrders(propId));

                row = table.AddRow();
                row.Cells[2].MergeRight = 2;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("PARKING:");
                row.Cells[1].AddParagraph(CarPark(propId));

                row = table.AddRow();
                row.Cells[2].MergeRight = 2;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("OPIS SALI/STOŁÓW:");
                row.Cells[1].AddParagraph(HallDescription(propId));

                row = table.AddRow();
                row.Cells[0].MergeRight = 3;
                row.Cells[0].Format.Font.Size = 7;
                row.Cells[0].AddParagraph("Zgodnie z treścią art. 71 Kodeksu Cywilnego niniejsza propozycja cenowa nie stanowi oferty handlowej ma jedynie charakter informacyjny. Informacja  jest ważna do Po upływie tego terminu lub/i w przypadku zmniejszenia ilości zarezerwowanych pokoi lub/i zmniejszenia ilości uczestników spotkania, Hotel zastrzega sobie prawo do zmiany proponowanych cen lub wycofania tych cen. Po upływie terminu jw. Hotel także nie gwarantuje dostępności pokoi i sal konferencyjnych. W przypadku zainteresowania terminem Państwa zapytania (niniejszej oferty) ze strony innego klienta, poprosimy o potwierdzenie Państwa rezerwacji i zawarcie umowy w ciągu 24 godzin lub dokonanie rezerwacji gwaratowanej bez możliwości anulacji bezkosztowej.");
                row.Cells[4].AddParagraph("2015-11-24");
                row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[4].Format.Font.Bold = true;

                row = table.AddRow();
                row.Cells[0].MergeRight = 4;
                row.Cells[0].Shading.Color = Colors.Gray; 
                row.Cells[0].Format.Font.Size = 13;
                row.Cells[0].AddParagraph("SERDECZNIE ZAPRASZAMY DO HOTELI NAZWA");
                row.Cells[0].Format.Font.Color = Colors.White;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }
        public void createRowForExtra(Document document, Table table, Column column, int propId, Row row2)
        {
            float nettoSum = 0;
            float bruttoSum = 0;
            var extra = (from r in _ctx.PropExtraServices
                         where r.Id_proposition == propId
                         select r).ToList();

            for (int i = 0; i < extra.Count; i++)
            {
                //Create table columns
                table.Rows.Height = 3;
                Row row = table.AddRow();

                string netto = ComputeNettoPrice((float)extra[i].BruttoPrice, (float)(extra[i].Vat)).ToString();

                row.Borders.Top.Visible = false;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[3].Shading.Color = Colors.LightGray;
                row.Cells[6].Shading.Color = Colors.LightGray;
                row.Cells[7].Shading.Color = Colors.LightGray;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[7].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[0].AddParagraph(extra[i].ServiceType);
                row.Cells[1].AddParagraph(Convert.ToDecimal(extra[i].BruttoPrice.ToString()).ToString("#,##0.00")+ " zł");
                row.Cells[2].AddParagraph(Convert.ToDecimal(netto.ToString()).ToString("#,##0.00") + " zł");
                row.Cells[3].AddParagraph(extra[i].Vat.ToString() + "%");
                row.Cells[4].AddParagraph(extra[i].Amount.ToString());
                row.Cells[5].AddParagraph(extra[i].Days.ToString());
                row.Cells[6].AddParagraph(Convert.ToDecimal(((float)extra[i].Days * (float)extra[i].Amount * float.Parse(netto)).ToString()).ToString("#,##0.00") + " zł");
                row.Cells[7].AddParagraph(Convert.ToDecimal(((float)extra[i].Days * (float)extra[i].Amount * (float)extra[i].BruttoPrice).ToString()).ToString("#,##0.00")+ " zł");
                nettoSum += (float)extra[i].Days * (float)extra[i].Amount * float.Parse(netto);
                bruttoSum += (float)extra[i].Days * (float)extra[i].Amount * (float)extra[i].BruttoPrice;
                
            }

            orderBruttoSum += bruttoSum;
            orderNettoSum += nettoSum;

            row2 = table.AddRow();
            row2.Cells[6].Format.Alignment = ParagraphAlignment.Center;
            row2.Cells[7].Format.Alignment = ParagraphAlignment.Center;
            row2.Cells[0].Shading.Color = Colors.LightGray;
            row2.Cells[0].Format.Font.Bold = true;
            row2.Cells[0].AddParagraph("PODSUMOWANIE");
            row2.Cells[1].MergeRight = 4;
            row2.Cells[6].AddParagraph(Convert.ToDecimal(nettoSum.ToString()).ToString("#,##0.00")+ " zł");
            row2.Cells[7].AddParagraph(Convert.ToDecimal(bruttoSum.ToString()).ToString("#,##0.00") + " zł");
        }

        public void createRowForLunch(Document document, Table table, Column column, int propId, Row row2)
        {
            float nettoSum = 0;
            float bruttoSum = 0;
            var extra = (from r in _ctx.PropMenuPosition
                         where r.Id_proposition == propId
                         select r).ToList();

            for (int i = 0; i < extra.Count; i++)
            {
                //Create table columns
                table.Rows.Height = 3;
                Row row = table.AddRow();

                string netto = ComputeNettoPrice((float)extra[i].BruttoPrice, (float)(extra[i].Vat)).ToString();

                row.Borders.Top.Visible = false;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[3].Shading.Color = Colors.LightGray;
                row.Cells[6].Shading.Color = Colors.LightGray;
                row.Cells[7].Shading.Color = Colors.LightGray;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[7].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[0].AddParagraph(extra[i].TypeOfService);
                row.Cells[1].AddParagraph(Convert.ToDecimal(extra[i].BruttoPrice.ToString()).ToString("#,##0.00") + " zł");
                row.Cells[2].AddParagraph(Convert.ToDecimal(netto.ToString()).ToString("#,##0.00") + " zł");
                row.Cells[3].AddParagraph(extra[i].Vat.ToString() + "%");
                row.Cells[4].AddParagraph(extra[i].Amount.ToString());
                row.Cells[5].AddParagraph(extra[i].Days.ToString());
                row.Cells[6].AddParagraph(Convert.ToDecimal(((float)extra[i].Days * (float)extra[i].Amount * float.Parse(netto)).ToString()).ToString("#,##0.00") + " zł");
                row.Cells[7].AddParagraph(Convert.ToDecimal(((float)extra[i].Days * (float)extra[i].Amount * (float)extra[i].BruttoPrice).ToString()).ToString("#,##0.00") + " zł");
                nettoSum += (float)extra[i].Days * (float)extra[i].Amount * float.Parse(netto);
                bruttoSum += (float)extra[i].Days * (float)extra[i].Amount * (float)extra[i].BruttoPrice;

            }

            orderBruttoSum += bruttoSum;
            orderNettoSum += nettoSum;

            row2 = table.AddRow();
            row2.Cells[6].Format.Alignment = ParagraphAlignment.Center;
            row2.Cells[7].Format.Alignment = ParagraphAlignment.Center;
            row2.Cells[0].Shading.Color = Colors.LightGray;
            row2.Cells[0].Format.Font.Bold = true;
            row2.Cells[0].AddParagraph("PODSUMOWANIE");
            row2.Cells[1].MergeRight = 4;
            row2.Cells[6].AddParagraph(Convert.ToDecimal(nettoSum.ToString()).ToString("#,##0.00") + " zł");
            row2.Cells[7].AddParagraph(Convert.ToDecimal(bruttoSum.ToString()).ToString("#,##0.00") + " zł");
        }

        public void createRowForAccomodation(Document document, Table table, Column column, int propId, Row row2)
        {
            float nettoSum = 0;
            float bruttoSum = 0;
            var extra = (from r in _ctx.PropAccomodation
                         where r.Id_proposition == propId
                         select r).ToList();

            for (int i = 0; i < extra.Count; i++)
            {
                //Create table columns
                table.Rows.Height = 3;
                Row row = table.AddRow();

                string netto = ComputeNettoPrice((float)extra[i].BruttoPrice, (float)(extra[i].Vat)).ToString();

                row.Borders.Top.Visible = false;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[3].Shading.Color = Colors.LightGray;
                row.Cells[6].Shading.Color = Colors.LightGray;
                row.Cells[7].Shading.Color = Colors.LightGray;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[7].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[0].AddParagraph(extra[i].TypeOfRoom);
                row.Cells[1].AddParagraph(Convert.ToDecimal(extra[i].BruttoPrice.ToString()).ToString("#,##0.00") + " zł");
                row.Cells[2].AddParagraph(Convert.ToDecimal(netto.ToString()).ToString("#,##0.00") + " zł");
                row.Cells[3].AddParagraph(extra[i].Vat.ToString() + "%");
                row.Cells[4].AddParagraph(extra[i].Amount.ToString());
                row.Cells[5].AddParagraph(extra[i].Days.ToString());
                row.Cells[6].AddParagraph(Convert.ToDecimal(((float)extra[i].Days * (float)extra[i].Amount * float.Parse(netto)).ToString()).ToString("#,##0.00") + " zł");
                row.Cells[7].AddParagraph(Convert.ToDecimal(((float)extra[i].Days * (float)extra[i].Amount * (float)extra[i].BruttoPrice).ToString()).ToString("#,##0.00") + " zł");
                nettoSum += (float)extra[i].Days * (float)extra[i].Amount * float.Parse(netto);
                bruttoSum += (float)extra[i].Days * (float)extra[i].Amount * (float)extra[i].BruttoPrice;

            }

            orderBruttoSum += bruttoSum;
            orderNettoSum += nettoSum;

            row2 = table.AddRow();
            row2.Cells[6].Format.Alignment = ParagraphAlignment.Center;
            row2.Cells[7].Format.Alignment = ParagraphAlignment.Center;
            row2.Cells[0].Shading.Color = Colors.LightGray;
            row2.Cells[0].Format.Font.Bold = true;
            row2.Cells[0].AddParagraph("PODSUMOWANIE");
            row2.Cells[1].MergeRight = 4;
            row2.Cells[6].AddParagraph(Convert.ToDecimal(nettoSum.ToString()).ToString("#,##0.00") + " zł");
            row2.Cells[7].AddParagraph(Convert.ToDecimal(bruttoSum.ToString()).ToString("#,##0.00") + " zł");
        }
        public void createRowForHallEquipment(Document document, Table table, Column column, int propId, Row row2)
        {
            float nettoSum = 0;
            float bruttoSum = 0;
            var extra = (from r in _ctx.PropHallEquipment
                         where r.Id_proposition == propId
                         select r).ToList();

            for (int i = 0; i < extra.Count; i++)
            {
                //Create table columns
                table.Rows.Height = 3;
                Row row = table.AddRow();

                string netto = ComputeNettoPrice((float)extra[i].BruttoPrice, (float)(extra[i].Vat)).ToString();

                row.Borders.Top.Visible = false;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[3].Shading.Color = Colors.LightGray;
                row.Cells[6].Shading.Color = Colors.LightGray;
                row.Cells[7].Shading.Color = Colors.LightGray;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[7].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[0].AddParagraph(extra[i].Things);
                row.Cells[1].AddParagraph(Convert.ToDecimal(extra[i].BruttoPrice.ToString()).ToString("#,##0.00") + " zł");
                row.Cells[2].AddParagraph(Convert.ToDecimal(netto.ToString()).ToString("#,##0.00") + " zł");
                row.Cells[3].AddParagraph(extra[i].Vat.ToString() + "%");
                row.Cells[4].AddParagraph(extra[i].Amount.ToString());
                row.Cells[5].AddParagraph(extra[i].Days.ToString());
                row.Cells[6].AddParagraph(Convert.ToDecimal(((float)extra[i].Days * (float)extra[i].Amount * float.Parse(netto)).ToString()).ToString("#,##0.00") + " zł");
                row.Cells[7].AddParagraph(Convert.ToDecimal(((float)extra[i].Days * (float)extra[i].Amount * (float)extra[i].BruttoPrice).ToString()).ToString("#,##0.00") + " zł");
                nettoSum += (float)extra[i].Days * (float)extra[i].Amount * float.Parse(netto);
                bruttoSum += (float)extra[i].Days * (float)extra[i].Amount * (float)extra[i].BruttoPrice;

            }

            orderBruttoSum += bruttoSum;
            orderNettoSum += nettoSum;

            row2 = table.AddRow();
            row2.Cells[6].Format.Alignment = ParagraphAlignment.Center;
            row2.Cells[7].Format.Alignment = ParagraphAlignment.Center;
            row2.Cells[0].Shading.Color = Colors.LightGray;
            row2.Cells[0].Format.Font.Bold = true;
            row2.Cells[0].AddParagraph("PODSUMOWANIE");
            row2.Cells[1].MergeRight = 4;
            row2.Cells[6].AddParagraph(Convert.ToDecimal(nettoSum.ToString()).ToString("#,##0.00") + " zł");
            row2.Cells[7].AddParagraph(Convert.ToDecimal(bruttoSum.ToString()).ToString("#,##0.00") + " zł");
        }


        public void createPdf(string propId, string path)
        {
            try
            {
                int propId2 = Int32.Parse(propId);
                //new document
                Document document = new Document();

                //set default margin
                document.DefaultPageSetup.RightMargin = 18;
                document.DefaultPageSetup.LeftMargin = 18;

                //default section and paragraph, formatting paragraph
                Section section = document.AddSection();
                Paragraph paragraph = section.AddParagraph("Hotel Nazwa Hotelu Jakas Jakas ****");
                paragraph.Format.Font.Size = 14;
                paragraph.Format.Font.Color = Colors.DarkRed;
                paragraph.Format.SpaceBefore = "2.2cm";
                paragraph.Format.SpaceAfter = "0.4cm";
                paragraph.Format.Alignment = ParagraphAlignment.Center;
                paragraph.Format.Font.Bold = true;

                //add logo
                Image image = section.AddImage("logo.png");
                image.Width = Unit.FromPoint(593);
                image.Height = Unit.FromPoint(125);
                image.RelativeVertical = RelativeVertical.Page;
                image.RelativeHorizontal = RelativeHorizontal.Page;
                image.WrapFormat.Style = WrapStyle.Through;

                //Create table, set font size and font style
                Table table = document.LastSection.AddTable();
                table.Borders.Visible = true;
                table.Format.Font.Size = 10;
                table.Format.Font.Name = "Calibri";

                //Create table columns
                Column column = table.AddColumn("4.95cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                column = table.AddColumn("4.95cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                column = table.AddColumn("4.95cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                column = table.AddColumn("4.95cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                table.Rows.Height = 3;

                Row row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("DOTYCZY:");
                row.Cells[1].MergeDown = 1;
                //row.Cells[3].MergeDown = 1; // merge down third cell with next one
                row.Cells[1].AddParagraph("Nazwa Hotelu ****");
                row.Cells[1].AddParagraph("ul. Przykładowa 122a");
                row.Cells[1].AddParagraph("00-000 Przykład");
                row.Cells[2].AddParagraph("DLA:");
                row.Cells[3].AddParagraph(CompanyName(propId2));

                row = table.AddRow();
                row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Top;
                row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Top;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("ADRES:");
                row.Cells[2].AddParagraph("ADRES:");
                row.Cells[3].AddParagraph(CompanyAddress(propId2));

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("NIP:");
                row.Cells[1].AddParagraph("0000000000");
                row.Cells[2].AddParagraph("NIP:");
                row.Cells[3].AddParagraph(CompanyNip(propId2));


                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("DATA AKTUALIZACJI:");
                row.Cells[1].AddParagraph(SelectDate(propId2).ToString());
                row.Cells[2].AddParagraph("TERMIN WAŻNOŚCI PROPOZYCJI:");
                row.Cells[3].AddParagraph(UpdatedDate(propId2).ToString());

                row = table.AddRow();
                row.Cells[0].MergeRight = 1;
                row.Cells[2].MergeRight = 1;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("KONTAKT ZE STRONY HOTELU:");
                row.Cells[2].AddParagraph("KONTAKT ZE STRONY ZAMAWIAJĄCEGO:");

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph(SelectUserPosition(SelectUserId(propId2)));
                row.Cells[1].AddParagraph(SelectUserName(SelectUserId(propId2)) + " " + SelectUserSurname(SelectUserId(propId2)));
                row.Cells[2].AddParagraph("Sz.P.:");
                row.Cells[3].AddParagraph(CustomerFullName(propId2));

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("TELEFON:");
                row.Cells[1].AddParagraph(SelectUserPhone(SelectUserId(propId2)));
                row.Cells[2].AddParagraph("TELEFON:");
                row.Cells[3].AddParagraph(CustomerPhoneNumber(propId2));

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("ADRES E-MAIL:");
                row.Cells[1].AddParagraph(SelectUserEmail(SelectUserId(propId2)));
                row.Cells[2].AddParagraph("ADRES E-MAIL:");
                row.Cells[3].AddParagraph(CustomerEmail(propId2));

                row = table.AddRow();
                row.Cells[0].MergeRight = 1;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[2].AddParagraph("OSOBA DECYZYJNA:");
                row.Cells[3].AddParagraph(DecisingPersonFullName(propId2));

                row = table.AddRow();
                row.Cells[0].MergeRight = 3;
                row.Cells[0].Shading.Color = Colors.Gray;
                row.Cells[1].Shading.Color = Colors.Gray;
                row.Cells[2].Shading.Color = Colors.Gray;
                row.Cells[0].AddParagraph("SZCZEGÓŁY REZERWACJI:");
                row.Cells[0].Format.Font.Color = Colors.White;

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("DATA ROZPOCZĘCIA:");
                row.Cells[1].AddParagraph(StartData(propId2) + " " + StartTime(propId2).ToString());
                row.Cells[2].AddParagraph("ILOŚĆ OSÓB:");
                row.Cells[3].AddParagraph(PeopleNumber(propId2).ToString());

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("DATA ZAKOŃCZENIA:");
                row.Cells[1].AddParagraph(EndData(propId2) + " " + EndTime(propId2).ToString());
                row.Cells[2].AddParagraph("USTAWIENIE SALI:");
                row.Cells[3].AddParagraph(HallSetting(propId2));

                createTable(document, propId2);

                savePdf(document, path);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string SelectDate(int propositionId)
        {
            var var = (from prop in _ctx.Proposition
                       where prop.Id == propositionId
                       select prop.UpdateDate).Single();
            return var != null ? var.ToString().Substring(0, 10) : "";
        }

        public string UpdatedDate(int propositionId)
        {
            var var = (from prop in _ctx.Proposition
                       where prop.Id == propositionId
                       select prop.UpdateDate).Single();
            return var != null ? var.AddDays(4).ToString().Substring(0, 10) : "";
        }
        public int SelectUserId(int propositionId)
        {
            var var = (from prop in _ctx.Proposition
                       where prop.Id == propositionId
                       select prop.Id_user).Single();
            return var;
        }

        public string SelectUserPhone(int userId)
        {
            var var = (from user in _ctx.Users
                       where user.Id == userId
                       select user.PhoneNum).Single();
            return var;
        }

        public string SelectUserEmail(int userId)
        {
            var var = (from user in _ctx.Users
                       where user.Id == userId
                       select user.Email).Single();
            return string.IsNullOrEmpty(var) ? "" : var;
        }

        public string SelectUserName(int userId)
        {
            var var = (from user in _ctx.Users
                       where user.Id == userId
                       select user.Name).Single();
            return string.IsNullOrEmpty(var) ? "" : var;
        }

        public string SelectUserSurname(int userId)
        {
            var var = (from user in _ctx.Users
                       where user.Id == userId
                       select user.Surname).Single();
            return string.IsNullOrEmpty(var) ? "" : var;
        }

        public string SelectUserPosition(int userId)
        {
            var var = (from user in _ctx.Users
                       where user.Id == userId
                       select user.Position).Single();
            return string.IsNullOrEmpty(var) ? "" : var;
        }

        public string CompanyName(int propositionId)
        {
            var var = (from propclient in _ctx.PropClient
                       where propclient.Id_proposition == propositionId
                       select propclient.CompanyName).Single();
            return string.IsNullOrEmpty(var) ? "" : var;
        }

        public string CompanyNip(int propositionId)
        {
            var var = (from propclient in _ctx.PropClient
                       where propclient.Id_proposition == propositionId
                       select propclient.NIP).Single();
            return string.IsNullOrEmpty(var) ? "" : var;
        }

        public string CompanyAddress(int propositionId)
        {
            var var = (from propclient in _ctx.PropClient
                       where propclient.Id_proposition == propositionId
                       select propclient.CompanyAdress).Single();
            return string.IsNullOrEmpty(var) ? "" : var;
        }

        public string CustomerFullName(int propositionId)
        {
            var var = (from propclient in _ctx.PropClient
                       where propclient.Id_proposition == propositionId
                       select propclient.CustomerFullName).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string CustomerPhoneNumber(int propositionId)
        {
            var var = (from propclient in _ctx.PropClient
                       where propclient.Id_proposition == propositionId
                       select propclient.PhoneNum).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string DecisingPersonFullName(int propositionId)
        {
            var var = (from propclient in _ctx.PropClient
                       where propclient.Id_proposition == propositionId
                       select propclient.DecisingPersonFullName).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string CustomerEmail(int propositionId)
        {
            var var = (from propclient in _ctx.PropClient
                       where propclient.Id_proposition == propositionId
                       select propclient.CustomerEmail).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string PaymentForm(int propositionId)
        {
            var var = (from payform in _ctx.PropPaymentSuggestions
                       where payform.Id_proposition == propositionId
                       select payform.PaymentForm).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string InvoiceServiceName(int propositionId)
        {
            var var = (from payform in _ctx.PropPaymentSuggestions
                       where payform.Id_proposition == propositionId
                       select payform.InvoiceServiceName).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string IndividualOrders(int propositionId)
        {
            var var = (from payform in _ctx.PropPaymentSuggestions
                       where payform.Id_proposition == propositionId
                       select payform.IndividualOrders).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string CarPark(int propositionId)
        {
            var var = (from payform in _ctx.PropPaymentSuggestions
                       where payform.Id_proposition == propositionId
                       select payform.CarPark).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string HallDescription(int propositionId)
        {
            var var = (from payform in _ctx.PropPaymentSuggestions
                       where payform.Id_proposition == propositionId
                       select payform.HallDescription).Single();
            return string.IsNullOrEmpty(var) ? "" : var;;
        }

        public string PeopleNumber(int propositionId)
        {
            var var = (from details in _ctx.PropReservationDetails
                       where details.Id_proposition == propositionId
                       select details.PeopleNumber).Single();
            if (var == null)
                return "";
            return var.ToString();

        }

        public string HallSetting(int propositionId)
        {
            var var = (from details in _ctx.PropReservationDetails
                       where details.Id_proposition == propositionId
                       select details.HallSetting).Single();
            return string.IsNullOrEmpty(var) ? "" : var; ;
        }

        public string StartData(int propositionId)
        {
            var var = (from details in _ctx.PropReservationDetails
                       where details.Id_proposition == propositionId
                       select details.StartData).Single();
            return var != null ? var.ToString().Substring(0, 10) : "";
        }

        public string EndData(int propositionId)
        {
            var var = (from details in _ctx.PropReservationDetails
                       where details.Id_proposition == propositionId
                       select details.EndData).Single();
            return var!=null ? var.ToString().Substring(0, 10) : "";
        }

        public string StartTime(int propositionId)
        {
            var var = (from details in _ctx.PropReservationDetails
                       where details.Id_proposition == propositionId
                       select details.StartTime).Single();
            return var != null ? var.ToString() : "";
        }

        public string EndTime(int propositionId)
        {
            var var = (from details in _ctx.PropReservationDetails
                       where details.Id_proposition == propositionId
                       select details.EndTime).Single();
            return var != null ? var.ToString() : "";

        }

        private decimal ComputeNettoPrice(float? value, float? vat)
        {
            if (value == null)
                value = 0;
            return Math.Round(((decimal)value * 100 / (100 + (decimal)vat)), 2);
        }

        private string addrSplit(string addr,int start, int end)
        {
            string[] tokens = addr.Split(' ');
            return tokens[start] + " " + tokens[end];
        }
    }
}
