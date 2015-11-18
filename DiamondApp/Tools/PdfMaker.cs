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

namespace DiamondApp.Tools
{
    public class PdfMaker : ObservableObject
    {
        public bool savePdf(Document document)
        {
            bool isSucces = false;
            try
            {
                //save document
                PdfDocumentRenderer renderDocument = new PdfDocumentRenderer(false);
                renderDocument.Document = document;
                renderDocument.RenderDocument();
                renderDocument.Save("test.pdf");
                isSucces = true;
                MessageBox.Show("Udaało się  ");
            }
            catch (Exception ex)
            {
                isSucces = false;
                MessageBox.Show(ex.ToString());
            }

            return isSucces;
        }

        public void createTable(Document document)
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

            row = table.AddRow();
            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[2].Shading.Color = Colors.LightGray;
            row.Cells[3].Shading.Color = Colors.LightGray;
            row.Cells[6].Shading.Color = Colors.LightGray;
            row.Cells[7].Shading.Color = Colors.LightGray;
            row.Cells[0].AddParagraph("SALA KONFERENCYJNA:");

            createRowForGastronomic(document, 4, table, column);
            row = table.AddRow();
            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].AddParagraph("PODSUMOWANIE");
            row.Cells[1].MergeRight = 4;

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

            createRowForGastronomic(document,4,table, column);

            row = table.AddRow();
            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].AddParagraph("PODSUMOWANIE");
            row.Cells[1].MergeRight = 4;

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

            createRowForGastronomic(document, 4, table, column);

            row = table.AddRow();
            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].AddParagraph("PODSUMOWANIE");
            row.Cells[1].MergeRight = 4;

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

            row = table.AddRow();
            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[2].Shading.Color = Colors.LightGray;
            row.Cells[3].Shading.Color = Colors.LightGray;
            row.Cells[6].Shading.Color = Colors.LightGray;
            row.Cells[7].Shading.Color = Colors.LightGray;
            row.Cells[0].AddParagraph("PARKING ( część nie dozorowana ):");

            row = table.AddRow();
            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[2].Shading.Color = Colors.LightGray;
            row.Cells[3].Shading.Color = Colors.LightGray;
            row.Cells[6].Shading.Color = Colors.LightGray;
            row.Cells[7].Shading.Color = Colors.LightGray;
            row.Cells[0].AddParagraph("PARKING ( część dozorowana) ");

            createRowForGastronomic(document, 1, table, column);

            row = table.AddRow();
            row.Cells[0].Shading.Color = Colors.LightGray;
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].AddParagraph("PODSUMOWANIE");
            row.Cells[1].MergeRight = 4;

            createTableForPaymanet(document);


        }

        public void createTableForPaymanet(Document document)
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
                row.Cells[0].AddParagraph("FORMA PŁATNOŚCI:");
                row.Cells[1].AddParagraph("50 % zadatek / 50 % do dnia realizacji");
                row.Cells[2].AddParagraph("WARTOŚĆ ZAMÓWIENIA:");
               
                row = table.AddRow();
                row.Cells[2].MergeDown = 3;
                row.Cells[3].MergeDown = 3;
                row.Cells[4].MergeDown = 3;
                row.Cells[2].MergeRight = 2;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("NAZWA USŁUGI NA FAKTURZE:");
                row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[2].Format.Font.Bold = true;
                row.Cells[2].Format.Font.Size = 6;
                row.Cells[2].AddParagraph("W przypadku zlecenia podpis osoby upoważnionej, data i pieczęć firmy:");

                row = table.AddRow();
                row.Cells[2].MergeRight = 2;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("ZAMÓWIENIA INDYWIDUALNE:");

                row = table.AddRow();
                row.Cells[2].MergeRight = 2;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("PARKING:");

                row = table.AddRow();
                row.Cells[2].MergeRight = 2;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("OPIS SALI/STOŁÓW:");

                row = table.AddRow();
                row.Cells[0].MergeRight = 3;
                row.Cells[0].Format.Font.Size = 7;
                row.Cells[0].AddParagraph("Zgodnie z treścią art. 71 Kodeksu Cywilnego niniejsza propozycja cenowa nie stanowi oferty handlowej ma jedynie charakter informacyjny. Informacja  jest ważna do Po upływie tego terminu lub/i w przypadku zmniejszenia ilości zarezerwowanych pokoi lub/i zmniejszenia ilości uczestników spotkania, Hotel zastrzega sobie prawo do zmiany proponowanych cen lub wycofania tych cen. Po upływie terminu jw. Hotel także nie gwarantuje dostępności pokoi i sal konferencyjnych. W przypadku zainteresowania terminem Państwa zapytania (niniejszej oferty) ze strony innego klienta, poprosimy o potwierdzenie Państwa rezerwacji i zawarcie umowy w ciągu 24 godzin lub dokonanie rezerwacji gwaratowanej bez możliwości anulacji bezkosztowej.");
                row.Cells[4].AddParagraph("jakas data");
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
        public void createRowForGastronomic(Document document, int howMany, Table table, Column column)
        {
            for (int i = 0; i < howMany; i++)
            {
                //Create table columns
                table.Rows.Height = 3;
                Row row = table.AddRow();
                row.Borders.Top.Visible = false;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[3].Shading.Color = Colors.LightGray;
                row.Cells[6].Shading.Color = Colors.LightGray;
                row.Cells[7].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("DOTYCZY:");
                
            }
        }

        public void createPdf()
        {
            try
            {
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
                row.Cells[3].MergeDown = 1; // merge down third cell with next one
                row.Cells[1].AddParagraph("Park Hotel Jakis ****");
                row.Cells[1].AddParagraph("ul. Zwycięstwa 122a");
                row.Cells[1].AddParagraph("77-777 Warszawa");
                row.Cells[1].AddParagraph("77-777 Warszawa");
                row.Cells[2].AddParagraph("DLA:");

                row = table.AddRow();
                row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Top;
                row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Top;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("ADRES:");
                row.Cells[2].AddParagraph("ADRES:");

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("NIP:");
                row.Cells[2].AddParagraph("NIP:");

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("DATA AKTUALIZACJI:");
                row.Cells[2].AddParagraph("TERMIN WAŻNOŚCI PROPOZYCJI:");

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
                row.Cells[0].AddParagraph("Specjalista ds. sprzedaży:");
                row.Cells[2].AddParagraph("Sz.P.:");

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("TELEFON:");
                row.Cells[2].AddParagraph("TELEFON:");

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("ADRES E-MAIL:");
                row.Cells[2].AddParagraph("ADRES E-MAIL:");

                row = table.AddRow();
                row.Cells[0].MergeRight = 1;
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[1].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[2].AddParagraph("OSOBA DECYZYJNA:");

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
                row.Cells[0].AddParagraph("TERMIN:");
                row.Cells[2].AddParagraph("ILOŚĆ OSÓB:");

                row = table.AddRow();
                row.Cells[0].Shading.Color = Colors.LightGray;
                row.Cells[2].Shading.Color = Colors.LightGray;
                row.Cells[0].AddParagraph("CZAS TRWANIA:");
                row.Cells[2].AddParagraph("USTAWIENIE SALI:");

                createTable(document);

                savePdf(document);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
