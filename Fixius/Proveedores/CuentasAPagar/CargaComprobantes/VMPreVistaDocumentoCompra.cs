using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Proveedor.CuenasAPagar.CargaComprobantes
{
    public class VMPreVistaDocumentoCompra : VistaModeloBaseDialogo<DocumentoCompra>
    {
        public IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor> PresentadorMiniBuscaProveedor
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor>)GetValue(PresentadorMiniBuscaProveedorProperty); }
            set { SetValue(PresentadorMiniBuscaProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaProveedorProperty =
            DependencyProperty.Register("PresentadorMiniBuscaProveedor", typeof(IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor>), typeof(VMPreVistaDocumentoCompra));

        public VMPreVistaDocumentoCompra() : base() 
        {
            if (this.EntidadActual == null)
            {
                this.EntidadActual = new DocumentoCompra();
                this.Modelo = this.EntidadActual;
            }
            this.SetPresentador<Servicios.DTO.Proveedores.Proveedor>("PresentadorMiniBuscaProveedor", p => this.EntidadActual.Proveedor = p, this.EntidadActual.Proveedor);
            this.Modelo.Empresa = Sistema.Instancia.EmpresaActual;
            this.PresentadorMiniBuscaProveedor.cantidadNumeros = 5;
        }

        public override int[] ObtenerIds()
        {
            //int[] args = {(int)this.EntidadActual.Empresa.CondicionAnteIVA, this.Modelo.Proveedor.Id, (int)this.Modelo.TipoDocumento,this.Modelo.Numero,this.Modelo.pPrenumero,this.Modelo.Empresa.Id};

            //this.Modelo.Empresa = Sistema.Instancia.EmpresaActual;
            int[] args = { (int)Servicios.DTO.Fiscal.CondicionAnteIva.ResponsableInscripto, this.Modelo.Proveedor.Id, (int)this.Modelo.TipoDocumento, this.Modelo.Numero, this.Modelo.Prenumero, this.Modelo.Empresa.Id };
            return args;
        }
        
    }
}
