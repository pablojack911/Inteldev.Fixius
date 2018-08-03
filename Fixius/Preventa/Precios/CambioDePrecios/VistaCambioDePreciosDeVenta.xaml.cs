using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inteldev.Fixius.Preventa.Precios.CambioDePrecios
{
    /// <summary>
    /// Interaction logic for VistaCambioDePreciosDeVenta.xaml
    /// </summary>
    public partial class VistaCambioDePreciosDeVenta : UserControl
    {
        public VistaCambioDePreciosDeVenta()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //if (e.PropertyName!="Codigo" && e.PropertyName!="Articulo" && e.Column is DataGridTextColumn)
            if (e.PropertyType == typeof(decimal) && e.Column is DataGridTextColumn)
            {
                var dgcol = e.Column as DataGridTextColumn;

                dgcol.Binding.StringFormat = "{0:0.000}";

                dgcol.CellStyle = this.FindResource("RightCellStyle") as Style;




            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }
    }
}
