using Inteldev.Core;
using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.FuerzaDeVenta
{
    public class VMFuerzaDeVenta : VistaModeloBase<Jerarquia>
    {
        #region DP

		public ObservableCollection<Jerarquia> Items
		{
			get { return (ObservableCollection<Jerarquia>)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Areas.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemsProperty =
			DependencyProperty.Register("Items", typeof(ObservableCollection<Jerarquia>), typeof(VMFuerzaDeVenta));

		#endregion

		#region Constructores

		public VMFuerzaDeVenta( )
		{
            servicioJefe = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Servicios.DTO.Preventa.Jefe>>();
            servicioSupervisor = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Servicios.DTO.Preventa.Supervisor>>();
            servicioVendedor = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Servicios.DTO.Preventa.Preventista>>();
			this.CargaAreas();
		}

		public VMFuerzaDeVenta(Jerarquia DTO)
			: base(DTO)
		{
		}

		#endregion

		private IServicioABM<Servicios.DTO.Preventa.Jefe> servicioJefe;
        private IServicioABM<Servicios.DTO.Preventa.Supervisor> servicioSupervisor;
        private IServicioABM<Servicios.DTO.Preventa.Preventista> servicioVendedor;

		public void CargaAreas( )
		{
			this.Items = new ObservableCollection<Jerarquia>();
			var jefes = servicioJefe.ObtenerLista(0,CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo);

			Jerarquia raiz = new Jerarquia();
			Jerarquia jefe = null;
			Jerarquia supervisor = null;
			Jerarquia vendedor = null;

			foreach (var Jefe in jefes)
			{
				jefe = new Jerarquia();
				jefe.Nivel = 1;
				jefe.Id = Jefe.Id;
				jefe.Nombre = Jefe.Nombre;
				jefe.Codigo = Jefe.Codigo;
				var supervisores = servicioSupervisor.ObtenerLista(Jefe.Id,CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo);
				foreach (var Supervisor in supervisores)
				{
					supervisor = new Jerarquia();
					supervisor.Id = Supervisor.Id;
					supervisor.Nivel = 2;
					supervisor.Nombre = Supervisor.Nombre;
					supervisor.Codigo = Supervisor.Codigo;
					supervisor.Padre = jefe;
					var vendedores = servicioVendedor.ObtenerLista(Supervisor.Id,CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo);
					foreach (var Vendedor in vendedores)
					{
						vendedor = new Jerarquia();
						vendedor.Nivel = 3;
						vendedor.Id = Vendedor.Id;
						vendedor.Nombre = Vendedor.Nombre;
						vendedor.Codigo = Vendedor.Codigo;
						vendedor.Padre = supervisor;
						supervisor.Nodos.Add(vendedor);
					}
					jefe.Nodos.Add(supervisor);
				}
				raiz.Nodos.Add(jefe);
			}
			raiz.Nombre = "Raiz";
			raiz.Nivel = 0;
			raiz.Codigo = "0";
			Items.Add(raiz);
		}

		public object Mapeador(Jerarquia jerarquia, DTOMaestro concreto)
		{
			concreto.Id = jerarquia.Id;
			concreto.Codigo = jerarquia.Codigo;
			concreto.Nombre = jerarquia.Nombre;
			dynamic concretoDyn = concreto;
			if (jerarquia.Padre != null) //no estoy en la raiz
			{
				switch (jerarquia.Padre.Nivel)
				{
					
					case 1:
                        concretoDyn.Jefe = (Servicios.DTO.Preventa.Jefe)this.Mapeador(jerarquia.Padre, servicioJefe.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad());
						break;
					
					case 2:
                        concretoDyn.Supervisor = (Servicios.DTO.Preventa.Supervisor)this.Mapeador(jerarquia.Padre, servicioSupervisor.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad());
						break;
					
					case 3:
                        concretoDyn.Vendedor = (Servicios.DTO.Preventa.Preventista)this.Mapeador(jerarquia.Padre, servicioVendedor.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad());
						break;
					default:
						break;
				}
			}
			return concreto;
		}

		public object Grabar(Jerarquia jerarquia, VMFuerzaDeVenta VM)
		{
			//despues fijarse cuando edito que pasa
			switch (jerarquia.Nivel)
			{
				case 1:
                    servicioJefe.Grabar((Servicios.DTO.Preventa.Jefe)this.Mapeador(jerarquia, servicioJefe.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad()), Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
					break;
				case 2:
                    servicioSupervisor.Grabar((Servicios.DTO.Preventa.Supervisor)this.Mapeador(jerarquia, servicioSupervisor.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad()), Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
					break;
				case 3:
                    servicioVendedor.Grabar((Servicios.DTO.Preventa.Preventista)this.Mapeador(jerarquia, servicioVendedor.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad()), Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
					break;
				default:
					break;
			}
			this.CargaAreas();
			return true;
		}

		public object Borrar(object p)
		{
			var actual = (Jerarquia)p;
			switch (actual.Nivel)
			{
				case 0:
					foreach (var Jefe in actual.Nodos)
					{
                        servicioJefe.Borrar((Servicios.DTO.Preventa.Jefe)this.Mapeador(Jefe, servicioJefe.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad()), Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
					}
					break;
				case 1:
                    servicioJefe.Borrar((Servicios.DTO.Preventa.Jefe)this.Mapeador(actual, servicioJefe.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad()), Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
					break;
				case 2:
                    servicioSupervisor.Borrar((Servicios.DTO.Preventa.Supervisor)this.Mapeador(actual, servicioSupervisor.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad()), Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
					break;
				case 3:
                    servicioVendedor.Borrar((Servicios.DTO.Preventa.Preventista)this.Mapeador(actual, servicioVendedor.Crear(Sistema.Instancia.EmpresaActual.Codigo).GetEntidad()), Sistema.Instancia.UsuarioActual, Sistema.Instancia.EmpresaActual.Codigo);
					break;
				default:
					break;
			}
			this.CargaAreas();
			return true;
		}

		public Jerarquia CreaItemArbol(object seleccionado)
		{
			var item = new Jerarquia();
			Jerarquia sel = (Jerarquia)seleccionado;
			item.Id = 0;
			item.Nivel = sel.Nivel + 1;
			item.Nodos = null;
			if (item.Nivel == 6)
				item.Padre = sel.Padre;
			else
				item.Padre = sel;
			return item;
		}

		public Jerarquia Editar(object seleccionado)
		{
			var sel = (Jerarquia)seleccionado;
			var item = new Jerarquia();
			item.Id = sel.Id;
			item.Nombre = sel.Nombre;
			item.Codigo = sel.Codigo;
			item.Padre = sel.Padre;
			return sel;
		}
    }
}
