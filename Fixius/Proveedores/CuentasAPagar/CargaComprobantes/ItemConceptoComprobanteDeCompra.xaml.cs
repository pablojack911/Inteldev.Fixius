using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
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

namespace Inteldev.Fixius.Proveedores.CuenasAPagar.CargaComprobantes
{
    /// <summary>
    /// Interaction logic for ItemConceptoComprobanteDeCompra.xaml
    /// </summary>
    public partial class ItemConceptoComprobanteDeCompra : UserControl
    {
        public ItemConceptoComprobanteDeCompra()
        {
            InitializeComponent();
            string[] excluyentes = { "IvaTasaGeneral", "IvaTasaReducida", "IvaTasaDiferencial", "Final" };
            this.comboTipoConcepto.Excluir = excluyentes;
            string[] excluyentesExtra = { "Neto", "Exento", "Final", "ImpuestoInterno", "PercepcionIva", "PercepcionIIBB", "Final" };
            this.comboTipoConceptoExtra.Excluir = excluyentesExtra;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboTipoConcepto.Combo.Focus();
        }

    }
}
