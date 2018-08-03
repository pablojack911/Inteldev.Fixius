using System.Windows;
using System.Windows.Controls;

namespace Inteldev.Fixius.Proveedores.Compras.OrdenDeCompra
{
    /// <summary>
    /// Interaction logic for VistaProveedorNuevoOrdenDeCompra.xaml
    /// </summary>
    public partial class VistaProveedorNuevoOrdenDeCompra : UserControl
    {
        public VistaProveedorNuevoOrdenDeCompra()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.mbProveedor.Campo.Focus();
        }
    }
}
