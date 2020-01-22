using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using promovil_rest.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace promovil_rest.Clases
{
    public class GeneratePDF
    {



        public void ManipulatePdf(String dest, List<EstadoCuenta> estado)
        {

            Double totalSaldo = 0.0D;
            Decimal totalVencer = 0.0M;
            Decimal total030 = 0.0M;
            Decimal total3060 = 0.0M;
            Decimal total6090 = 0.0M;
            Decimal total90 = 0.0M;
            Decimal totalVencido = 0.0M;
            PdfWriter pdfWriter = null;


           /* try
            {
                pdfWriter = new PdfWriter(dest);
            }
            catch (IOException ex)
            {

                pdfWriter.SetCloseStream(false);
            }*/

            pdfWriter = new PdfWriter(dest);

            PdfDocument pdfDoc = new PdfDocument(pdfWriter);
            Document doc = new Document(pdfDoc, new PageSize(595, 842));

            PdfFont BOLD = PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);
            PdfFont COURIER = PdfFontFactory.CreateFont(StandardFonts.COURIER);
            doc.SetFont(COURIER);
            doc.SetFontSize(8f);
            doc.SetMargins(15, 20, 15, 20);

            float fntSize;
            fntSize = 30f;
         
            Text cuentas = new Text("Cuentas por Cobrar").SetFont(BOLD);
            string strcuentas = cuentas.ToString();
            doc.Add(new Paragraph("Cuentas por Cobrar").SetFont(BOLD).SetFontSize(fntSize));
            doc.Add(new Paragraph(""));
            doc.Add(new Paragraph(""));
            doc.Add(new Paragraph("CORSENESA"));
            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("M/d/yyyy").ToString()));
            doc.Add(new Paragraph("Nombre Cliente: " + estado[0].NombreCliente));
            doc.Add(new Paragraph("Codigo Cliente: " + estado[0].Cliente.ToString()));

            Table table = new Table(new float[11]).UseAllAvailableWidth();
            table.SetMarginTop(0);
            table.SetMarginBottom(0);

            // first row
            Cell cell = new Cell(1, 11).Add(new Paragraph("Estado de cuenta Cliente"));
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetPadding(5);
            cell.SetBackgroundColor(new DeviceRgb(112, 128, 144));
            table.AddCell(cell);


            table.AddCell("Tipo Num Doc.");
            table.AddCell("Emisión");
            table.AddCell("Vencimiento");
            table.AddCell("Paciente");
            table.AddCell("Monto");
            table.AddCell("Saldo por vencer");
            table.AddCell("(0 a 30)");
            table.AddCell("(31 a 60)");
            table.AddCell("(61 a 90)");
            table.AddCell("(90+)");
            table.AddCell("Vencido");

            foreach (EstadoCuenta ec in estado)
            {
                //    String monto = String.Parse(ec.SaldoTotal);
                table.AddCell(ec.TipoDocumento + "\n\r" + ec.NumeroDocumentos).SetTextAlignment(TextAlignment.LEFT); 
                /*  string emision = ec.FechaEmision.ToString();
                  emision = DateTime.Now.ToString("dd.MM.yyyy");

                  string vencimiento = ec.FechaEmision.ToString();
                  emision = DateTime.Now.ToString("dd.MM.yyyy");*/

                table.AddCell(ec.FechaEmision.Date.ToString()).SetTextAlignment(TextAlignment.LEFT);
                table.AddCell(ec.FechaVencimiento.Date.ToString()).SetTextAlignment(TextAlignment.LEFT);

                table.AddCell(ec.Paciente);
                table.AddCell(new Paragraph("Q." + ec.SaldoTotal.ToString("0.00")).SetTextAlignment(TextAlignment.RIGHT)); 
                table.AddCell(new Paragraph("Q." + ec.PORVENCER.ToString("0.00")).SetTextAlignment(TextAlignment.RIGHT)); 
                table.AddCell(new Paragraph("Q." + ec.V030.ToString("0.00")).SetTextAlignment(TextAlignment.RIGHT)); 
                table.AddCell(new Paragraph("Q." + ec.V3160.ToString("0.00")).SetTextAlignment(TextAlignment.RIGHT)); 
                table.AddCell(new Paragraph("Q." + ec.V6190.ToString("0.00")).SetTextAlignment(TextAlignment.RIGHT)); 
                table.AddCell(new Paragraph("Q." + ec.M90.ToString("0.00")).SetTextAlignment(TextAlignment.RIGHT)); 
                table.AddCell(new Paragraph("Q." + ec.Vencido.ToString("0.00")).SetTextAlignment(TextAlignment.RIGHT)); 

                totalSaldo = totalSaldo + ec.SaldoTotal;
                totalVencer = totalVencer + ec.PORVENCER;
                total030 = total030 + ec.V030;
                total3060 = total3060 + ec.V3160;
                total6090 = total6090 + ec.V6190;
                total90 = total90 + ec.M90;
                totalVencido = totalVencido + ec.Vencido;
            }
            Text second = new Text("Total de cliente").SetFont(BOLD);
            Cell cell23 = new Cell(2, 4).Add(new Paragraph(second));
            table.AddCell(cell23.SetTextAlignment(TextAlignment.LEFT));

            Text saldo = new Text("Q." + totalSaldo.ToString("0.00")).SetFont(BOLD);
            Text vencer = new Text("Q." + totalVencer.ToString("0.00")).SetFont(BOLD);
            Text credA = new Text("Q." + total030.ToString("0.00")).SetFont(BOLD);
            Text credB = new Text("Q." + total3060.ToString("0.00")).SetFont(BOLD);
            Text credC = new Text("Q." + total6090.ToString("0.00")).SetFont(BOLD);
            Text credD = new Text("Q." + total90.ToString("0.00")).SetFont(BOLD);
            Text vencido = new Text("Q." + totalVencido.ToString("0.00")).SetFont(BOLD);


            table.AddCell(new Paragraph(saldo).SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell(new Paragraph(vencer).SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell(new Paragraph(credA).SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell(new Paragraph(credB).SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell(new Paragraph(credC).SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell(new Paragraph(credD).SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell(new Paragraph(vencido).SetTextAlignment(TextAlignment.RIGHT));


            doc.Add(table);

            doc.Flush();
           
            doc.Close();
            pdfDoc.Close();

            pdfWriter.Close();
            pdfWriter.Dispose();           
        }
    }
}