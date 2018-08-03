using Inteldev.Core;
using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Articulos
{
	public class PresentadorBusquedaArticuloOrden : PresentadorBusquedaArticulo
	{
		public List<Articulo> Articulos { get; set; }

		public PresentadorBusquedaArticuloOrden( ) : base() { }

		public override List<Area> CargaArea( )
		{
			var areas = from art in Articulos
						select art.AreaId;
			var lista = areas.Distinct().ToList();
			var result = new List<Area>();
			foreach (var item in lista)
			{
				var area = this.servicioArea.ObtenerPorId(item.Value, CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo);
				result.Add(area);
			}
			return result;
		}

		public override List<Sector> CargaSector(int idArea)
		{
			var sectores = from art in Articulos
						   where art.AreaId == idArea
						select art.SectorId;
			var lista = sectores.Distinct().ToList();
			var result = new List<Sector>();
			foreach (var item in lista)
			{
				if(item != null)
					result.Add(this.servicioSector.ObtenerPorId(item.Value, CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo));
			}
			return result;
		}

		public override List<Subsector> CargaSubSector(int idSector)
		{
			var subSectores = from art in Articulos
							  where art.SectorId == idSector
						   select art.SubsectorId;
			var lista = subSectores.Distinct().ToList();
			var result = new List<Subsector>();
			foreach (var item in lista)
			{
				if (item != null)
					result.Add(this.servicioSubSector.ObtenerPorId(item.Value, CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo));
			}
			return result;
		}

		public override List<Familia> Cargafamilia(int idSubSector)
		{
			var familias = from art in Articulos
						   where art.SubsectorId == idSubSector
						   select art.FamiliaId;
			var lista = familias.Distinct().ToList();
			var result = new List<Familia>();
			foreach (var item in lista)
			{
				if (item != null)
					result.Add(this.servicioFamilia.ObtenerPorId(item.Value, CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo));
			}
			return result;
		}

		public override List<Subfamilia> CargaSubFamilia(int idSubFamilia)
		{
			var subFamilia = from art in Articulos
						   select art.SubfamiliaId;
			var lista = subFamilia.Distinct().ToList();
			var result = new List<Subfamilia>();
			foreach (var item in lista)
			{
				if (item != null)
					result.Add(this.servicioSubFamilia.ObtenerPorId(item.Value, CargarRelaciones.NoCargarNada,Sistema.Instancia.EmpresaActual.Codigo));
			}
			return result;
		}
	}
}
