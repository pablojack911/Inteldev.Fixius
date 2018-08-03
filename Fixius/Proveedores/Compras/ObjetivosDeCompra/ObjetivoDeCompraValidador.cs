using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inteldev.Fixius.Proveedores.Compras.ObjetivosDeCompra
{
    public class ObjetivoDeCompraValidador : PresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor>
	{

		public override object BuscarPorId(object p)
		{
            var so = Inteldev.Core.Presentacion.ClienteServicios.FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Servicios.DTO.Proveedores.ObjetivosDeCompra>>();
			List<Inteldev.Core.DTO.ResultadoBusqueda<Inteldev.Fixius.Servicios.DTO.Proveedores.ObjetivosDeCompra>> resultado = so.ObtenerResultados(p.ToString(),Sistema.Instancia.EmpresaActual.Codigo);
			foreach(var item in resultado)
			{
				if(item.CantidadDeItems > 0)
					return false;
			}
				return base.BuscarPorId(p);
		}
	}
}
