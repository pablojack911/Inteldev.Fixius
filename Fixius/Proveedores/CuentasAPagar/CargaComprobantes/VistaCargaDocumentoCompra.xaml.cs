using System.Windows;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Proveedores.CuenasAPagar.CargaComprobantes;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Contratos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion;
using System;

namespace Inteldev.Fixius.Proveedores.CuentasAPagar.CargaComprobantes
{
    /// <summary>
    /// Interaction logic for VistaCargaDocumentoCompra.xaml
    /// </summary>
    /// 

    public partial class VistaCargaDocumentoCompra : Window
    {

        public VistaCargaDocumentoCompra()
        {
            InitializeComponent();
            this.DataContext = new VMDocumentoCompra();
            this.Cabecera.empresa.Campo.Focus();
            this.Cabecera.TipoDocumento.Excluir = new string[] { "OrdenDePago", "LiquidacionBancaria", "NotaDeCreditoInterno", "NotadeDébitoInterno" };
            this.btnGrabar.Click += btnGrabar_Click;
            this.Cabecera.btnBuscarOGrabar.Click += btnBuscarOGrabar_Click;
            //var ventana = new BaseVentanaDialogo();
            //var vista = FabricaVistas.Instancia.BuscaVista(typeof(DocumentoCompra));
            //ventana.DataContext = new VMCabeceraDocumentoDeCompra(new DocumentoCompra());
            //ventana.Content = Activator.CreateInstance(vista);
            //ventana.ShowDialog();
        }

        void btnBuscarOGrabar_Click(object sender, RoutedEventArgs e)
        {
            this.fechaIngreso.TextFecha.Focus();
        }

        void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            this.btnGrabar.Command.Execute(0);
            this.Cabecera.empresa.Campo.Focus();
        }
    }
}
