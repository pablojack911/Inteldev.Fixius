using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Vistas;
using System;
using System.Windows;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System.Collections.ObjectModel;
using Inteldev.Fixius;

using Inteldev.Fixius.Preventa.ZonasGeograficas;
using Inteldev.Core.DTO;
using System.Configuration;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System.Collections.Generic;
using System.Text;
using GMap.NET;
using Inteldev.Fixius.Mapas;



namespace Fixius
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            FabricaPresentadores.Instancia.CargarRegistro(new RegistroMapeos());
            FabricaVistas.Instancia.CargarRegistro(new RegistroVistas());
            var ip = ConfigurationManager.AppSettings.GetValues("ServerIp")[0];

            FabricaClienteServicio.Instancia.ServerIp = ip; // "192.168.1.214";
            FabricaClienteServicio.Instancia.puertoTcp = 8081;
            Sistema.Comenzar();
            //conecta a los servicios
            try
            {
                var s = FabricaClienteServicio.Instancia.CrearCliente<IServicioUsuario>("ServicioUsuario");
                Sistema.Instancia.ControladorLogin.ServicioLogin = ((u, c) => s.Autenticar(u, c));
                var m = FabricaClienteServicio.Instancia.CrearCliente<IServicioMenu>("ServicioMenu");
                Sistema.Instancia.ControladorMenu.ServicioCargarMenu = ((u, t) => m.ObtenerMenu(u, t));
            }
            catch (Exception exc)
            {
                Mensajes.Error(exc);
            }
            //inicializa la vista
            InitializeComponent();
            //si no pones esto. No carga las opciones del menu.
            var presentadorMenu = (IPresentadorMenu)FabricaPresentadores.Instancia.Resolver(typeof(IPresentadorMenu));
            presentadorMenu.CargaMenu(Sistema.Instancia.ControladorLogin.UnidadDeNegocioActual, Sistema.Instancia.ControladorMenu);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ventana = new Prueba();
            ventana.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var vmpos = new VMPosicionGPSPreventistas();
            vmpos.ObtnereDatos();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var servicioPreventa = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Preventista>>();
            if (servicioPreventa != null)
            {
                var preventista = servicioPreventa.ObtenerPorCodigo("N9", Inteldev.Core.CargarRelaciones.NoCargarNada, "01");
                if (preventista != null)
                {
                    var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioCoordenadasClientes>("ServicioCoordenadasClientes");
                    if (servicio != null)
                    {
                        var x = servicio.ObtenerCoordenadasPorPreventista(preventista, DateTime.Now, "01");
                        var listacodigos = new StringBuilder();
                        if (x.Count > 0)
                        {
                            foreach (var item in x)
                            {
                                listacodigos.AppendLine(item.Codigo + "Lat: " + item.Latitud + ", Long: " + item.Longitud);
                            }
                            MessageBox.Show("Total de clientes: " + x.Count + ".\n" + listacodigos.ToString());
                        }
                        else
                            MessageBox.Show("No tiene ningun cliente.");
                    }
                    else
                        MessageBox.Show("servicioRutaDeVenta es null");
                }
                else
                    MessageBox.Show("preventista es null");
            }
            else
                MessageBox.Show("servicioPreventa es null");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var coordenada = new CoordenadaCliente() { Codigo = "A9940", Latitud = -38.011140, Longitud = -57.593774 };
            var coordenada2 = new CoordenadaCliente() { Codigo = "A9058", Latitud = -37.993234, Longitud = -57.610854 };
            var coordenada3 = new CoordenadaCliente() { Codigo = "A4919", Latitud = -37.991507, Longitud = -57.549089 };
            var coordenada4 = new CoordenadaCliente() { Codigo = "86867", Latitud = -37.969354, Longitud = -57.568013 };
            var listaCoordenadas = new List<CoordenadaCliente>();
            listaCoordenadas.Add(coordenada);
            listaCoordenadas.Add(coordenada2);
            listaCoordenadas.Add(coordenada3);
            listaCoordenadas.Add(coordenada4);
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioCoordenadasClientes>("ServicioCoordenadasClientes");
            if (servicio != null)
            {
                var gc = servicio.GrabarLista(listaCoordenadas, new Usuario() { Nombre = "POCHO" }, "01");
                MessageBox.Show(gc.getMensaje());
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //var coordenada = new Coordenada() { Latitud = -38.011140, Longitud = -57.593774 };
            //var coordenada2 = new Coordenada() {  Latitud = -37.993234, Longitud = -57.610854 };
            //var coordenada3 = new Coordenada() {  Latitud = -36.991507, Longitud = -57.549089 };
            //var coordenada4 = new Coordenada() {  Latitud = -35.969354, Longitud = -57.568013 };
            //var coordenada5 = new Coordenada() {  Latitud = -38.011140, Longitud = -57.593774 };
            //var listaCoordenadas = new List<Coordenada>();
            //listaCoordenadas.Add(coordenada);
            //listaCoordenadas.Add(coordenada2);
            //listaCoordenadas.Add(coordenada3);
            //listaCoordenadas.Add(coordenada4);
            //listaCoordenadas.Add(coordenada5);
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioRutaDeVenta>("ServicioRutaDeVenta");
            if (servicio != null)
            {
                var codigo = "0711";
                var empresa = "01";
                var div = "00BEB";
                var lista = servicio.ObtenerVertices(codigo, empresa, div);
                var sb = new StringBuilder(string.Format("OBTENIDO DE {0} - {1} - {2}\n", codigo, empresa, div));
                if (lista.Count > 0)
                {
                    foreach (var item in lista)
                    {
                        sb.AppendLine("Lat: " + item.Latitud + " Long: " + item.Longitud);
                    }
                }
                else
                    sb.AppendLine("SIN RESULTADOS");
                MessageBox.Show(sb.ToString(), "Resultados de la consulta", MessageBoxButton.OK);
            }

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var coord = new PointLatLng(-38.002601, -57.601849);
            GeoCoderStatusCode status;
            var res = GoogleMapGeocoder.ObtenerDireccionPorCoordenadas(coord, out status);
            Mensajes.Aviso(res);

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var x = new ImportKml();
            x.ImportartKml();
        }

        //private void Cerrando(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (MessageBox.Show("¿Seguro que desea salir?", "Alerta", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
        //        e.Cancel = true;
        //}

    }
}
