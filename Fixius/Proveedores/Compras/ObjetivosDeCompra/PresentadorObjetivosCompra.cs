using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;

using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Proveedores.Compras.ObjetivosDeCompra
{
    public class PresentadorObjetivosCompra : PresentadorGrilla<Servicios.DTO.Proveedores.ObjetivosDeCompra, Objetivos, ItemObjetivoDeCompra>
	{
		public PresentadorObjetivosCompra(Objetivos Objeto)
			: base(Objeto)
		{
			
		}

		public override void CrearVentana( )
		{
			this.ventana = new BaseVentanaDialogo();
			var vista = new ItemObjetivoDeCompra();
			var presentador = (IPresentadorMiniBusca<Articulo>)FabricaPresentadores.Instancia.Resolver(typeof(IPresentadorMiniBusca<Articulo>));			
			presentador.Entidad = this.Objeto.Articulo;
			presentador.ActualizarDto = p => p = this.Objeto.Articulo;
			vista.miniBuscaArticulo.Presentador = presentador;
			ventana.VistaPrincipal.Content = vista;
			ventana.DataContext = this; //asigna datacontext como este presentador.
			ventana.Show();
		}
	}
}
