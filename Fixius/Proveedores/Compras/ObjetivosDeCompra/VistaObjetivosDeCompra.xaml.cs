using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
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
using System.Windows.Threading;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;

using Inteldev.Core.Presentacion.Presentadores;

namespace Inteldev.Fixius.Proveedores.Compras.ObjetivosDeCompra
{
    /// <summary>
    /// Interaction logic for VistaObjetivosDeCompra.xaml
    /// </summary>
    public partial class VistaObjetivosDeCompra : UserControl
    {
        public VistaObjetivosDeCompra()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => this.presenta()));
        }

        private object presenta()
        {
            var presentador = this.objetivosDeCompra.Presentador as IPresentadorGrilla<Servicios.DTO.Proveedores.ObjetivosDeCompra, Objetivos, ItemObjetivoDeCompra>;
            //presentador.quitarColumnasIdNombre(this.objetivosDeCompra, typeof(Objetivos));
            return true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }
    }
}
