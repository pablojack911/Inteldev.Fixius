using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Inteldev.Fixius.Mapas
{
    public static class PrintManager
    {
        public static void ReporteDePosiciones(string CodigoVendedor, string NombreVendedor, string FechaDelReporte, List<ItemReporte> Posiciones)
        {
            var table1 = new Table();

            // Set some global formatting properties for the table.
            table1.CellSpacing = 10;
            table1.Background = Brushes.White;

            // Create 6 columns and add them to the table's Columns collection.
            //int numberOfColumns = 6;
            //for (int x = 0; x < numberOfColumns; x++)
            //{
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //CLIENTE
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //CHECKIN
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //CHECKOUT
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //TIEMPO
            //table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //1ER CLIENTE VISITADO
            //table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //1ER CLIENTE DE RUTA

            //}
            // Create and add an empty TableRowGroup to hold the table's Rows.
            table1.RowGroups.Add(new TableRowGroup());

            //TITULO
            table1.RowGroups[0].Rows.Add(new TableRow());
            // Alias the current working row for easy reference.
            TableRow currentRow = table1.RowGroups[0].Rows[0];
            // Global formatting for the title row.
            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 25;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = System.Windows.FontWeights.Bold;
            // Add the header row with content, 
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("REPORTE DE POSICIONES"))) { TextAlignment = TextAlignment.Center }); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = 4;

            //SUBTITULO
            // Add the second (header) row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[1];
            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(CodigoVendedor + " - " + NombreVendedor)))); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = 4;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[2];
            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FechaDelReporte)))); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = 4;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[3];
            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("CLIENTE"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("CHECK IN"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("CHECK OUT"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("TIEMPO"))) { TextAlignment = TextAlignment.Center });

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[4];

            string cli = "";
            string checkin = "";
            string checkout = "";
            string tiempo = "";

            for (int i = 0; i < Posiciones.Count; i++)
            {
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[i + 4];
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;
                currentRow.FontFamily = new FontFamily("Sans Serif");
                cli = Posiciones[i].Cliente;
                checkin = Posiciones[i].CheckIn.TimeOfDay.ToString(@"hh\:mm");
                checkout = Posiciones[i].CheckOut.TimeOfDay.ToString(@"hh\:mm");
                tiempo = Posiciones[i].Tiempo;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(cli))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(checkin))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(checkout))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(tiempo))) { TextAlignment = TextAlignment.Center });
            }

            var fd = new FlowDocument(table1);

            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() != true)
                return;

            fd.PageHeight = p.PrintableAreaHeight;
            fd.PageWidth = p.PrintableAreaWidth;
            fd.PagePadding = new Thickness(25);

            fd.ColumnGap = 0;

            fd.ColumnWidth = (fd.PageWidth -
                           fd.ColumnGap -
                           fd.PagePadding.Left -
                           fd.PagePadding.Right);

            p.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "Imprimiendo...");
        }
    }
}
