using GMap.NET;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Fixius.ServiceMobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Timers;
using Inteldev.Core.Presentacion;
using System.Threading;

namespace Inteldev.Fixius.Mapas
{
    public class ControladorVendedores
    {
        public static void CargarZonas(Elemento item, DateTime fecha)
        {
            var servicioRutaVenta = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioRutaDeVenta>("ServicioRutaDeVenta");
            var zonas = servicioRutaVenta.ObtenerRutasDelDia(item.Codigo, fecha);
            foreach (var zona in zonas)
            {
                item.ZonasMapa.Add(new ZonaMapa()
                {
                    Codigo = zona.Codigo,
                    Vertices = servicioRutaVenta.ObtenerVertices(zona.Codigo, zona.Empresa.Codigo, zona.Division)
                });
            }
            item.CargadoPorCompleto = item.ZonasMapa.Count > 0 && item.Clientes.Count > 0;
        }

        public static void CargarClientes(Elemento item, DateTime fecha)
        {
            var servicioCoordenadaCliente = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioCoordenadasClientes>("ServicioCoordenadasClientes");
            if (item.Clientes.Count == 0)
            {
                var coordenadasClientes = servicioCoordenadaCliente.ObtenerCoordenadasPorPreventista(item.Codigo, fecha, "01");
                item.Clientes.AddRange(coordenadasClientes);
            }
            item.CargadoPorCompleto = item.ZonasMapa.Count > 0 && item.Clientes.Count > 0;
        }

        public static void CargarPosiciones(Elemento vendedor, DateTime fechaDesde, DateTime fechaHasta)
        {
            ServiceSoapClient serviceMobile = new ServiceSoapClient();
            var dt = serviceMobile.ObtenerPosicionesDelPreventista(vendedor.Codigo, fechaDesde, fechaHasta);
            Func<object, dynamic, object> nonull = (p, def) => p == null ? def : p;
            var ultimaCoordenadaConocida = new PointLatLng() { Lat = -38.002601, Lng = -57.601849 };
            foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
            {
                var pos = new Posicion();

                var Lat = double.Parse(item.Field<object>("latitud").ToString());
                var Lng = double.Parse(item.Field<object>("longitud").ToString());

                if (Lat != 0 && Lng != 0)
                {
                    ultimaCoordenadaConocida.Lat = Lat;
                    ultimaCoordenadaConocida.Lng = Lng;
                }

                pos.Coordenada = new PointLatLng()
                {
                    Lat = ultimaCoordenadaConocida.Lat,
                    Lng = ultimaCoordenadaConocida.Lng
                };

                pos.Cliente = item.Field<object>("cliente").ToString();
                if (item.Field<object>("estado") != null)
                    pos.Estado = (Estado)item.Field<object>("estado");
                else
                    pos.Estado = Estado.OK;
                pos.Fecha = item.Field<DateTime>("fecha");
                if (item.Field<object>("motivonocompra") != null)
                    pos.MotivoNoCompra = (MotivoNoCompra)item.Field<object>("motivonocompra");
                else
                    pos.MotivoNoCompra = MotivoNoCompra.Compra;

                pos.BultosCompra = int.Parse((nonull(item.Field<object>("bultos"), 0).ToString()));
                pos.PesosCompra = Decimal.Parse(nonull(item.Field<object>("pesos"), 0).ToString());

                vendedor.Posiciones.Add(pos);
            }
        }

        public static void CalcularBultosYPesos(Elemento vendedor, DateTime fechaDesde, DateTime fechaHasta)
        {
            if (vendedor.Posiciones.Count == 0)
                ControladorVendedores.CargarPosiciones(vendedor, fechaDesde, fechaHasta);

            var listaDeClientesVisitados = vendedor.Posiciones.Where(p => p.Cliente != "" && p.Estado == Estado.CHECKOUT_CLIENTE).ToList(); //tomo todos los checkouts donde tengo almacenados los pesos y los bultos
            var listaCodigosRevisados = new List<string>(); //almaceno aqui los codigos de los clientes que voy revisando. ocurre que hay veces que se registran mas de una vez el mismo cliente, con cantidades distintas de pesos y bultos. debo tomar siempre el último registro de estos.
            var listaFinalCheckouts = new List<Posicion>();
            foreach (var item in listaDeClientesVisitados)
            {
                var cliente = item.Cliente;
                if (!listaCodigosRevisados.Contains(cliente)) //verifico que no haya analizado a este cliente aun
                {
                    listaCodigosRevisados.Add(cliente); //procedo a agregarlo para saltear la proxima ocurrencia en la lista 

                    var todosLosRegistrosDeEsteCliente = listaDeClientesVisitados.Where(p => p.Cliente == cliente && p.BultosCompra != null && p.PesosCompra != null).ToList();
                    listaFinalCheckouts.Add(todosLosRegistrosDeEsteCliente.OrderByDescending(t => t.Fecha).FirstOrDefault());
                }
                //listaDeClientesVisitados.RemoveAll(p => p.Cliente == cliente);
            }
            vendedor.Pesos = listaFinalCheckouts.Sum(p => p.PesosCompra);
            vendedor.Bultos = listaFinalCheckouts.Sum(p => p.BultosCompra);
        }

        public static void ActualizarCoordenadasClientes()
        {
            var servicioCoordenadaCliente = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioCoordenadasClientes>("ServicioCoordenadasClientes");
            var coordenadas = servicioCoordenadaCliente.ActualizarCoordenadasClientes().ToList();
            if (coordenadas.Count > 0)
            {
                Thread newWindowThread = new Thread(new ThreadStart(() =>
                {

                    // Create and show the Window
                    var ventanaActualizar = new ActualizarCoordenadasClientes(coordenadas);
                    ventanaActualizar.Show();
                    // Start the Dispatcher Processing
                    System.Windows.Threading.Dispatcher.Run();
                })) { Name = "VENTANA" };
                // Set the apartment state
                newWindowThread.SetApartmentState(ApartmentState.STA);
                // Make the thread a background thread
                newWindowThread.IsBackground = true;
                // Start the thread
                newWindowThread.Start();
            }
            else
                Mensajes.Aviso("La acción no produjo ningún resultado.");
        }
    }
}
