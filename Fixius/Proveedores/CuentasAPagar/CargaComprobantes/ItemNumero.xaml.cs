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

namespace Inteldev.Fixius.Proveedor.CuenasAPagar.CargaComprobantes
{
    /// <summary>
    /// Interaction logic for PreVistaComprobanteDeCompra.xaml
    /// </summary>
    public partial class ItemNumero : UserControl
    {
        public ItemNumero()
        {
            InitializeComponent();
            this.PosNumero.GridColumnTexto = 0;
        }
        //public PreVistaComprobanteDeCompra()
        //{
        //    InitializeComponent();
        //    Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        //    this.comboProveedor.Texto.Focus();
        //    string[] excluyentes = {"OrdenDePago"};
        //    this.tipos.Excluir = excluyentes;
        //}

        //private void UserControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.comboProveedor.Campo.Focus();
        //}
    }
}
