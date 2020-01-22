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
    public class BodegaPDF
    {
        public void generatePDF(String dest, Chart carrito, List<Items> items)
        {

            Double totalSaldo = 0.0D;
            Decimal totalVencer = 0.0M;
            Decimal total030 = 0.0M;
            Decimal total3060 = 0.0M;
            Decimal total6090 = 0.0M;
            Decimal total90 = 0.0M;
            Decimal totalVencido = 0.0M;

            PdfWriter pdfWriter = null;
            pdfWriter = new PdfWriter(dest);
            PdfDocument pdfDoc = new PdfDocument(pdfWriter);
            Document doc = new Document(pdfDoc, new PageSize(595, 842));

            PdfFont BOLD = PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);
            PdfFont COURIER = PdfFontFactory.CreateFont(StandardFonts.COURIER);
            doc.SetFont(COURIER);
            doc.SetFontSize(8f);
            doc.SetMargins(15, 20, 15, 20);
            float fntSize;
            fntSize = 28f;

            String nombreCliente = carrito.nombre_cliente;
            String codigoCliente = carrito.codigo_cliente;

            doc.Add(new Paragraph("Pedido Cliente: " + carrito.nombre_cliente).SetFont(BOLD).SetFontSize(fntSize));
            doc.Add(new Paragraph(""));
            doc.Add(new Paragraph("CORSENESA"));
            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("M/d/yyyy").ToString()).SetFixedLeading(2));
            doc.Add(new Paragraph("Nombre Cliente: " + nombreCliente).SetFixedLeading(2));
            doc.Add(new Paragraph("Codigo Cliente: " + codigoCliente).SetFixedLeading(2));
            doc.Add(new Paragraph("Codigo Vendedor: " + carrito.cod_vendedor).SetFixedLeading(2));
            doc.Add(new Paragraph(""));
            Table table = new Table(new float[6]).UseAllAvailableWidth();
            table.SetMarginTop(0);
            table.SetMarginBottom(0);

            Cell cell = new Cell(1, 6).Add(new Paragraph("Pedido Cliente " + nombreCliente));
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetPadding(5);
            cell.SetBackgroundColor(new DeviceRgb(112, 128, 144));
            table.AddCell(cell);

            table.AddCell("#").SetTextAlignment(TextAlignment.CENTER); 
            table.AddCell("Empresa").SetTextAlignment(TextAlignment.CENTER);

            table.AddCell("Cantidad").SetTextAlignment(TextAlignment.CENTER);
            table.AddCell("Codigo").SetTextAlignment(TextAlignment.CENTER); 
            table.AddCell("Nombre producto").SetTextAlignment(TextAlignment.CENTER); 
            table.AddCell("Precio venta").SetTextAlignment(TextAlignment.CENTER);
            

            int i = 0;
            foreach (Items item in items)
            {
                i++;
                table.AddCell((new Paragraph(i.ToString()).SetTextAlignment(TextAlignment.CENTER)));
                table.AddCell((new Paragraph(item.producto.Empresa)).SetTextAlignment(TextAlignment.CENTER));
                table.AddCell((new Paragraph(item.cantidad.ToString())).SetTextAlignment(TextAlignment.RIGHT));
                table.AddCell((new Paragraph(item.producto.Codigo)).SetTextAlignment(TextAlignment.CENTER));
                table.AddCell((new Paragraph(item.producto.nombre)).SetTextAlignment(TextAlignment.CENTER));
                table.AddCell((new Paragraph("Q." + item.producto.prec_vta1.ToString("0.00"))).SetTextAlignment(TextAlignment.RIGHT));
                
              
            }

            doc.Add(table);    
            doc.Flush();
            doc.Close();
            pdfDoc.Close();
            pdfWriter.Close();
            pdfWriter.Dispose();
        }
    }
}