using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Controladores;
using Inteldev.Fixius.Controles.Proveedor;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
	public class VMObjetivosDeCompra : VistaModeloBase<ObjetivosDeCompra>
	{
		#region DP's

		public IPresentadorMiniBusca<Proveedor> PresentadorProveedor
		{
			get { return (IPresentadorMiniBusca<Proveedor>)GetValue(PresentadorProveedorProperty); }
			set { SetValue(PresentadorProveedorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorProveedor.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorProveedorProperty =
			DependencyProperty.Register("PresentadorProveedor", typeof(IPresentadorMiniBusca<Proveedor>), typeof(VMObjetivosDeCompra));



		public IPresentadorGrilla<ObjetivosDeCompra, Objetivos, ItemObjetivoDeCompra> PresentadorObjetivos
		{
			get { return (IPresentadorGrilla<ObjetivosDeCompra,Objetivos,ItemObjetivoDeCompra>)GetValue(PresentadorObjetivosProperty); }
			set { SetValue(PresentadorObjetivosProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PresentadorObjetivos.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PresentadorObjetivosProperty =
			DependencyProperty.Register("PresentadorObjetivos", typeof(IPresentadorGrilla<ObjetivosDeCompra,Objetivos,ItemObjetivoDeCompra>), typeof(VMObjetivosDeCompra));		

		#endregion

		public VMObjetivosDeCompra( ) : base() { }

		public VMObjetivosDeCompra(ObjetivosDeCompra DTO)
			: base(DTO)
		{
			this.SetPresentador<Proveedor>("PresentadorProveedor",(p=>DTO.Proveedor=p),DTO.Proveedor);
			this.SetPresentador<Objetivos>("PresentadorObjetivos",DTO.Objetivos);
		}
                
	}
}
