using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.DTO.Fiscal;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Inteldev.Fixius.Articulos.Maestro
{

    public partial class VistaArticulo : UserControl
    {
        public VistaArticulo()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Descripcion.Campo.Focus();
        }

        //private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (Basico.IsSelected)
        //        Keyboard.Focus(Descripcion.Campo);
        //    if (Agrupacion.IsSelected)
        //        Keyboard.Focus(Descripcion.Campo);
        //    if (Logistica.IsSelected)
        //        Keyboard.Focus(Envase.Campo);
        //    if (Fiscales.IsSelected)
        //        Keyboard.Focus(ObservacionesArticulos.Campo);
        //    if (DatosOld.IsSelected)
        //        Keyboard.Focus(Linea.Campo);
        //}
    }
}
