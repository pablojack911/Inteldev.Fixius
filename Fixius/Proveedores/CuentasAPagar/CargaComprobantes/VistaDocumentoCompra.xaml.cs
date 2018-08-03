using System;
using System.Collections.Generic;
using System.Data;
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

namespace Inteldev.Fixius.Proveedores.CuenasAPagar.CargaComprobantes
{
    /// <summary>
    /// Interaction logic for VistaComprobanteDeCompra.xaml
    /// </summary>
    public partial class VistaDocumentoCompra : UserControl
    {
        public VistaDocumentoCompra()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            this.fechaIngreso.TextFecha.Focus();
        }

     
    }
}
