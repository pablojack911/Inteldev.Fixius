
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Inteldev.Fixius.Proveedores.Compras.ListaDePrecios
{
    /// <summary>
    /// Interaction logic for VistaListaDePreciosProveedor.xaml
    /// </summary>
    public partial class VistaListaDePreciosProveedor : UserControl
    {
        public VistaListaDePreciosProveedor()
        {
            InitializeComponent();

        }





        //ListaDePrecios Lista;
        //private void cargar()
        //{
        //Lista = this.DataContext as ListaDePrecios;
        //this.Controlador = new Controladores.ControladorPlantillaListaPrecio(this.Lista.Columnas);
        //this.datagridPrecios.ItemsSource = Lista.Detalle.DefaultView;

        //}

        //void Detalle_RowChanged(object sender, DataRowChangeEventArgs e)
        //{
        //    if (e.Action == DataRowAction.Change)
        //        e.Row.SetModified();

        //}


        int index = -2;
        private void datagridPrecios_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var Lista = this.DataContext as VMListaProveedor;

            if (e.PropertyName == "Id")
            {
                e.Cancel = true;
            }
            else
            {
                if (index >= 0)
                {
                    var columna = Lista.Modelo.Columnas.FirstOrDefault(c => c.Orden == index);
                    e.Column.CellStyle = Lista.ControladorPlantilla.ObtenerStyleParaColumna(columna);

                    if (columna.TipoColumna == TipoColumna.SubTotal ||
                           columna.TipoColumna == TipoColumna.Costo ||
                           columna.TipoColumna == TipoColumna.Final)
                    {
                        var exp = Lista.ControladorPlantilla.ObtenerExpresion(columna);
                        Lista.Modelo.Detalle.Columns[index + 2].Expression = exp;
                        e.Column.IsReadOnly = true;

                    }

                }
                else
                {
                    e.Column.IsReadOnly = true;
                }
            }
            index++;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    var tabla = this.Lista.Detalle;
        //    foreach (DataRow item in tabla.Rows)
        //    {
        //        if (item.RowState== DataRowState.Modified)
        //            MessageBox.Show(item[2].ToString());
        //    }


        //}
    }
}
