using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.Controladores;
using System;
namespace Inteldev.Fixius.Presentadores
{
	public interface IPresentadorMenu
	{
		void CargaMenu(UnidadeDeNegocio? UnidadActual, ControladorMenu cm);
	}
}
