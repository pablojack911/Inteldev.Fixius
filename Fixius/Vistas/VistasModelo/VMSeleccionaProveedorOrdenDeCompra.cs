using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
	public class VMSeleccionaProveedorOrdenDeCompra : VistaModeloBaseDialogo<ProveedorNuevo>
	{
		#region DP's

		public IPresentadorMiniBusca<Proveedor> PresentadorMiniBuscaProveedor
		{
			get { return (IPresentadorMiniBusca<Proveedor>)GetValue(PresentadorMiniBuscaProveedorProperty); }
			set { SetValue(PresentadorMiniBuscaProveedorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorMiniBuscaProveedorProperty =
			DependencyProperty.Register("PresentadorMiniBuscaProveedor", typeof(IPresentadorMiniBusca<Proveedor>), typeof(VMSeleccionaProveedor));


		public IPresentadorBusquedaArticulo PresentadorBusquedaArticulo
		{
			get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorBusquedaArticuloProperty); }
			set { SetValue(PresentadorBusquedaArticuloProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorBusquedaArticulo.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorBusquedaArticuloProperty =
			DependencyProperty.Register("PresentadorBusquedaArticulo", typeof(IPresentadorBusquedaArticulo), typeof(VMSeleccionaProveedorOrdenDeCompra));

		#endregion


		public VMSeleccionaProveedorOrdenDeCompra( )
			: base()
		{
			if (this.EntidadActual == null)
			{
				this.EntidadActual = new ProveedorNuevo();
			}
			this.SetPresentador<Proveedor>("PresentadorMiniBuscaProveedor", p => this.EntidadActual.Proveedor = p, this.EntidadActual.Proveedor);
			this.PresentadorBusquedaArticulo = (IPresentadorBusquedaArticulo)FabricaPresentadores.Instancia.Resolver(typeof(IPresentadorBusquedaArticulo));
			this.PresentadorBusquedaArticulo.SetServicios();
			this.PresentadorBusquedaArticulo.CargaArea();
		}
	}
}
