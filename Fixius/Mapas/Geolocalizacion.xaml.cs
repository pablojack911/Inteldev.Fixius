using Fixius;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Mapas.Pines;
using Inteldev.Fixius.ServiceMobile;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Speech.Synthesis;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Inteldev.Fixius.Mapas
{
    /// <summary>
    /// Interaction logic for Geolocalizacion.xaml
    /// </summary>
    public partial class Geolocalizacion : Window
    {
        #region Props

        Timer actualizadorDePosiciones;
        Timer locutor;

        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        ModoVerMarcadores modo;

        public ObservableCollection<Elemento> vendedores { get; set; }

        Queue<String> colaDeMensajes;

        SpeechSynthesizer voz = new SpeechSynthesizer();

        #endregion

        #region Prop Dp's

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(Geolocalizacion), new PropertyMetadata(string.Empty));

        public string EtiquetaBotonSilenciar
        {
            get { return (string)GetValue(EtiquetaBotonSilenciarProperty); }
            set { SetValue(EtiquetaBotonSilenciarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EtiquetaBotonSilenciar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EtiquetaBotonSilenciarProperty =
            DependencyProperty.Register("EtiquetaBotonSilenciar", typeof(string), typeof(Geolocalizacion), new PropertyMetadata("Silenciar"));



        public string FiltroDeLista
        {
            get { return (string)GetValue(FiltroDeListaProperty); }
            set { SetValue(FiltroDeListaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FiltroDeLista.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FiltroDeListaProperty =
            DependencyProperty.Register("FiltroDeLista", typeof(string), typeof(Geolocalizacion), new PropertyMetadata("Codigo"));



        #endregion

        #region Servicios
        IServicioABM<Preventista> servicioPreventista;

        ServiceSoapClient serviceMobile = new ServiceSoapClient();
        #endregion

        #region Commands
        public ICommand CmdVerClientesPorRuta { get; set; }
        //public ICommand CmdOcultarClientes { get; set; }
        public ICommand CmdDibujarZona { get; set; }
        public ICommand CmdVerCaminoPreventista { get; set; }
        public ICommand CmdVerificarPreventistaDentroDeZona { get; set; }
        public ICommand CmdVerDomicilioVendedor { get; set; }
        #endregion

        public Geolocalizacion()
        {
            InitializeComponent();
            this.servicioPreventista = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Inteldev.Fixius.Servicios.DTO.Preventa.Preventista>>();

            this.CmdVerClientesPorRuta = new RelayCommand(e => this.VerClientesDeLaRuta(e));
            this.CmdDibujarZona = new RelayCommand(e => this.VerZonasDelPreventista(e));
            this.CmdVerCaminoPreventista = new RelayCommand(e => this.VerCaminoDelPreventista(e));
            this.CmdVerificarPreventistaDentroDeZona = new RelayCommand(e => this.VerificarPreventistaDentroDeZona(e));
            this.CmdVerDomicilioVendedor = new RelayCommand(e => this.VerDomicilioVendedor(e));

            this.modo = ModoVerMarcadores.Nada;
            this.vendedores = new ObservableCollection<Elemento>();
            this.colaDeMensajes = new Queue<string>();
            this.DataContext = this;

            GeoCoderStatusCode status;
            map.Position = GoogleMapGeocoder.ObtenerCordenadasPorDireccion("Mar del Plata, Buenos Aires", out status);

            this.listaDeElementos.SelectionChanged += listaDeElementos_SelectionChanged;

            this.actualizadorDePosiciones = new Timer(45000);
            this.actualizadorDePosiciones.Elapsed += actualizadorDePosiciones_Elapsed;

            this.locutor = new Timer(30000);
            this.locutor.Elapsed += locutor_Elapsed;

            this.actualizadorDePosiciones.Start();
            this.locutor.Start();

            this.voz.SpeakCompleted += voz_SpeakCompleted;

            this.FechaDesde = DateTime.Today.AddHours(8);
            this.FechaHasta = DateTime.Today.AddDays(1);

            this.ObtenerPosiciones();
        }

        void locutor_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var item in vendedores)
            {
                switch (item.Estado)
                {
                    case Estado.OK:
                        break;
                    case Estado.GPS_APAGADO:
                        this.Hablar(string.Format("{0} apagó el gepeese.", item.Codigo));
                        break;
                    case Estado.DATOS_APAGADO:
                        this.Hablar(string.Format("{0} Deshabilitó paquete de datos.", item.Codigo));
                        break;
                    case Estado.FUERA_DE_ZONA:
                        this.Hablar(string.Format("{0} Está Fuera de Zona.", item.Codigo));
                        break;
                    case Estado.DESLOGUEADO:
                        this.Hablar(string.Format("{0} Se ha deslogueado.", item.Codigo));
                        break;
                    case Estado.NO_REPORTA:
                        var ahora = DateTime.Now;
                        var fecha = DateTime.Parse(item.Fecha);
                        var timeSpan = ahora - fecha;
                        var horas = "";
                        var minutos = "";
                        if (timeSpan.Minutes > 0)
                            minutos = string.Format("{0} minutos.", timeSpan.Minutes);
                        if (timeSpan.Hours > 0)
                            if (timeSpan.Hours == 1)
                                horas = string.Format("{0} hora", timeSpan.Hours);
                            else
                                horas = string.Format("{0} horas", timeSpan.Hours);
                        var msj = String.Format("{0} no reporta hace {1},{2}", item.Codigo, horas, minutos);
                        this.Hablar(msj);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Hablar(string mensaje)
        {
            if (voz.State != SynthesizerState.Speaking)
                voz.SpeakAsync(mensaje);
            else
                colaDeMensajes.Enqueue(mensaje);
        }

        void voz_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if (colaDeMensajes.Count > 0)
                try
                {
                    voz.SpeakAsync(colaDeMensajes.Dequeue().ToString());
                }
                catch (Exception ex)
                {

                }
        }

        void actualizadorDePosiciones_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.ObtenerPosiciones();
        }
        void ObtenerPosiciones()
        {
            //        this.timer.Stop();

            App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        try
                        {
                            this.Status = "Obteniendo Posisiones...";
                            var dt = serviceMobile.ObtenerPosicionActualPreventistas();

                            foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
                            {
                                Func<object, dynamic, object> nonull = (p, def) => p == null ? def : p;
                                var elemento = new Elemento()
                                {
                                    Codigo = item.Field<string>("usuario"),
                                    Estado = (Estado)nonull(item.Field<object>("estado"), Estado.OK),
                                    Empresa = item.Field<string>("empresa"),
                                    Fecha = item.Field<object>("fecha").ToString(),
                                    CoordenadaActual = new PointLatLng(
                                                           double.Parse(item.Field<object>("latitud").ToString()),
                                                           double.Parse(item.Field<object>("longitud").ToString())
                                                           ),
                                    Visitados = (int)nonull(item.Field<object>("visitados"), 0),
                                    Compradores = (int)nonull(item.Field<object>("compradores"), 0),

                                    FondoDeCelda = item.Field<string>("empresa") == "10" ? @"h:\Sistemas\alta.jpg" : @"h:\Sistemas\hiller.jpg"
                                    //Pesos = decimal.Parse(nonull(item.Field<object>("pesos").ToString(),"0").ToString())

                                };
                                if (elemento.Codigo != null)
                                    this.AgregarActualizarVendedor(elemento);
                            }


                            switch (this.modo)
                            {
                                case ModoVerMarcadores.Nada:
                                    break;
                                case ModoVerMarcadores.Todo:
                                    this.MostrarTodo();
                                    break;
                                case ModoVerMarcadores.Seleccionado:
                                    this.MostrarMarcador(this.listaDeElementos.SelectedItem as Elemento, true);
                                    break;
                                default:
                                    break;
                            }

                            this.Status = "Ultima actualizacion: " + DateTime.Now.ToString();


                        }
                        catch (Exception exc)
                        {
                            Mensajes.Error(exc);
                            this.Status = "Error al obtener Posiciones...Reintentando";
                        }

                    });

            //        this.timer.Start();
        }

        private void AgregarActualizarVendedor(Elemento elemento)
        {
            var v = vendedores.FirstOrDefault(e => e.Codigo == elemento.Codigo);
            if (v == null)
            {
                //this.servicioPreventista = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Preventista>>();
                var prev = servicioPreventista.ObtenerPorCodigo(elemento.Codigo, Core.CargarRelaciones.NoCargarNada, "01");
                if (prev != null)
                {
                    elemento.Foto = prev.Foto;
                    elemento.Nombre = prev.Nombre;
                    elemento.CoordenadaDomicilio = new PointLatLng(prev.Latitud, prev.Longitud);
                }

                this.CalculaTiempoReporte(elemento); //establece si hace mas de 30 min si no reporta

                if (elemento.CoordenadaActual.Lat == 0 && elemento.CoordenadaActual.Lng == 0) //si lat y lng vienen 0, 0 es porque tiene gps apagado
                {
                    elemento.CoordenadaActual = new PointLatLng(-38.002452, -57.601936);
                    elemento.Estado = Estado.GPS_APAGADO;
                }

                VerificarPreventistaDentroDeZona(elemento);

                this.vendedores.Add(elemento);
            }
            else
            {
                v.Estado = elemento.Estado;
                v.Fecha = elemento.Fecha;

                this.CalculaTiempoReporte(v);

                if (elemento.CoordenadaActual.Lat == 0 && elemento.CoordenadaActual.Lng == 0)
                    elemento.Estado = Estado.GPS_APAGADO;
                else
                    v.CoordenadaActual = elemento.CoordenadaActual;

                VerificarPreventistaDentroDeZona(v);
            }
        }

        private void CalculaTiempoReporte(Elemento elemento)
        {
            var fecha = DateTime.Parse(elemento.Fecha);
            var timeSpan = DateTime.Now - fecha;
            if (timeSpan.Minutes > 30 || timeSpan.Hours > 1)
            {
                elemento.Estado = Estado.NO_REPORTA;
            }
        }

        private void listaDeElementos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = this.listaDeElementos.SelectedItem as Elemento;
            item.VerClientes = false;
            item.VerZona = false;
            item.VerTodasLasPosiciones = false;
            ControladorVendedores.CalcularBultosYPesos(item, FechaDesde, FechaHasta);
            SeleccionarMarcador(item);
            this.modo = ModoVerMarcadores.Seleccionado;
        }

        private void SeleccionarMarcador(Elemento item)
        {
            MostrarMarcador(item, true);
            map.Position = item.CoordenadaActual;
            //map.Zoom = 15;

        }

        private void MostrarMarcador(Elemento item, bool clear = false)
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

            var menuItem = new MenuItem();
            menuItem.Header = "Ver Clientes de la Ruta";
            menuItem.Command = this.CmdVerClientesPorRuta;
            menuItem.CommandParameter = item.Codigo;
            pin.Menu.Items.Add(menuItem);

            var menuMostrarZona = new MenuItem();
            menuMostrarZona.Header = "Dibujar Zona";
            menuMostrarZona.Command = this.CmdDibujarZona;
            menuMostrarZona.CommandParameter = item;
            pin.Menu.Items.Add(menuMostrarZona);

            var menuMostrarCamino = new MenuItem();
            menuMostrarCamino.Header = "Mostrar todos los reportes";
            menuMostrarCamino.Command = this.CmdVerCaminoPreventista;
            menuMostrarCamino.CommandParameter = item;
            pin.Menu.Items.Add(menuMostrarCamino);

            var menuMostrarDomicilio = new MenuItem();
            menuMostrarDomicilio.Header = "Mostrar domicilio del vendedor";
            menuMostrarDomicilio.Command = this.CmdVerDomicilioVendedor;
            menuMostrarDomicilio.CommandParameter = item;
            pin.Menu.Items.Add(menuMostrarDomicilio);

            pin.Menu.UpdateLayout();

            pin.MouseDoubleClick += pin_MouseDoubleClick;

            marcador.Shape = pin;
            marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
            this.map.Markers.Add(marcador);

            if (item.VerZona)
                this.VerZonasDelPreventista(item);
            if (item.VerClientes)
                this.VerClientes(item);
            if (item.VerTodasLasPosiciones)
                this.VerCaminoDelPreventista(item);
            if (item.VerDomicilioDelVendedor)
                this.VerDomicilioVendedor(item);

            this.RefrescarVista();
        }

        void MostrarTodo()
        {
            map.Markers.Clear();
            foreach (var item in vendedores)
            {
                item.VerClientes = false;
                item.VerZona = false;
                item.VerTodasLasPosiciones = false;
                this.MostrarMarcador(item);
            }
            //this.map.Zoom = 10;
        }

        private void RefrescarVista()
        {
            var zoomActual = this.map.Zoom;
            this.map.Zoom = 20;
            this.map.Zoom = zoomActual;
        }

        private object VerClientesDeLaRuta(object e)
        {
            if (e == null)
                return false;

            var codigoPreventista = e.ToString();
            var elemento = this.vendedores.FirstOrDefault(el => el.Codigo == codigoPreventista);
            if (elemento != null)
            {
                if (elemento.Clientes.Count == 0)
                {
                    ControladorVendedores.CargarClientes(elemento, DateTime.Today);
                }
                elemento.VerClientes = true;
                this.VerClientes(elemento);
            }
            return true;
        }

        private object VerZonasDelPreventista(object e)
        {
            Random r = new Random();
            var prev = (Elemento)e;
            prev.VerZona = true;
            try
            {
                if (prev.ZonasMapa.Count == 0) //cargo las listas de zonas y vertices si no estan cargadas
                {
                    ControladorVendedores.CargarZonas(prev, DateTime.Today);
                }
                foreach (var verticesZonas in prev.ZonasMapa) //dibujo
                {
                    var alpha = r.Next(0, byte.MaxValue + 1);
                    var red = r.Next(0, byte.MaxValue + 1);
                    var green = r.Next(0, byte.MaxValue + 1);
                    var blue = r.Next(0, byte.MaxValue + 1);
                    var brush = new SolidColorBrush(Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue));
                    this.DibujarPoligono(brush, verticesZonas.Vertices);
                }
                this.RefrescarVista();
            }
            catch (Exception ex)
            {
                Mensajes.Aviso(ex.Message);
            }
            return 0;
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

        private void DibujarPoligono(Brush color, List<Coordenada> vertices)
        {
            var poly = vertices.Select(v => new PointLatLng(v.Latitud, v.Longitud));
            ControladorZona controlZona = new ControladorZona(this.map);
            controlZona.CrearPoligono(poly, color);
        }

        private void VerClientes(Elemento vendedor)
        {
            if (vendedor.Posiciones.Count == 0)
                ControladorVendedores.CargarPosiciones(vendedor, FechaDesde, FechaHasta);
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
                    marcador.Shape.IsHitTestVisible = false;
                    marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
                    pin.ToolTip = item.Codigo + "\n" + item.Nombre + "\n" + item.Domicilio;
                    this.map.Markers.Add(marcador);
                }
            }
        }

        private void VerPuntosDeReporte(string codigoPreventista, ICollection<Coordenada> coordenadas)
        {
            foreach (var item in coordenadas)
            {
                if (item.Latitud != 0 && item.Longitud != 0)
                {
                    var marcador = new GMapMarker(new PointLatLng(item.Latitud, item.Longitud));
                    var forma = new Ellipse() { Height = 10, Width = 10 };
                    forma.Fill = Brushes.OrangeRed;
                    marcador.Shape = forma;
                    marcador.Shape.IsHitTestVisible = false;
                    marcador.Offset = new Point(-forma.Width / 2, -forma.Height);
                    this.map.Markers.Add(marcador);
                }
            }
        }

        private void VerPuntosDeReporte(string codigoPreventista, ICollection<PointLatLng> coordenadas)
        {
            foreach (var item in coordenadas)
            {
                if (item.Lat != 0 && item.Lng != 0)
                {
                    var marcador = new GMapMarker(item);
                    var forma = new Ellipse() { Height = 10, Width = 10 };
                    forma.Fill = Brushes.OrangeRed;
                    marcador.Shape = forma;
                    marcador.Shape.IsHitTestVisible = false;
                    marcador.Offset = new Point(-forma.Width / 2, -forma.Height);
                    this.map.Markers.Add(marcador);
                }
            }
        }

        private object VerCaminoDelPreventista(object e)
        {
            var preventista = (Elemento)e;
            preventista.VerTodasLasPosiciones = true;
            try
            {
                ControladorVendedores.CargarPosiciones(preventista, FechaDesde, FechaHasta);

                this.DibujarRuta(Brushes.Red, preventista.Posiciones.Select(p => p.Coordenada).ToList());
                this.VerPuntosDeReporte(preventista.Codigo, preventista.Posiciones.Select(p => p.Coordenada).ToList());

                this.RefrescarVista();
            }
            catch (Exception exc)
            {

            }
            return true;
        }

        private object VerificarPreventistaDentroDeZona(object e)
        {
            if (e != null)
            {
                var vendedor = (Elemento)e;

                if (vendedor.ZonasMapa.Count == 0)
                {
                    ControladorVendedores.CargarZonas(vendedor, DateTime.Today);
                }
                var countZonas = vendedor.ZonasMapa.Count;
                var cantFueraZona = 0;
                foreach (var zona in vendedor.ZonasMapa)
                {
                    var poly = new List<PointLatLng>();
                    foreach (var vertice in zona.Vertices)
                    {
                        poly.Add(new PointLatLng(vertice.Latitud, vertice.Longitud));
                    }
                    var res = this.DentroDeZona(poly, vendedor.CoordenadaActual.Lat, vendedor.CoordenadaActual.Lng);
                    if (!res)
                        cantFueraZona++;
                }
                if (countZonas > 0)
                    if (cantFueraZona == countZonas)
                        vendedor.Estado = Estado.FUERA_DE_ZONA;
            }

            return 0;

        }

        /// <summary>
        /// Devuelve true si el punto se encuentra dentro del poligono
        /// </summary>
        /// <param name="poligonoZona"></param>
        /// <param name="ubicacionActualVendedor"></param>
        /// <returns></returns>
        private bool DentroDeZona(List<PointLatLng> poligonoZona, double latitud, double longitud)
        {
            //  Globals which should be set before calling this function:
            //
            //  int    polyCorners  =  how many corners the polygon has (no repeats)
            //  float  polyX[]      =  horizontal coordinates of corners
            //  float  polyY[]      =  vertical coordinates of corners
            //  float  x, y         =  point to be tested
            //
            //  (Globals are used in this example for purposes of speed.  Change as
            //  desired.)
            //
            //  The function will return YES if the point x,y is inside the polygon, or
            //  NO if it is not.  If the point is exactly on the edge of the polygon,
            //  then the function may return YES or NO.
            //
            //  Note that division by zero is avoided because the division is protected
            //  by the "if" clause which surrounds it.

            var polyCorners = poligonoZona.Count;
            var polyY = poligonoZona.Select(v => v.Lng).ToArray();
            var polyX = poligonoZona.Select(v => v.Lat).ToArray();
            var y = longitud;
            var x = latitud;
            int i, j = polyCorners - 1;
            bool oddNodes = false;

            for (i = 0; i < polyCorners; i++)
            {
                if ((polyY[i] < y && polyY[j] >= y || polyY[j] < y && polyY[i] >= y) && (polyX[i] <= x || polyX[j] <= x))
                {
                    if (polyX[i] + (y - polyY[i]) / (polyY[j] - polyY[i]) * (polyX[j] - polyX[i]) < x)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        private void DibujarRuta(Brush color, List<Coordenada> vertices)
        {
            var poly = vertices.Select(v => new PointLatLng(v.Latitud, v.Longitud));
            ControladorZona controlZona = new ControladorZona(this.map);
            controlZona.CrearRuta(poly, color);
        }

        private void DibujarRuta(Brush color, List<PointLatLng> vertices)
        {
            //var poly = vertices.Select(v => new PointLatLng(v.Latitud, v.Longitud));
            ControladorZona controlZona = new ControladorZona(this.map);
            controlZona.CrearRuta(vertices, color);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.voz.Dispose();
            this.actualizadorDePosiciones.Dispose();
            this.locutor.Dispose();
        }

        private void btnVerTodos_Click(object sender, RoutedEventArgs e)
        {
            this.MostrarTodo();
            this.modo = ModoVerMarcadores.Todo;
        }

        private void btnActualizaPosicion_Click(object sender, RoutedEventArgs e)
        {
            this.ObtenerPosiciones();
        }

        private void btnSilenciar_Click(object sender, RoutedEventArgs e)
        {
            if (voz.State == SynthesizerState.Speaking)
            {
                voz.Pause();
                EtiquetaBotonSilenciar = "Reanudar reporte";
            }
            else
            {
                this.colaDeMensajes.Clear();
                voz.Resume();
                EtiquetaBotonSilenciar = "Silenciar";
            }
        }

        void pin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var elemento = (sender as PinRojo).Tag as Elemento;
            MessageBox.Show(elemento.CoordenadaActual.ToString());
        }

        private void mnuHistorial_Click(object sender, RoutedEventArgs e)
        {
            var historial = new Historial();
            historial.Show();
        }

        private void mnuInformePrimerasVisitas_Click(object sender, RoutedEventArgs e)
        {
            var itemsPrimerReportePreventistas = new List<Tuple<string, string, string, string, string, string>>();

            GeoCoderStatusCode status;

            foreach (var item in this.vendedores)
            {
                var codigo = item.Codigo;
                var nombre = item.Nombre;
                //var direccion = GoogleMapGeocoder.ObtenerDireccionPorCoordenadas(item.CoordenadaActual, out status);
                var clienteVisitadoHora = this.ConsultarPrimerClienteVisitado(codigo);
                var hora = "";
                var primerclientevisitado = "";
                if (clienteVisitadoHora != null)
                {
                    primerclientevisitado = clienteVisitadoHora.Item1;
                    hora = clienteVisitadoHora.Item2;
                }
                var zonas = item.ZonasClienteParaGrilla;
                var primerclientequeteniaquevisitar = this.ConsultarPrimerClienteAVisitar(item);
                itemsPrimerReportePreventistas.Add(new Tuple<string, string, string, string, string, string>(codigo, nombre, hora, zonas, primerclientevisitado, primerclientequeteniaquevisitar));
                //sb.AppendLine(codigo + " | " + nombre + " | " + direccion + " | " + DateTime.Parse(hora).TimeOfDay.ToString(@"hh\:mm") + " |" + primerclientevisitado + " | " + primerclientequeteniaquevisitar);
            }
            //System.IO.File.WriteAllText(@"d:\WriteText.txt", sb.ToString());

            this.CrearReportePrimeraVisitaPreventista(itemsPrimerReportePreventistas);
        }

        private void CrearReportePrimeraVisitaPreventista(List<Tuple<string, string, string, string, string, string>> itemsPrimerReportePreventistas)
        {
            var table1 = new Table();

            // Set some global formatting properties for the table.
            table1.CellSpacing = 4;
            table1.Background = Brushes.White;

            // Create 6 columns and add them to the table's Columns collection.
            //int numberOfColumns = 6;
            //for (int x = 0; x < numberOfColumns; x++)
            //{
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //CODIGO
            table1.Columns.Add(new TableColumn() { Width = new GridLength(140) }); //CLIENTE
            table1.Columns.Add(new TableColumn() { Width = new GridLength(200) }); //ZONAS
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //1ER CLIENTE VISITADO
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //1ER CLIENTE DE RUTA
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //HORA

            //}

            // Create and add an empty TableRowGroup to hold the table's Rows.
            table1.RowGroups.Add(new TableRowGroup());

            //TITULO
            table1.RowGroups[0].Rows.Add(new TableRow());
            // Alias the current working row for easy reference.
            TableRow currentRow = table1.RowGroups[0].Rows[0];
            // Global formatting for the title row.
            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 25;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = System.Windows.FontWeights.Bold;
            // Add the header row with content, 
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("INFORME DE PRIMERAS VISITAS"))) { TextAlignment = TextAlignment.Center }); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;

            //SUBTITULO
            // Add the second (header) row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[1];
            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("\nReporte generado: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm") + "\n")))); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;

            //table1.RowGroups[0].Rows.Add(new TableRow());
            //currentRow = table1.RowGroups[0].Rows[2];
            //// Global formatting for the header row.
            //currentRow.FontSize = 18;
            //currentRow.FontFamily = new FontFamily("Sans Serif");
            //currentRow.FontWeight = FontWeights.Bold;
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(FechaDelReporte)))); //POSICION 0 en ARRAY
            //currentRow.Cells[0].ColumnSpan = 4;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[2];
            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("CODIGO"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("NOMBRE"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ZONAS"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("1ER CLIENTE DE RUTA"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("1ER CLIENTE VISITADO"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("HORA"))) { TextAlignment = TextAlignment.Center });

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[3];
            string cod = "";
            string cli = "";
            string hora = "";
            string zonas = "";
            string clienteVisitado = "";
            string clienteAVisitar = "";

            for (int i = 0; i < itemsPrimerReportePreventistas.Count; i++)
            {
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[i + 3];
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;
                currentRow.FontFamily = new FontFamily("Sans Serif");
                if (i % 2 == 0)
                    currentRow.Background = Brushes.AliceBlue;
                cod = itemsPrimerReportePreventistas[i].Item1;
                cli = itemsPrimerReportePreventistas[i].Item2;
                hora = itemsPrimerReportePreventistas[i].Item3;
                zonas = itemsPrimerReportePreventistas[i].Item4;
                clienteVisitado = itemsPrimerReportePreventistas[i].Item5;
                clienteAVisitar = itemsPrimerReportePreventistas[i].Item6;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(cod))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(cli))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(zonas))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(clienteAVisitar))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(clienteVisitado))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(hora))) { TextAlignment = TextAlignment.Center });
            }

            //table1.RowGroups[0].Rows.Add(new TableRow());
            //currentRow = table1.RowGroups[0].Rows[itemsPrimerReportePreventistas.Count + 3];
            //// Global formatting for the header row.
            //currentRow.FontSize = 16;
            //currentRow.FontFamily = new FontFamily("Sans Serif");
            //currentRow.FontWeight = FontWeights.Bold;
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("(*) Último registro de información.")))); //POSICION 0 en ARRAY
            //currentRow.Cells[0].ColumnSpan = 6;


            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() != true)
                return;

            var fd = new FlowDocument(table1);
            fd.PageHeight = p.PrintableAreaHeight;
            fd.PageWidth = p.PrintableAreaWidth;
            fd.PagePadding = new Thickness(25);

            fd.ColumnGap = 0;

            fd.ColumnWidth = (fd.PageWidth -
                           fd.ColumnGap -
                           fd.PagePadding.Left -
                           fd.PagePadding.Right);


            p.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "Primeras visitas de " + DateTime.Now.ToString("yyyy'-'MM'-'dd"));
        }

        private Tuple<string, string> ConsultarPrimerClienteVisitado(string codigo)
        {
            string codigoClienteVisitado = "-----";
            string horaDeVisita = "";
            try
            {
                var dt = this.serviceMobile.ObtenerPrimerClienteVisitado(codigo, DateTime.Today.AddHours(8).AddMinutes(30), DateTime.Today.AddDays(1));

                foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("cliente") != ""))
                {
                    codigoClienteVisitado = item.Field<string>("cliente");
                    horaDeVisita = item.Field<DateTime>("fecha").TimeOfDay.ToString(@"hh\:mm");
                }

            }
            catch (Exception exc)
            {

            }

            return new Tuple<string, string>(codigoClienteVisitado, horaDeVisita);
        }

        private string ConsultarPrimerClienteAVisitar(Elemento item)
        {
            string codigoClienteAVistar = "SIN-RUTA";
            foreach (var zona in item.ZonasMapa)
            {
                try
                {
                    using (var oleDbConnection1 = new OleDbConnection("Provider=VFPOLEDB.1 ;Data Source=////server//work//preventa//datos//truesoft.dbc"))
                    {
                        var command = oleDbConnection1.CreateCommand();
                        command.CommandType = CommandType.Text;
                        command.CommandText = string.Format(@"select cliente from config_zona where zona='{0}' and recorrido = 1 and baja=0", zona.Codigo);
                        oleDbConnection1.Open();
                        var codZona = (string)command.ExecuteScalar();
                        if (codZona != null)
                            codigoClienteAVistar = codZona;
                        oleDbConnection1.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return codigoClienteAVistar;
        }

        private void mnuInformeUltimasPosiciones_Click(object sender, RoutedEventArgs e)
        {
            var itemsInformeActualPosicionesPreventistas = new List<Tuple<string, string, string, string, string>>();

            GeoCoderStatusCode status;

            foreach (var item in this.vendedores)
            {
                var codigo = item.Codigo;
                var nombre = item.Nombre;
                var direccion = GoogleMapGeocoder.ObtenerDireccionPorCoordenadas(item.CoordenadaActual, out status);
                var hora = item.Fecha;
                var zonasAsignadas = item.ZonasClienteParaGrilla;
                itemsInformeActualPosicionesPreventistas.Add(new Tuple<string, string, string, string, string>(codigo, nombre, direccion, DateTime.Parse(hora).TimeOfDay.ToString(@"hh\:mm"), zonasAsignadas));
            }
            this.CrearInformeActualPosiconesPreventistas(itemsInformeActualPosicionesPreventistas);
        }

        private void CrearInformeActualPosiconesPreventistas(List<Tuple<string, string, string, string, string>> itemsInformeActualPosicionesPreventistas)
        {
            var table1 = new Table();

            table1.CellSpacing = 4;
            table1.Background = Brushes.White;

            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //CODIGO
            table1.Columns.Add(new TableColumn() { Width = new GridLength(140) }); //CLIENTE
            table1.Columns.Add(new TableColumn() { Width = new GridLength(200) }); //UBICACION
            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //HORA
            table1.Columns.Add(new TableColumn() { Width = new GridLength(200) }); //ZONAS

            table1.RowGroups.Add(new TableRowGroup());

            //TITULO
            table1.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table1.RowGroups[0].Rows[0];
            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 25;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = System.Windows.FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("INFORME ACTUAL DE POSICIONES"))) { TextAlignment = TextAlignment.Center }); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;

            //SUBTITULO
            // Add the second (header) row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[1];
            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("\nReporte generado: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm") + "\n")))); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[2];
            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("CODIGO"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("NOMBRE"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("UBICACION (*)"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("HORA (**)"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ZONAS ASIGNADAS"))) { TextAlignment = TextAlignment.Center });

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[3];
            string cod = "";
            string cli = "";
            string ubicac = "";
            string hora = "";
            string zonasAsignadas = "";

            for (int i = 0; i < itemsInformeActualPosicionesPreventistas.Count; i++)
            {
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[i + 3];
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;
                currentRow.FontFamily = new FontFamily("Sans Serif");
                if (i % 2 == 0)
                    currentRow.Background = Brushes.AliceBlue;
                cod = itemsInformeActualPosicionesPreventistas[i].Item1;
                cli = itemsInformeActualPosicionesPreventistas[i].Item2;
                ubicac = itemsInformeActualPosicionesPreventistas[i].Item3;
                hora = itemsInformeActualPosicionesPreventistas[i].Item4;
                zonasAsignadas = itemsInformeActualPosicionesPreventistas[i].Item5;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(cod))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(cli))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(ubicac))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(hora))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(zonasAsignadas))) { TextAlignment = TextAlignment.Center });
            }

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[itemsInformeActualPosicionesPreventistas.Count + 3];
            // Global formatting for the header row.
            currentRow.FontSize = 16;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("(*) Ubicación aproximada en base a las coordenadas reportadas por el teléfono del vendedor.\n(**) Último registro de información.")))); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;


            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() != true)
                return;

            var fd = new FlowDocument(table1);
            fd.PageHeight = p.PrintableAreaHeight;
            fd.PageWidth = p.PrintableAreaWidth;
            fd.PagePadding = new Thickness(25);

            fd.ColumnGap = 0;

            fd.ColumnWidth = (fd.PageWidth -
                           fd.ColumnGap -
                           fd.PagePadding.Left -
                           fd.PagePadding.Right);


            p.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "Informe de Posiciones - " + DateTime.Now.ToString("yyyy'-'MM'-'dd'- 'HH mm"));
        }

        private void btnchkVerAlta_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.vendedores)
            {
                if (item.Empresa == "10")
                    item.Visible = true;
                else
                    item.Visible = false;
            }
        }

        private void btnchkVerHiller_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.vendedores)
            {
                if (item.Empresa == "01")
                    item.Visible = true;
                else
                    item.Visible = false;
            }
        }

        private void btnchkVerTODO_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.vendedores)
            {
                item.Visible = true;
            }
        }

        private void mnuAsistenciasAlta_Click(object sender, RoutedEventArgs e)
        {
            var presentesAlta = new List<Tuple<string, string, string>>(); //codigo, nombre, zona

            var dt = serviceMobile.ObtenerPreventistasPorEmpresa("10");
            var todosLosVendedoresAlta = new List<string>();

            foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
            {
                todosLosVendedoresAlta.Add(item.Field<string>("usuario"));
            }

            var vendedoresPresentesAlta = this.vendedores.Where(v => v.Empresa == "10").ToList();

            //var sb = new StringBuilder();
            //foreach (var item in todosLosVendedoresAlta)
            //{
            //    sb.AppendLine(item);
            //}
            //System.IO.File.WriteAllText(@"d:\PreventistasAlta.txt", sb.ToString());

            var ausentesAlta = new List<Tuple<string, string>>();
            foreach (var item in todosLosVendedoresAlta)
            {
                if (vendedoresPresentesAlta.Any(v => v.Codigo == item))
                {
                    var vend = vendedoresPresentesAlta.Find(v => v.Codigo == item);
                    presentesAlta.Add(new Tuple<string, string, string>(vend.Codigo, vend.Nombre, vend.ZonasClienteParaGrilla));
                }
                else
                {
                    var vend = servicioPreventista.ObtenerPorCodigo(item, Core.CargarRelaciones.NoCargarNada, "01");
                    if (vend != null)
                    {
                        ausentesAlta.Add(new Tuple<string, string>(vend.Codigo, vend.Nombre));
                    }
                }
            }

            this.CrearInformeAsistencias(presentesAlta, ausentesAlta, "ALTA DISTRIBUCION SA");
        }

        private void mnuAsistenciasHiller_Click(object sender, RoutedEventArgs e)
        {
            var presentesHiller = new List<Tuple<string, string, string>>(); //codigo, nombre, zona

            var dt = serviceMobile.ObtenerPreventistasPorEmpresa("01");
            var todosLosVendedoresHiller = new List<string>();

            foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
            {
                todosLosVendedoresHiller.Add(item.Field<string>("usuario"));
            }

            var vendedoresPresentesHiller = this.vendedores.Where(v => v.Empresa == "01").ToList();

            //var sb = new StringBuilder();
            //foreach (var item in todosLosVendedoresHiller)
            //{
            //    sb.AppendLine(item);
            //}
            //System.IO.File.WriteAllText(@"d:\PreventistasHiller.txt", sb.ToString());

            var ausentesHiller = new List<Tuple<string, string>>();
            foreach (var item in todosLosVendedoresHiller)
            {
                if (vendedoresPresentesHiller.Any(v => v.Codigo == item))
                {
                    var vend = vendedoresPresentesHiller.Find(v => v.Codigo == item);
                    presentesHiller.Add(new Tuple<string, string, string>(vend.Codigo, vend.Nombre, vend.ZonasClienteParaGrilla));
                }
                else
                {
                    var vend = servicioPreventista.ObtenerPorCodigo(item, Core.CargarRelaciones.NoCargarNada, "01");
                    if (vend != null)
                    {
                        ausentesHiller.Add(new Tuple<string, string>(vend.Codigo, vend.Nombre));
                    }
                }
            }

            this.CrearInformeAsistencias(presentesHiller, ausentesHiller, "HILLER SA");
        }


        private void CrearInformeAsistencias(List<Tuple<string, string, string>> presentes, List<Tuple<string, string>> ausentes, string empresa)
        {
            var table1 = new Table();



            table1.CellSpacing = 4;
            table1.Background = Brushes.White;

            table1.Columns.Add(new TableColumn() { Width = new GridLength(80) }); //CODIGO
            table1.Columns.Add(new TableColumn() { Width = new GridLength(140) }); //NOMBRE
            table1.Columns.Add(new TableColumn() { Width = new GridLength(250) }); //ZONAS
            table1.Columns.Add(new TableColumn() { Width = new GridLength(230) }); //ZONAS


            table1.RowGroups.Add(new TableRowGroup());

            //var ultimaFila = table1.RowGroups[0].Rows.Count;

            //TITULO
            table1.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 25;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = System.Windows.FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("INFORME DE ASISTENCIAS DE " + empresa))) { TextAlignment = TextAlignment.Center }); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;

            //SUBTITULO
            // Add the second (header) row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("\nReporte generado: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm"))))); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;

            //SUBTITULO 2
            // Add the second (header) row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("\nPRESENTES\n")) { TextAlignment = TextAlignment.Center })); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("CODIGO"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("NOMBRE"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ZONAS ASIGNADAS"))) { TextAlignment = TextAlignment.Center });

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
            string cod = "";
            string nombre = "";
            string zonasAsignadas = "";

            for (int i = 0; i < presentes.Count; i++)
            {
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;
                currentRow.FontFamily = new FontFamily("Sans Serif");
                if (i % 2 == 0)
                    currentRow.Background = Brushes.AliceBlue;
                cod = presentes[i].Item1;
                nombre = presentes[i].Item2;
                zonasAsignadas = presentes[i].Item3;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(cod))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(nombre))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(zonasAsignadas))) { TextAlignment = TextAlignment.Center });
            }

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
            // Global formatting for the header row.
            currentRow.FontSize = 16;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("\nAUN NO REPORTAN\n")) { TextAlignment = TextAlignment.Center })); //POSICION 0 en ARRAY
            currentRow.Cells[0].ColumnSpan = table1.Columns.Count;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.FontFamily = new FontFamily("Sans Serif");
            currentRow.FontWeight = FontWeights.Bold;
            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("CODIGO"))) { TextAlignment = TextAlignment.Center });
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("NOMBRE"))) { TextAlignment = TextAlignment.Center });

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
            //string cod = "";
            //string nombre = "";
            //string zonasAsignadas = "";

            for (int i = 0; i < ausentes.Count; i++)
            {
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[table1.RowGroups[0].Rows.Count - 1];
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;
                currentRow.FontFamily = new FontFamily("Sans Serif");
                if (i % 2 == 0)
                    currentRow.Background = Brushes.AliceBlue;
                cod = ausentes[i].Item1;
                nombre = ausentes[i].Item2;
                //zonasAsignadas = presentes[i].Item3;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(cod))) { TextAlignment = TextAlignment.Center });
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(nombre))) { TextAlignment = TextAlignment.Center });
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(zonasAsignadas))) { TextAlignment = TextAlignment.Center });
            }


            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() != true)
                return;

            var fd = new FlowDocument(table1);
            fd.PageHeight = p.PrintableAreaHeight;
            fd.PageWidth = p.PrintableAreaWidth;
            fd.PagePadding = new Thickness(25);

            fd.ColumnGap = 0;

            fd.ColumnWidth = (fd.PageWidth -
                           fd.ColumnGap -
                           fd.PagePadding.Left -
                           fd.PagePadding.Right);


            p.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "Asistencias " + empresa + " - " + DateTime.Now.ToString("yyyy'-'MM'-'dd'- 'HH mm"));
        }



        private void mnuSubirKml_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".kml";
            dlg.Filter = "Archivo KML (*.kml)|*.kml";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result.HasValue && result.Value)
            {
                // Open document 
                string filename = dlg.FileName;
                //textBox1.Text = filename;
            }
        }

        private void btnFiltrarPorCodigo_Click(object sender, RoutedEventArgs e)
        {
            this.FiltroDeLista = "Codigo";
        }

        private void btnFiltrarPorNombre_Click(object sender, RoutedEventArgs e)
        {
            this.FiltroDeLista = "Nombre";
        }

        private void mnuActualizarClientes_Click(object sender, RoutedEventArgs e)
        {
            ControladorVendedores.ActualizarCoordenadasClientes();
        }

        private void mnuZonas_Click(object sender, RoutedEventArgs e)
        {
            var vistaZonas = new VisualizadorDeZonas();
            vistaZonas.Show();
        }
    }

    public enum ModoVerMarcadores
    {
        Nada,
        Todo,
        Seleccionado
    }
}