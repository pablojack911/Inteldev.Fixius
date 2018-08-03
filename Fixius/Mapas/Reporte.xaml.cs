using ExportToExcel;
using Inteldev.Core.Presentacion;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Inteldev.Fixius.Mapas
{
    /// <summary>
    /// Interaction logic for Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {



        public string CodigoVendedor
        {
            get { return (string)GetValue(CodigoVendedorProperty); }
            set { SetValue(CodigoVendedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CodigoVendedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CodigoVendedorProperty =
            DependencyProperty.Register("CodigoVendedor", typeof(string), typeof(Reporte));

        public string NombreVendedor
        {
            get { return (string)GetValue(NombreVendedorProperty); }
            set { SetValue(NombreVendedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NombreVendedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NombreVendedorProperty =
            DependencyProperty.Register("NombreVendedor", typeof(string), typeof(Reporte));

        public string FechaDelReporte
        {
            get { return (string)GetValue(FechaDelReporteProperty); }
            set { SetValue(FechaDelReporteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FechaDelReporte.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FechaDelReporteProperty =
            DependencyProperty.Register("FechaDelReporte", typeof(string), typeof(Reporte));

        public ObservableCollection<ItemReporte> Posiciones
        {
            get { return (ObservableCollection<ItemReporte>)GetValue(PosicionesProperty); }
            set { SetValue(PosicionesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Posiciones.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PosicionesProperty =
            DependencyProperty.Register("Posiciones", typeof(ObservableCollection<ItemReporte>), typeof(Reporte));


        public Reporte()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintManager.Imprimir(this.CodigoVendedor, this.NombreVendedor, this.FechaDelReporte, this.Posiciones.ToList());
            this.Close();
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Libro de Excel|.xlsx";
                saveFileDialog1.Title = "Exportar a Excel";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    ExcelPackage ep = new ExcelPackage(new FileInfo(saveFileDialog1.FileName));
                    ExcelWorksheet ws = ep.Workbook.Worksheets["Sheet1"];

                    List<string> domains = new List<string>();
                    for (int rw = 1; rw <= ws.Dimension.End.Row; rw++)
                    {
                        if (ws.Cells[rw, 1].Value != null)
                            domains.Add(ws.Cells[rw, 1].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Mensajes.Aviso(ex.Message);
            }
        }

        private List<String> CreaString()
        {
            var lista = new List<String>();
            foreach (var p in this.Posiciones)
            {
                lista.Add(p.Cliente + " | " + p.CheckIn.ToString("h:mm tt") + " | " + p.CheckOut.ToString("h:mm tt") + " | " + p.Tiempo);
            }
            //var check = item.Posiciones.FirstOrDefault(r => r.Estado == Estado.CHECKIN_CLIENTE);
            return lista;
            //System.IO.File.WriteAllText(@"d:\" + item.Codigo + ".txt", sb.ToString());
        }
    }
}
