using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.ServiceMobile;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using Inteldev.Fixius.Mapas.Pines;
using Inteldev.Core.Presentacion;
using Inteldev.Core.DTO.Locacion;

namespace Inteldev.Fixius.Mapas
{
    /// <summary>
    /// Interaction logic for Historial.xaml
    /// </summary>
    public partial class Historial : Window
    {
        public ObservableCollection<Elemento> Vendedores { get; set; }

        //List<string> codigosVendedoresAlta;
        //List<string> codigosVendedoresHiller;
        #region Servicios
        IServicioABM<Preventista> servicioPreventista;

        ServiceSoapClient serviceMobile = new ServiceSoapClient();
        #endregion
        public DateTime? diaSeleccionado { get; set; }

        //public Reporte VentanaReporte { get; set; }
        ReportMaker CreadorDeReportes;
        public Historial()
        {
            InitializeComponent();
            this.Vendedores = new ObservableCollection<Elemento>();

            this.servicioPreventista = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Inteldev.Fixius.Servicios.DTO.Preventa.Preventista>>();

            GeoCoderStatusCode status;
            this.map.Position = GoogleMapGeocoder.ObtenerCordenadasPorDireccion("Mar del Plata, Buenos Aires", out status);

            this.listaDeElementos.SelectionChanged += listaDeElementos_SelectionChanged;

            this.DataContext = this;

            //this.VentanaReporte = new Reporte();
            this.CreadorDeReportes = new ReportMaker();

            this.reportes.dgPosiciones.SelectionChanged += dgPosiciones_SelectionChanged;

            //this.codigosVendedoresAlta = this.ObtenerVendedoresPorEmpresa("10");
            //this.codigosVendedoresHiller = this.ObtenerVendedoresPorEmpresa("01");

        }


