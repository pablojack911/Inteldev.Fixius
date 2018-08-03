using Inteldev.Core;
using Inteldev.Core.Contratos;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;

using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inteldev.Fixius.Articulos
{
	public class PresentadorBusquedaArticulo : Inteldev.Fixius.Articulos.IPresentadorBusquedaArticulo 
	{
		protected IServicioABM<Area> servicioArea;
		protected IServicioABM<Sector> servicioSector;
		protected IServicioABM<Subsector> servicioSubSector;
		protected IServicioABM<Familia> servicioFamilia;
		protected IServicioABM<Subfamilia> servicioSubFamilia;

		public Func<int, List<Area>> ServicioBuscar { get; set; }

		public string hola;

		public PresentadorBusquedaArticulo()
		{
		}

		public void SetServicios( )
		{
			servicioArea = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Area>>();
			servicioSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Sector>>();
			servicioSubSector = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subsector>>();
			servicioFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Familia>>();
			servicioSubFamilia = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Subfamilia>>();
		}

		public virtual List<Area> CargaArea( )
		{
			return servicioArea.ObtenerLista(0,CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo).ToList();
		}

		public virtual List<Sector> CargaSector(int idArea)
		{
			return servicioSector.ObtenerLista(idArea,CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo).ToList();
		}

		public virtual List<Subsector> CargaSubSector(int idSector)
		{
			return servicioSubSector.ObtenerLista(idSector,CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo).ToList();
		}

		public virtual List<Familia> Cargafamilia(int idSubSector)
		{
			return servicioFamilia.ObtenerLista(idSubSector,CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo).ToList();
		}

		public virtual List<Subfamilia> CargaSubFamilia(int idSubFamilia)
		{
			return servicioSubFamilia.ObtenerLista(idSubFamilia,CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo).ToList();
		}

	}
}
