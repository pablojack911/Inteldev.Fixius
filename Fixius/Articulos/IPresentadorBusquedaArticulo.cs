using System;
namespace Inteldev.Fixius.Articulos
{
	public interface IPresentadorBusquedaArticulo
	{
		System.Collections.Generic.List<Inteldev.Fixius.Servicios.DTO.Articulos.Area> CargaArea( );
		System.Collections.Generic.List<Inteldev.Fixius.Servicios.DTO.Articulos.Familia> Cargafamilia(int idSubSector);
		System.Collections.Generic.List<Inteldev.Fixius.Servicios.DTO.Articulos.Sector> CargaSector(int idArea);
		System.Collections.Generic.List<Inteldev.Fixius.Servicios.DTO.Articulos.Subfamilia> CargaSubFamilia(int idSubFamilia);
		System.Collections.Generic.List<Inteldev.Fixius.Servicios.DTO.Articulos.Subsector> CargaSubSector(int idSector);
		void SetServicios( );
	}
}
