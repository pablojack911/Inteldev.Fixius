using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;

using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
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

namespace Inteldev.Fixius.Proveedores.Compras.ObjetivosDeCompra
{
    /// <summary>
    /// Interaction logic for ObjetivosDeCompra.xaml
    /// </summary>
    public partial class ItemObjetivoDeCompra : UserControl
    {
        public ItemObjetivoDeCompra()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.PrimerCampo.TextFecha.Focus();
        }
    }
}
