using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using System;

namespace Inteldev.Fixius.Proveedores.Compras.ListaDePrecios
{
	public class PresentadorListasDePreciosProveedor : PresentadorABM<Servicios.DTO.Proveedores.ListaDePrecios, VMListaProveedor>
	{
		public override void Configurar()
		{
			base.Configurar();
			Func<Servicios.DTO.Proveedores.ListaDePrecios> nuevo = delegate
			{
				var vistaNuevo = new BaseVentanaDialogo();
				vistaNuevo.VistaPrincipal.Content = new VistaListaProveedorNuevo();
				var presentador = new VMSeleccionaProveedor() { Ventana = vistaNuevo };
				vistaNuevo.DataContext = presentador;
				vistaNuevo.ShowDialog();

				if (presentador.SeleccionOk)
				{
					return this.Servicio.CrearConParametros(Sistema.Instancia.EmpresaActual.Codigo,presentador.ObtenerCodigo()).GetEntidad();
				}
				else
					return null;
			};

			this.CmdNuevo = new RelayCommand(m => TryCatch.Intentar(i => this.Editar(nuevo()), this.PuedeCrearNuevo()));
		}

	}
}
