using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.Proveedores.Compras.ObjetivosDeCompra
{
    public class VMObjetivosDeCompra : VistaModeloBase<Servicios.DTO.Proveedores.ObjetivosDeCompra>
	{
		#region DP's

        public IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor> PresentadorProveedor
		{
            get { return (IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor>)GetValue(PresentadorProveedorProperty); }
			set { SetValue(PresentadorProveedorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorProveedor.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorProveedorProperty =
            DependencyProperty.Register("PresentadorProveedor", typeof(IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor>), typeof(VMObjetivosDeCompra));



        public IPresentadorGrilla<Servicios.DTO.Proveedores.ObjetivosDeCompra, Objetivos, ItemObjetivoDeCompra> PresentadorObjetivos
		{
            get { return (IPresentadorGrilla<Servicios.DTO.Proveedores.ObjetivosDeCompra, Objetivos, ItemObjetivoDeCompra>)GetValue(PresentadorObjetivosProperty); }
			set { SetValue(PresentadorObjetivosProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorObjetivos.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorObjetivosProperty =
            DependencyProperty.Register("PresentadorObjetivos", typeof(IPresentadorGrilla<Servicios.DTO.Proveedores.ObjetivosDeCompra, Objetivos, ItemObjetivoDeCompra>), typeof(VMObjetivosDeCompra));		

		#endregion

		public VMObjetivosDeCompra( ) : base() { }

        public VMObjetivosDeCompra(Servicios.DTO.Proveedores.ObjetivosDeCompra DTO)
			: base(DTO)
		{
            this.SetPresentador<Servicios.DTO.Proveedores.Proveedor>("PresentadorProveedor", (p => DTO.Proveedor = p), DTO.Proveedor);
            this.PresentadorProveedor.cantidadNumeros = 5;
			this.SetPresentador<Objetivos>("PresentadorObjetivos",DTO.Objetivos);
		}
                
	}
}
