using Inteldev.Core;
using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
	public class VMArea : VistaModeloBase<Jerarquia>
	{
		#region DP

		public ObservableCollection<Jerarquia> Items
		{
			get { return (ObservableCollection<Jerarquia>)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Areas.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemsProperty =
			DependencyProperty.Register("Items", typeof(ObservableCollection<Jerarquia>), typeof(VMArea));

		#endregion

		#region Constructores

		public VMArea( )
		{
            servicioArea = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Area>>();
            servicioSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Sector>>();
            servicioSubSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subsector>>();
            servicioFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Familia>>();
            servicioSubFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subfamilia>>();
			this.CargaAreas();
		}

		public VMArea(Jerarquia DTO)
			: base(DTO)
		{
		}

		#endregion

		private IServicioABM<Area> servicioArea;
		private IServicioABM<Sector> servicioSector;
		private IServicioABM<Subsector> servicioSubSector;
		private IServicioABM<Familia> servicioFamilia;
		private IServicioABM<Subfamilia> servicioSubFamilia;

		public void CargaAreas( )
		{
			this.Items = new ObservableCollection<Jerarquia>();
			var areas = servicioArea.ObtenerLista(0,CargarRelaciones.NoCargarNada);

			Jerarquia raiz = new Jerarquia();
			Jerarquia area = null;
			Jerarquia sector = null;
			Jerarquia subSector = null;
			Jerarquia familia = null;
			Jerarquia subFamilia = null;

			foreach (var Area in areas)
			{
				area = new Jerarquia();
				area.Nivel = 1;
				area.Id = Area.Id;
				area.Nombre = Area.Nombre;
				area.Codigo = Area.Codigo;
				var sectores = servicioSector.ObtenerLista(Area.Id,CargarRelaciones.NoCargarNada);
				foreach (var Sector in sectores)
				{
					sector = new Jerarquia();
					sector.Id = Sector.Id;
					sector.Nivel = 2;
					sector.Nombre = Sector.Nombre;
					sector.Codigo = Sector.Codigo;
					sector.Padre = area;
					var subSectores = servicioSubSector.ObtenerLista(Sector.Id,CargarRelaciones.NoCargarNada);
					foreach (var SubSector in subSectores)
					{
						subSector = new Jerarquia();
						subSector.Nivel = 3;
						subSector.Id = SubSector.Id;
						subSector.Nombre = SubSector.Nombre;
						subSector.Codigo = SubSector.Codigo;
						subSector.Padre = sector;
						var familias = servicioFamilia.ObtenerLista(SubSector.Id,CargarRelaciones.NoCargarNada);
						foreach (var Familia in familias)
						{
							familia = new Jerarquia();
							familia.Nivel = 4;
							familia.Id = Familia.Id;
							familia.Codigo = Familia.Codigo;
							familia.Nombre = Familia.Nombre;
							familia.Padre = subSector;
							var subFamilias = servicioSubFamilia.ObtenerLista(Familia.Id,CargarRelaciones.NoCargarNada);
							foreach (var SubFamilia in subFamilias)
							{
								subFamilia = new Jerarquia();
								subFamilia.Nivel = 5;
								subFamilia.Id = SubFamilia.Id;
								subFamilia.Codigo = SubFamilia.Codigo;
								subFamilia.Nombre = SubFamilia.Nombre;
								subFamilia.Padre = familia;
								subFamilia.Nodos = null;
								familia.Nodos.Add(subFamilia);
							}
							subSector.Nodos.Add(familia);
						}
						sector.Nodos.Add(subSector);
					}
					area.Nodos.Add(sector);
				}
				raiz.Nodos.Add(area);
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
					//sector
					case 1:
						concretoDyn.Area = (Area)this.Mapeador(jerarquia.Padre, servicioArea.Crear());
						break;
					//subsector
					case 2:
						concretoDyn.Sector = (Sector)this.Mapeador(jerarquia.Padre, servicioSector.Crear());
						break;
					//familia
					case 3:
						concretoDyn.Subsector = (Subsector)this.Mapeador(jerarquia.Padre, servicioSubSector.Crear());
						break;
					//subfamilia
					case 4:
						concretoDyn.Familia = (Familia)this.Mapeador(jerarquia.Padre, servicioFamilia.Crear());
						break;
					default:
						break;
				}
			}
			return concreto;
		}

		public object Grabar(Jerarquia jerarquia, VMArea VM)
		{
			//despues fijarse cuando edito que pasa
			switch (jerarquia.Nivel)
			{
				case 1:
					servicioArea.Grabar((Area)this.Mapeador(jerarquia, servicioArea.Crear()),Sistema.Instancia.UsuarioActual);
					break;
				case 2:
					servicioSector.Grabar((Sector)this.Mapeador(jerarquia, servicioSector.Crear()), Sistema.Instancia.UsuarioActual);
					break;
				case 3:
					servicioSubSector.Grabar((Subsector)this.Mapeador(jerarquia, servicioSubSector.Crear()), Sistema.Instancia.UsuarioActual);
					break;
				case 4:
					servicioFamilia.Grabar((Familia)this.Mapeador(jerarquia, servicioFamilia.Crear()), Sistema.Instancia.UsuarioActual);
					break;
				case 6:
				case 5:
					servicioSubFamilia.Grabar((Subfamilia)this.Mapeador(jerarquia, servicioSubFamilia.Crear()), Sistema.Instancia.UsuarioActual);
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
					foreach (var area in actual.Nodos)
					{
						servicioArea.Borrar((Area)this.Mapeador(area, servicioArea.Crear()), Sistema.Instancia.UsuarioActual);
					}
					break;
				case 1:
					servicioArea.Borrar((Area)this.Mapeador(actual, servicioArea.Crear()), Sistema.Instancia.UsuarioActual);
					break;
				case 2:
					servicioSector.Borrar((Sector)this.Mapeador(actual, servicioSector.Crear()), Sistema.Instancia.UsuarioActual);
					break;
				case 3:
					servicioSubSector.Borrar((Subsector)this.Mapeador(actual, servicioSubSector.Crear()), Sistema.Instancia.UsuarioActual);
					break;
				case 4:
					servicioFamilia.Borrar((Familia)this.Mapeador(actual, servicioFamilia.Crear()), Sistema.Instancia.UsuarioActual);
					break;
				case 5:
					servicioSubFamilia.Borrar((Subfamilia)this.Mapeador(actual, servicioSubFamilia.Crear()), Sistema.Instancia.UsuarioActual);
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
