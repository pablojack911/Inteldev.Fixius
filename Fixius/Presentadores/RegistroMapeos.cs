using Inteldev.Core.Patrones;
using Inteldev.Core.Presentacion.Presentadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Fixius.Servicios.DTO.Precios;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Tesoreria;
using Inteldev.Fixius.Articulos;
using Inteldev.Fixius.Clientes.Tablas.TarjetaMayorista;
using Inteldev.Fixius.Preventa.ListaDePrecios;
using Inteldev.Fixius.Preventa.DescuentosPorLista;
using Inteldev.Fixius.Tesoreria.MovimientoBancario;
using Inteldev.Fixius.Articulos.Maestro;
using Inteldev.Fixius.Clientes.Maestro;
using Inteldev.Fixius.Clientes.Tablas.TarjetaCliente;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Articulos.Tablas.Envase;
using Inteldev.Fixius.Clientes;
using Inteldev.Fixius.Proveedores.CuenasAPagar.CargaComprobantes;
using Inteldev.Fixius.Proveedores.Compras.ObjetivosDeCompra;
//using Microsoft.Practices.Unity;


namespace Inteldev.Fixius.Presentadores
{
    /// <summary>
    /// Contiene los mapeos para el unity. Asocia Interfaces con tipos concretos. 
    /// </summary>
    public class RegistroMapeos : RegistroFabrica
    {
        public override void Configurar()
        {
            this.Registrar(typeof(IPresentadorDomicilio), typeof(PresentadorDomicilio));
            this.Registrar(typeof(IPresentadorContacto<>), typeof(PresentadorContacto<>));
            this.Registrar(typeof(IPresentadorListaValores<,,>), typeof(PresentadorListaValores<,,>));
            this.Registrar(typeof(IPresentadorMaestroDetalle<,>), typeof(PresentadorMaestroDetalle<,>));
            this.Registrar(typeof(IPresentadorGrilla<,,>), typeof(PresentadorGrilla<,,>));
            this.Registrar(typeof(IPresentadorMiniBusca<>), typeof(PresentadorMiniBusca<>));
            this.Registrar(typeof(IPresentadorBuscador<>), typeof(PresentadorBuscador<>));
            this.Registrar(typeof(IPresentadorMinibuscaList<,>), typeof(PresentadorMinibuscaList<,>));
            this.Registrar(typeof(IPresentadorABM<,>), typeof(PresentadorABM<,>));
            this.Registrar(typeof(IPresentadorBase<>), typeof(PresentadorBase<>));
            this.Registrar(typeof(IPresentadorBaseDialogo<>), typeof(PresentadorBaseDialogo<>));
            this.Registrar(typeof(IPresentadorBusquedaArticulo), typeof(PresentadorBusquedaArticulo));
            this.Registrar(typeof(IPresentadorTarjetaMayorista), typeof(PresentadorTarjetaMayorista));
            this.Registrar(typeof(IPresentadorMenu), typeof(PresentadorMenu));
            this.Registrar(typeof(IPresentadorGrilla<ObjetivosDeCompra, Objetivos, ItemObjetivoDeCompra>), typeof(PresentadorObjetivosCompra));
            this.Registrar(typeof(IPresentadorGrilla<ListaDePreciosDeVenta, ItemListaDePreciosDeVenta, ItemDetalleListaDePrecioDeVenta>),
                           typeof(PresentadorItemListaDePreciosDeVenta));

            this.Registrar(typeof(IPresentadorGrilla<DescuentosPorLista, ItemDescuentoPorLista, VistaItemDescuentoPorLista>),
                           typeof(PresentadorItemDescuentoPorLista));

            this.Registrar(typeof(IPresentadorGrilla<DocumentoCompra, ItemsConceptos, ItemConceptoComprobanteDeCompra>), typeof(PresentadorItemDocumentoProveedor));

            //validador improvisado
            this.Registrar(typeof(IPresentadorMiniBusca<ObjetivosDeCompra>), typeof(Inteldev.Fixius.Proveedores.Compras.ObjetivosDeCompra.ObjetivoDeCompraValidador));
            this.Registrar(typeof(IPresentadorGrilla<CuentaBancaria, MovimientoBancario, ItemMovimientoBancario>), typeof(PresentadorItemMovimientoBancario));
            this.Registrar(typeof(IPresentadorBuscador<DocumentoCompra>), typeof(PresentadorBuscadorDocumnetoCompra));
            this.Registrar(typeof(IPresentadorBuscador<ClienteBusqueda>), typeof(PresentadorBuscadorCliente));
            //this.Registrar(typeof(IPresentadorArticulosCompuestos), typeof(PresentadorArticuloCompuesto));
            this.Registrar(typeof(IPresentadorGrilla<Cliente, RutaDeVenta, VistaRutasDeVentaCliente>), typeof(PresentadorRutasDeVentaCliente));
            this.Registrar(typeof(IPresentadorGrilla<Articulo, ArticuloCompuesto, ItemArticuloComponente>), typeof(PresentadorArticuloComponente));
            this.Registrar(typeof(IPresentadorGrilla<Envase, ArticuloEnvase, ItemArticuloEnvase>), typeof(PresentadorArticuloEnvase));

        }

    }
}