        void dgPosiciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                var dg = (DataGrid)sender;
                var posicion = (ItemReporte)dg.SelectedItem;
                if (posicion != null)
                {
                    var vendedor = this.Vendedores.FirstOrDefault(x => x.Codigo == posicion.CodigoVendedor);
                    var posiciones = vendedor.Posiciones.Where(x => x.Fecha >= posicion.CheckIn && x.Fecha <= posicion.CheckOut).Select(p => p.Coordenada).ToList();
                    this.map.Markers.Clear();
                    this.DibujarRuta(Brushes.Green, posiciones);
                    this.VerPuntosDeReporte(vendedor.Codigo, posiciones);
                    if (posicion.Cliente != "VIAJE") //muestro el pin del cliente
                    {
                        var coordCli = vendedor.Clientes.FirstOrDefault(c => c.Codigo == posicion.Cliente);
                        if (coordCli != null)
                        {
                            var marcador = new GMapMarker(new PointLatLng(coordCli.Latitud, coordCli.Longitud));

                            var pin = new PinAzul();

                            pin.Tag = coordCli;
                            pin.Etiqueta = coordCli.Codigo;
                            marcador.Shape = pin;
                            marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
                            this.map.Markers.Add(marcador);
                        }
                    }
                    this.RefrescarVista();
                }
            }
        }

        private void listaDeElementos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.map.Markers.Clear();
            if (this.listaDeElementos.SelectedItem != null)
            {
                var item = this.listaDeElementos.SelectedItem as Elemento;
                item.VerClientes = true;
                item.VerZona = true;
                item.VerTodasLasPosiciones = true;
                item.VerDomicilioDelVendedor = true;
                SeleccionarMarcador(item);
                //this.modo = ModoVerMarcadores.Seleccionado;
                //StringBuilder sb = new StringBuilder();
                //foreach (var x in item.Posiciones)
                //{
                //    sb.AppendLine(x.Cliente + " - " + x.Coordenada.ToString() + " - " + x.Fecha.ToString());
                //}
                //System.IO.File.WriteAllText(@"d:\WriteText.txt", sb.ToString());
            }
        }

        private void SeleccionarMarcador(Elemento item)
        {
            if (!item.CargadoPorCompleto)
            {
                ControladorVendedores.CargarZonas(item, diaSeleccionado.Value);
                ControladorVendedores.CargarClientes(item, diaSeleccionado.Value);
            }
            this.CargarPosiciones(item);
            MostrarMarcador(item, true);
            map.Position = item.CoordenadaActual;

            var reporte = this.CreadorDeReportes.CrearReporte(item);
            this.reportes.FechaDelReporte = this.dtpFecha.SelectedDate.Value.ToString("dddd, dd MMMM yyyy");
            this.reportes.Posiciones = new ObservableCollection<ItemReporte>(reporte);
            this.reportes.NombreVendedor = item.Nombre;
            this.reportes.CodigoVendedor = item.Codigo;

            //if (this.VentanaReporte.IsInitialized)
            //{
            //    this.VentanaReporte.Close();
            //    this.VentanaReporte = new Reporte();
            //}
            //this.VentanaReporte.Posiciones = new ObservableCollection<ItemReporte>(reporte);
            //this.VentanaReporte.CodigoVendedor = item.Codigo;
            //this.VentanaReporte.NombreVendedor = item.Nombre;
            //this.VentanaReporte.FechaDelReporte = this.dtpFecha.SelectedDate.Value.ToString("dddd, dd MMMM yyyy");
            //this.VentanaReporte.Owner = this;
            //this.VentanaReporte.Show();
        }

        private void CargarPosiciones(Elemento vendedor)
        {
            vendedor.Posiciones.Clear();

            var fechaDesde = diaSeleccionado.Value.AddHours(8);
            var fechaHasta = diaSeleccionado.Value.AddDays(1);

            try
            {
                ControladorVendedores.CargarPosiciones(vendedor, fechaDesde, fechaHasta);
            }
            catch (Exception exc)
            {

            }
        }

        private void MostrarMarcador(Elemento item, bool clear)
        {
            if (clear)
                map.Markers.Clear();

            var marcador = new GMapMarker(item.CoordenadaActual);
            Pin pin;
            if (item.Empresa == "10")
                pin = new PinNaranja();
            else
                pin = new PinAzul();
            pin.Tag = item;
            pin.Etiqueta = item.Codigo;

            marcador.Shape = pin;
            marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
            this.map.Markers.Add(marcador);

            this.VerZonasDelPreventista(item);
            this.VerClientes(item);
            this.VerCaminoDelPreventista(item);
            this.VerDomicilioVendedor(item);

            this.RefrescarVista();
        }

        private void RefrescarVista()
        {
            var zoomActual = this.map.Zoom;
            this.map.Zoom = 20;
            this.map.Zoom = zoomActual;
        }

        private void VerCaminoDelPreventista(Elemento vendedor)
        {
            var pinesPrev = new List<PointLatLng>();

            foreach (var item in vendedor.Posiciones)
            {
                pinesPrev.Add(item.Coordenada);
            }
            this.DibujarRuta(Brushes.Red, pinesPrev);
            this.VerPuntosDeReporte(vendedor.Codigo, pinesPrev);
            this.RefrescarVista();
        }

        private void VerPuntosDeReporte(string codigoPreventista, List<PointLatLng> coordenadas)
        {
            foreach (var item in coordenadas)
            {
                if (item.Lat != 0 && item.Lng != 0)
                {
                    var marcador = new GMapMarker(item);
                    var forma = new Ellipse() { Height = 10, Width = 10 };
                    forma.Fill = Brushes.Lavender;
                    marcador.Shape = forma;
                    marcador.Offset = new Point(-forma.Width / 2, -forma.Height);
                    this.map.Markers.Add(marcador);
                }
            }
        }

        private void VerClientes(Elemento vendedor)
        {
            foreach (var item in vendedor.Clientes)
            {
                if (item.Latitud != 0 && item.Longitud != 0)
                {
                    var marcador = new GMapMarker(new PointLatLng(item.Latitud, item.Longitud));
                    Pin pin;
                    if (vendedor.Posiciones.Any(p => p.Cliente == item.Codigo))
                        pin = new PinVerde();
                    else
                        pin = new PinAmarillo();
                    pin.Tag = item;
                    pin.Etiqueta = item.Orden.ToString();
                    marcador.Shape = pin;
                    marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
                    this.map.Markers.Add(marcador);
                }
            }
        }

        private object VerDomicilioVendedor(object e)
        {
            var prev = (Elemento)e;
            prev.VerDomicilioDelVendedor = true;
            try
            {
                if (prev.CoordenadaDomicilio.Lat != 0 && prev.CoordenadaDomicilio.Lng != 0)
                {
                    var marcador = new GMapMarker(prev.CoordenadaDomicilio);

                    var pin = new PinCasa();

                    pin.Tag = prev;
                    pin.Etiqueta = "";
                    marcador.Shape = pin;
                    marcador.Shape.IsHitTestVisible = false;
                    marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
                    this.map.Markers.Add(marcador);
                }
            }
            catch (Exception ex)
            {
                Mensajes.Aviso(ex.Message);
            }
            return 0;
        }

        private void VerZonasDelPreventista(Elemento e)
        {
            Random r = new Random();
            var prev = (Elemento)e;
            prev.VerZona = true;

            foreach (var item in prev.ZonasMapa)
            {
                var alpha = r.Next(0, byte.MaxValue + 1);
                var red = r.Next(0, byte.MaxValue + 1);
                var green = r.Next(0, byte.MaxValue + 1);
                var blue = r.Next(0, byte.MaxValue + 1);
                var brush = new SolidColorBrush(Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue));
                this.DibujarPoligono(brush, item.Vertices);
            }
        }

        private void DibujarPoligono(SolidColorBrush solidColorBrush, List<Coordenada> vertices)
        {
            var poly = vertices.Select(v => new PointLatLng(v.Latitud, v.Longitud));
            ControladorZona controlZona = new ControladorZona(this.map);
            controlZona.CrearPoligono(poly, solidColorBrush);
        }

        private void DibujarRuta(SolidColorBrush solidColorBrush, List<PointLatLng> vertices)
        {
            ControladorZona controlZona = new ControladorZona(this.map);
            controlZona.CrearRuta(vertices, solidColorBrush);
        }

        private void dtpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            diaSeleccionado = null;
            this.Vendedores.Clear();
            diaSeleccionado = dtpFecha.SelectedDate;
            if (diaSeleccionado != null)
            {
                var dt = serviceMobile.ObtenerVendedoresPorDia(diaSeleccionado.Value, diaSeleccionado.Value.AddDays(1));

                foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
                {
                    Func<object, dynamic, object> nonull = (p, def) => p == null ? def : p;
                    var elemento = new Elemento()
                    {
                        Codigo = item.Field<string>("usuario"),
                        Empresa = item.Field<string>("empresa"), //pertenece a Vendedores, no a PosicionGps
                        Visitados = (int)nonull(item.Field<object>("visitados"), 0),
                        Compradores = (int)nonull(item.Field<object>("compradores"), 0),
                        Bultos = (int)nonull(item.Field<object>("bultos"), 0),
                        Posiciones = new List<Posicion>(),
                        Clientes = new List<CoordenadaCliente>(),
                        FondoDeCelda = item.Field<string>("empresa") == "10" ? @"h:\Sistemas\alta.jpg" : @"h:\Sistemas\hiller.jpg"
                        //ZonasMapa = new ObservableCollection<ZonaMapa>()
                        //Pesos = decimal.Parse(nonull(item.Field<object>("pesos").ToString(),"0").ToString())
                    };
                    if (elemento.Codigo != null)
                        this.AgregarVendedor(elemento);
                }
            }
        }

        private void AgregarVendedor(Elemento elemento)
        {
            var v = Vendedores.FirstOrDefault(e => e.Codigo == elemento.Codigo);
            if (v == null)
            {
                var prev = servicioPreventista.ObtenerPorCodigo(elemento.Codigo, Core.CargarRelaciones.NoCargarNada, "01");
                if (prev != null)
                {
                    elemento.Foto = prev.Foto;
                    elemento.Nombre = prev.Nombre;
                    elemento.CoordenadaDomicilio = new PointLatLng(prev.Latitud, prev.Longitud);
                }

                if (elemento.CoordenadaActual.Lat == 0 && elemento.CoordenadaActual.Lng == 0) //si lat y lng vienen 0, 0 es porque tiene gps apagado
                {
                    elemento.CoordenadaActual = new PointLatLng(-38.002452, -57.601936);
                    elemento.Estado = Estado.GPS_APAGADO;
                }

                this.Vendedores.Add(elemento);
            }
        }

        private void btnchkVerAlta_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.Vendedores)
            {
                if (item.Empresa == "10")
                    item.Visible = true;
                else
                    item.Visible = false;
            }
        }

        private void btnchkVerHiller_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.Vendedores)
            {
                if (item.Empresa == "01")
                    item.Visible = true;
                else
                    item.Visible = false;
            }
        }

        private void btnchkVerTODO_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.Vendedores)
            {
                item.Visible = true;
            }
        }
    }
}
