using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.DTO.Fiscal;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections;
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
using Inteldev.Fixius.Servicios.DTO.Financiero;
using System.Diagnostics;
using Inteldev.Core.Extenciones;
using Inteldev.Core.Presentacion.Controles;

namespace Inteldev.Fixius.Proveedores.MaestroProveedor
{
    /// <summary>
    /// Interaction logic for VistaProveedor.xaml
    /// </summary>
    public partial class VistaProveedor : UserControl
    {
        public VistaProveedor()
        {
            InitializeComponent();
            string[] excluyentes = { "ConsumidorFinal" };
            this.CondicionAnteIVA.Excluir = excluyentes;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //this.txtItemCodigo.Campo.Focus();
            this.txtCodigoHistoricoPreventa.Campo.Focus();
            //var d = (VMProveedor)this.DataContext;
            //this.comboTipoProveedor.SelectedValue = d.Modelo.TipoProveedor;

        }

        private void ItemFormulario_LostFocus(object sender, RoutedEventArgs e)
        {
            var t = (ItemFormulario)sender;
            if (t.Valor != null)
            {
                if (t.Valor.ToString().Trim().Equals(string.Empty))
                    t.Valor = string.Empty;
                else
                    t.Valor = t.Valor.ToString().Trim().PadLeft(5, '0');
            }
        }





    }
}
