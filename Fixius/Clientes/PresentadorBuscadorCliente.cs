using Inteldev.Core.Contratos;
using Inteldev.Core.DTO;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Inteldev.Fixius.Clientes
{
    public class PresentadorBuscadorCliente : PresentadorBuscador<ClienteBusqueda>
    {
        public IServicioBusqueda<Inteldev.Fixius.Servicios.DTO.Clientes.ClienteBusqueda, Inteldev.Fixius.Servicios.DTO.Clientes.Cliente> ServicioBusqueda { get; set; }
        public PresentadorBuscadorCliente()
            : base()
        {
            this.ServicioBusqueda = FabricaClienteServicio.Instancia.CrearCliente<IServicioBusqueda<Inteldev.Fixius.Servicios.DTO.Clientes.ClienteBusqueda, Inteldev.Fixius.Servicios.DTO.Clientes.Cliente>>();
            ServicioBuscar = ((p, q) =>
            {
                var res = new List<ResultadoBusqueda<ClienteBusqueda>>();
                try
                {
                    res = this.ServicioBusqueda.ObtenerResultadosReducidos(p.ToString(), Sistema.Instancia.EmpresaActual.Codigo, q);
                }
                catch (CommunicationException ex)
                {
                    Mensajes.Aviso("Demasiados resultados para '" + p + "'. Especifique.");
                    this.ServicioBusqueda = FabricaClienteServicio.Instancia.CrearCliente<IServicioBusqueda<Inteldev.Fixius.Servicios.DTO.Clientes.ClienteBusqueda, Inteldev.Fixius.Servicios.DTO.Clientes.Cliente>>();
                }
                return res;
            }
                );
        }
    }
}
