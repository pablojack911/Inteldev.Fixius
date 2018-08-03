using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Fixius.Proveedores.CuentasAPagar.CargaComprobantes
{
    public class VMCabeceraDocumentoDeCompra : VistaModeloBase<DocumentoCompra>
    {
        public ICommand BotonBuscaDocumentoCompra { get; set; }

        public IPresentadorMiniBusca<Proveedor> PresentadorProveedor
        {
            get { return (IPresentadorMiniBusca<Proveedor>)GetValue(PresentadorProveedorProperty); }
            set { SetValue(PresentadorProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProveedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProveedorProperty =
            DependencyProperty.Register("PresentadorProveedor", typeof(IPresentadorMiniBusca<Proveedor>), typeof(VMCabeceraDocumentoDeCompra));

        public IPresentadorMiniBusca<Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Empresa>), typeof(VMCabeceraDocumentoDeCompra));

        public VMCabeceraDocumentoDeCompra()
        {
            this.InstanciarPresentador("PresentadorEmpresa");
            this.PresentadorEmpresa.cantidadNumeros = 2;

            this.InstanciarPresentador("PresentadorProveedor");
            this.PresentadorProveedor.cantidadNumeros = 5;
            this.BotonBuscaDocumentoCompra = new RelayCommand(p => TryCatch.Intentar(x => this.BuscarDocumentoDeCompraOCrearNuevo()), x => this.PuedeBuscarDocumentoDeCompra());

        }
        public VMCabeceraDocumentoDeCompra(DocumentoCompra DTO)
            : base(DTO)
        {
            //this.SetPresentador<Empresa>("PresentadorEmpresa", p => DTO.Empresa = p, DTO.Empresa);
            //this.PresentadorEmpresa.cantidadNumeros = 2;
            //this.SetPresentador<Proveedor>("PresentadorProveedor", p => DTO.Proveedor = p, DTO.Proveedor);
            //this.PresentadorProveedor.cantidadNumeros = 5;
        }

        private bool PuedeBuscarDocumentoDeCompra()
        {
            return true;
        }

        private object BuscarDocumentoDeCompraOCrearNuevo()
        {
            //var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<DocumentoCompra>>();
            //var nuevoDoc = servicio.CrearConParametros(Sistema.Instancia.EmpresaActual.Codigo, new int[] { 0, 0, 0, 0, 0 });
            //this.DataContext = new VMDocumentoCompra(nuevoDoc.GetEntidad());
            return null;
        }
    }
}
