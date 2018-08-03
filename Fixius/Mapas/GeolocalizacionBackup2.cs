//using Fixius;
//using GMap.NET;
////using GMap.NET.WindowsForms;
//using GMap.NET.WindowsPresentation;
//using Inteldev.Core.Contratos;
//using Inteldev.Core.DTO.Locacion;
//using Inteldev.Core.Presentacion;
//using Inteldev.Core.Presentacion.ClienteServicios;
//using Inteldev.Core.Presentacion.Comandos;
//using Inteldev.Core.Presentacion.Controladores;
//using Inteldev.Fixius.Contratos;
//using Inteldev.Fixius.Mapas.Pines;
//using Inteldev.Fixius.ServiceMobile;
//using Inteldev.Fixius.Servicios.DTO.Preventa;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Data;
//using System.Globalization;
//using System.Linq;
//using System.Speech.Synthesis;
//using System.Text;
//using System.Timers;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace Inteldev.Fixius.Mapas
//{
//    /// <summary>
//    /// Interaction logic for Geolocalizacion.xaml
//    /// </summary>
//    public partial class Geolocalizacion : Window
//    {
//        #region Props

//        Timer actualizadorDePosiciones;
//        Timer locutor;

//        ModoVerMarcadores modo;

//        public ObservableCollection<Elemento> vendedores { get; set; }

//        Queue<String> colaDeMensajes;

//        SpeechSynthesizer voz = new SpeechSynthesizer();

//        #endregion

//        #region Prop Dp's

//        public string Status
//        {
//            get { return (string)GetValue(StatusProperty); }
//            set { SetValue(StatusProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty StatusProperty =
//            DependencyProperty.Register("Status", typeof(string), typeof(Geolocalizacion), new PropertyMetadata(string.Empty));

//        public string EtiquetaBotonSilenciar
//        {
//            get { return (string)GetValue(EtiquetaBotonSilenciarProperty); }
//            set { SetValue(EtiquetaBotonSilenciarProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for EtiquetaBotonSilenciar.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty EtiquetaBotonSilenciarProperty =
//            DependencyProperty.Register("EtiquetaBotonSilenciar", typeof(string), typeof(Geolocalizacion), new PropertyMetadata("Silenciar"));


//        #endregion

//        #region Servicios
//        IServicioABM<Preventista> servicioPreventista;
//        IServicioCoordenadasClientes servicioCoordenadaCliente;
//        IServicioRutaDeVenta servicioRutaVenta;

//        ServiceSoapClient serviceMobile = new ServiceSoapClient();
//        #endregion

//        #region Commands
//        public ICommand CmdVerClientesPorRuta { get; set; }
//        public ICommand CmdDibujarZona { get; set; }
//        public ICommand CmdVerCaminoPreventista { get; set; }
//        public ICommand CmdVerificarPreventistaDentroDeZona { get; set; }
//        #endregion

//        public Geolocalizacion()
//        {
//            InitializeComponent();
//            this.servicioPreventista = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Inteldev.Fixius.Servicios.DTO.Preventa.Preventista>>();
//            this.servicioCoordenadaCliente = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioCoordenadasClientes>("ServicioCoordenadasClientes");
//            this.servicioRutaVenta = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioRutaDeVenta>("ServicioRutaDeVenta");

//            this.CmdVerClientesPorRuta = new RelayCommand(e => this.VerClientesDeLaRuta(e));
//            this.CmdDibujarZona = new RelayCommand(e => this.VerZonasDelPreventista(e));
//            this.CmdVerCaminoPreventista = new RelayCommand(e => this.VerCaminoDelPreventista(e));
//            this.CmdVerificarPreventistaDentroDeZona = new RelayCommand(e => this.VerificarPreventistaDentroDeZona(e));

//            this.modo = ModoVerMarcadores.Nada;
//            this.vendedores = new ObservableCollection<Elemento>();
//            this.colaDeMensajes = new Queue<string>();
//            this.DataContext = this;

//            GeoCoderStatusCode status;
//            map.Position = GoogleMapGeocoder.ObtenerCordenadasPorDireccion("Mar del Plata, Buenos Aires", out status);

//            this.listaDeElementos.SelectionChanged += listaDeElementos_SelectionChanged;

//            this.actualizadorDePosiciones = new Timer(45000);
//            this.actualizadorDePosiciones.Elapsed += actualizadorDePosiciones_Elapsed;

//            this.locutor = new Timer(30000);
//            this.locutor.Elapsed += locutor_Elapsed;

//            this.actualizadorDePosiciones.Start();
//            this.locutor.Start();

//            this.voz.SpeakCompleted += voz_SpeakCompleted;

//            this.ObtenerPosiciones();
//        }

//        void voz_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
//        {
//            if (colaDeMensajes.Count > 0)
//                try
//                {
//                    voz.SpeakAsync(colaDeMensajes.Dequeue().ToString());
//                }
//                catch (Exception ex)
//                {

//                }
//        }

//        void locutor_Elapsed(object sender, ElapsedEventArgs e)
//        {
//            foreach (var item in vendedores)
//            {
//                switch (item.Estado)
//                {
//                    case Estado.OK:
//                        break;
//                    case Estado.GPS_APAGADO:
//                        this.Hablar(string.Format("¡Atención! {0} apagó el gepeese.", item.Codigo));
//                        break;
//                    case Estado.DATOS_APAGADO:
//                        this.Hablar(string.Format("¡Atención! {0} Deshabilitó paquete de datos.", item.Codigo));
//                        break;
//                    case Estado.FUERA_DE_ZONA:
//                        this.Hablar(string.Format("¡Atención! {0} Está Fuera de Zona.", item.Codigo));
//                        break;
//                    case Estado.DESLOGUEADO:
//                        this.Hablar(string.Format("¡Atención! {0} Se ha deslogueado.", item.Codigo));
//                        break;
//                    case Estado.NO_REPORTA:
//                        var ahora = DateTime.Now;
//                        var fecha = DateTime.Parse(item.Fecha);
//                        var timeSpan = ahora - fecha;
//                        var horas = "";
//                        var minutos = "";
//                        if (timeSpan.Minutes > 0)
//                            minutos = string.Format("{0} minutos.", timeSpan.Minutes);
//                        if (timeSpan.Hours > 0)
//                            if (timeSpan.Hours == 1)
//                                horas = string.Format("{0} hora", timeSpan.Hours);
//                            else
//                                horas = string.Format("{0} horas", timeSpan.Hours);
//                        var msj = String.Format("¡Atención! {0} no reporta hace {1},{2}", item.Codigo, horas, minutos);
//                        this.Hablar(msj);
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }

//        private void Hablar(string mensaje)
//        {
//            if (voz.State != SynthesizerState.Speaking)
//                voz.SpeakAsync(mensaje);
//            else
//                colaDeMensajes.Enqueue(mensaje);
//        }

//        void actualizadorDePosiciones_Elapsed(object sender, ElapsedEventArgs e)
//        {
//            this.ObtenerPosiciones();
//        }
//        void ObtenerPosiciones()
//        {
//            //        this.timer.Stop();
//            App.Current.Dispatcher.Invoke((Action)delegate
//            {
//                try
//                {
//                    this.Status = "Obteniendo Posisiones...";
//                    var dt = serviceMobile.ObtenerPosicionActualPreventistas();

//                    foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
//                    {
//                        Func<object, dynamic, object> nonull = (p, def) => p == null ? def : p;
//                        var elemento = new Elemento()
//                        {
//                            Codigo = item.Field<string>("usuario"),
//                            Estado = (Estado)nonull(item.Field<object>("estado"), Estado.OK),
//                            Fecha = item.Field<object>("fecha").ToString(),
//                            CoordenadaActual = new PointLatLng(
//                                                   double.Parse(item.Field<object>("latitud").ToString()),
//                                                   double.Parse(item.Field<object>("longitud").ToString())
//                                                   ),
//                            Visitados = (int)nonull(item.Field<object>("visitados"), 0),
//                            Compradores = (int)nonull(item.Field<object>("compradores"), 0),
//                            Bultos = (int)nonull(item.Field<object>("bultos"), 0)
//                            //Pesos = decimal.Parse(nonull(item.Field<object>("pesos").ToString(),"0").ToString())

//                        };
//                        if (elemento.Codigo != null)
//                            this.AgregarActualizarVendedor(elemento);
//                    }


//                    switch (this.modo)
//                    {
//                        case ModoVerMarcadores.Nada:
//                            break;
//                        case ModoVerMarcadores.Todo:
//                            this.MostrarTodo();
//                            break;
//                        case ModoVerMarcadores.Seleccionado:
//                            this.MostrarMarcador(this.listaDeElementos.SelectedItem as Elemento, true);
//                            break;
//                        default:
//                            break;
//                    }

//                    this.Status = "Ultima actualizacion: " + DateTime.Now.ToString();


//                }
//                catch (Exception exc)
//                {
//                    Mensajes.Error(exc);
//                    this.Status = "Error al obtener Posiciones...Reintentando";
//                }

//            });

//            //        this.timer.Start();
//        }

//        private void AgregarActualizarVendedor(Elemento elemento)
//        {
//            var v = vendedores.FirstOrDefault(e => e.Codigo == elemento.Codigo);
//            if (v == null)
//            {
//                this.servicioPreventista = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Preventista>>();
//                var prev = servicioPreventista.ObtenerPorCodigo(elemento.Codigo, Core.CargarRelaciones.NoCargarNada, "01");
//                if (prev != null)
//                {
//                    elemento.Foto = prev.Foto;
//                    elemento.Nombre = prev.Nombre;
//                }
//                //cambio la lat y lng para que no sea 0.0
//                if (elemento.CoordenadaActual.Lat == 0 && elemento.CoordenadaActual.Lng == 0)
//                {
//                    elemento.CoordenadaActual = new PointLatLng(-38.002452, -57.601936);
//                    elemento.Estado = Estado.GPS_APAGADO;
//                }
//                var fecha = DateTime.Parse(elemento.Fecha);
//                var timeSpan = DateTime.Now - fecha;
//                if (timeSpan.Minutes > 30 || timeSpan.Hours > 1)
//                {
//                    elemento.Estado = Estado.NO_REPORTA;
//                }
//                var estado = VerificarPreventistaDentroDeZona(elemento);
//                this.vendedores.Add(elemento);
//            }
//            else
//            {
//                v.Estado = elemento.Estado;
//                v.Fecha = elemento.Fecha;

//                var fecha = DateTime.Parse(v.Fecha);
//                var timeSpan = DateTime.Now - fecha;
//                if (timeSpan.Minutes > 30 || timeSpan.Hours > 1)
//                {
//                    v.Estado = Estado.NO_REPORTA;
//                }

//                if (elemento.CoordenadaActual.Lat == 0 && elemento.CoordenadaActual.Lng == 0)
//                    elemento.Estado = Estado.GPS_APAGADO;
//                else
//                    v.CoordenadaActual = elemento.CoordenadaActual;
//                var estado = VerificarPreventistaDentroDeZona(elemento);
//            }
//        }

//        private void listaDeElementos_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            var item = this.listaDeElementos.SelectedItem as Elemento;
//            item.VerClientes = false;
//            item.VerZona = false;
//            item.VerTodasLasPosiciones = false;
//            SeleccionarMarcador(item);
//            this.modo = ModoVerMarcadores.Seleccionado;
//        }

//        private void SeleccionarMarcador(Elemento item)
//        {
//            MostrarMarcador(item, true);
//            map.Position = item.CoordenadaActual;
//            //map.Zoom = 15;

//        }

//        private void MostrarMarcador(Elemento item, bool clear = false)
//        {
//            if (clear)
//                map.Markers.Clear();

//            var marcador = new GMapMarker(item.CoordenadaActual);
//            var pin = new PinRojo();
//            pin.Tag = item;
//            pin.Etiqueta = item.Codigo;
//            //pin.Menu = new ContextMenu();

//            var menuItem = new MenuItem();
//            menuItem.Header = "Ver Clientes de la Ruta";
//            menuItem.Command = this.CmdVerClientesPorRuta;
//            menuItem.CommandParameter = item.Codigo;
//            pin.Menu.Items.Add(menuItem);

//            var menuMostrarZona = new MenuItem();
//            menuMostrarZona.Header = "Dibujar Zona";
//            menuMostrarZona.Command = this.CmdDibujarZona;
//            menuMostrarZona.CommandParameter = item;
//            pin.Menu.Items.Add(menuMostrarZona);

//            var menuMostrarCamino = new MenuItem();
//            menuMostrarCamino.Header = "Mostrar todos los reportes";
//            menuMostrarCamino.Command = this.CmdVerCaminoPreventista;
//            menuMostrarCamino.CommandParameter = item;
//            pin.Menu.Items.Add(menuMostrarCamino);

//            pin.Menu.UpdateLayout();

//            //pin.MouseDoubleClick += pin_MouseDoubleClick;

//            marcador.Shape = pin;
//            marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
//            this.map.Markers.Add(marcador);

//            if (item.VerZona)
//                this.VerZonasDelPreventista(item);
//            if (item.VerClientes)
//                this.VerClientes(item.RutaClientes);
//            if (item.VerTodasLasPosiciones)
//                this.VerCaminoDelPreventista(item);

//            this.RefrescarVista();
//        }

//        void MostrarTodo()
//        {
//            map.Markers.Clear();
//            foreach (var item in vendedores)
//            {
//                item.VerClientes = false;
//                item.VerZona = false;
//                this.MostrarMarcador(item);
//            }
//            //this.map.Zoom = 10;
//        }

//        private void RefrescarVista()
//        {
//            var zoomActual = this.map.Zoom;
//            this.map.Zoom = 20;
//            this.map.Zoom = zoomActual;
//        }

//        private object VerClientesDeLaRuta(object e)
//        {
//            if (e == null)
//                return false;

//            var codigoPreventista = e.ToString();

//            if (codigoPreventista != null && codigoPreventista != string.Empty)
//            {
//                var elemento = this.vendedores.FirstOrDefault(el => el.Codigo == codigoPreventista);
//                if (elemento != null)
//                {
//                    if (elemento.RutaClientes.Count == 0)
//                    {
//                        var preventista = this.servicioPreventista.ObtenerPorCodigo(codigoPreventista, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);

//                        var coordenadas = servicioCoordenadaCliente.ObtenerCoordenadasPorPreventista(preventista, DateTime.Today, "01");

//                        elemento.RutaClientes.AddRange(coordenadas);
//                    }
//                    elemento.VerClientes = true;
//                    this.VerClientes(elemento.RutaClientes);

//                    //var codigosderutas = this.servicioRutaVenta.ObtenerRutasDelDia(preventista, DateTime.Today);

//                    //foreach (var item in codigosderutas)
//                    //{
//                    //    var vertices = servicioRutaVenta.ObtenerVertices(item.Codigo, "01", "00BEB");

//                    //    this.DibujarPoligono(Brushes.Blue, vertices);
//                    //}
//                }
//            }
//            return true;
//        }

//        private object VerZonasDelPreventista(object e)
//        {
//            var prev = (Elemento)e;
//            prev.VerZona = true;
//            try
//            {
//                var codigosderutas = this.servicioRutaVenta.ObtenerRutasDelDia(prev.Codigo, DateTime.Today);

//                foreach (var item in codigosderutas)
//                {
//                    var vertices = servicioRutaVenta.ObtenerVertices(item.Codigo, "01", "00BEB");
//                    this.DibujarPoligono(Brushes.Blue, vertices);
//                }
//                this.RefrescarVista();
//            }
//            catch (Exception ex)
//            {
//                Mensajes.Aviso(ex.Message);
//            }
//            return 0;
//        }

//        private void DibujarPoligono(Brush color, List<Coordenada> vertices)
//        {
//            var poly = vertices.Select(v => new PointLatLng(v.Longitud, v.Latitud));
//            ControladorZona controlZona = new ControladorZona(this.map);
//            controlZona.CrearPoligono(poly, color);
//        }

//        private void VerClientes(ICollection<CoordenadaCliente> coordenadas)
//        {
//            foreach (var item in coordenadas)
//            {
//                if (item.Latitud != 0 && item.Longitud != 0)
//                {
//                    var marcador = new GMapMarker(new PointLatLng(item.Longitud, item.Latitud));

//                    var pin = new PinAzul();

//                    pin.Tag = item;
//                    pin.Etiqueta = item.Codigo;
//                    marcador.Shape = pin;
//                    marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
//                    this.map.Markers.Add(marcador);
//                }
//            }
//        }

//        private object VerCaminoDelPreventista(object e)
//        {
//            var preventista = (Elemento)e;
//            preventista.VerTodasLasPosiciones = true;
//            var fechaDesde = DateTime.Today.AddHours(8);
//            var fechaHasta = DateTime.Today.AddDays(1);
//            var pinesPrev = new List<Coordenada>();
//            try
//            {
//                var dt = serviceMobile.ObtenerPosicionesDelPreventista(preventista.Codigo, fechaDesde, fechaHasta);

//                foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
//                {
//                    var coordenada = new Coordenada()
//                    {
//                        Latitud = double.Parse(item.Field<object>("latitud").ToString()),
//                        Longitud = double.Parse(item.Field<object>("longitud").ToString())
//                    };
//                    if (coordenada.Latitud != 0 && coordenada.Longitud != 0)
//                        pinesPrev.Add(coordenada);
//                }
//                this.DibujarRuta(Brushes.Red, pinesPrev);

//                foreach (var item in pinesPrev)
//                {
//                    if (item.Latitud != 0 && item.Longitud != 0)
//                    {
//                        var marcador = new GMapMarker(new PointLatLng(item.Longitud, item.Latitud));

//                        var pin = new PinAzul();

//                        pin.Tag = preventista;
//                        marcador.Shape = pin;
//                        marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
//                        this.map.Markers.Add(marcador);
//                    }
//                }
//                this.RefrescarVista();
//            }
//            catch (Exception exc)
//            {

//            }
//            return true;
//        }

//        private object VerificarPreventistaDentroDeZona(object e)
//        {
//            App.Current.Dispatcher.Invoke((Action)delegate
//            {
//                if (e != null)
//                {
//                    var vendedor = (Elemento)e;
//                    if (vendedor.CoordenadaActual.Lat != 0 && vendedor.CoordenadaActual.Lng != 0)
//                    {
//                        if (vendedor.RutaClientes.Count == 0)
//                        {
//                            var preventista = this.servicioPreventista.ObtenerPorCodigo(vendedor.Codigo, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);
//                            vendedor.Zonas = this.servicioRutaVenta.ObtenerRutasDelDia(preventista, DateTime.Today);
//                        }
//                        var countZonas = vendedor.Zonas.Count;
//                        var cantFueraZona = 0;
//                        foreach (var zona in vendedor.Zonas)
//                        {
//                            zona.Vertices = this.servicioRutaVenta.ObtenerVertices(zona.Codigo, "01", "00BEB");
//                            var poly = new List<PointLatLng>();
//                            foreach (var vertice in zona.Vertices)
//                            {
//                                poly.Add(new PointLatLng(vertice.Latitud, vertice.Longitud));
//                            }
//                            var res = this.DentroDeZona(poly, vendedor.CoordenadaActual);
//                            if (res)
//                                break;
//                            else
//                                cantFueraZona++;

//                        }
//                        if (cantFueraZona == countZonas)
//                            vendedor.Estado = Estado.FUERA_DE_ZONA;
//                    }
//                }
//            });
//            return 0;

//        }

//        /// <summary>
//        /// Devuelve true si el punto se encuentra dentro del poligono
//        /// </summary>
//        /// <param name="poligonoZona"></param>
//        /// <param name="ubicacionActualVendedor"></param>
//        /// <returns></returns>
//        private bool DentroDeZona(List<PointLatLng> poligonoZona, PointLatLng ubicacionActualVendedor)
//        {
//            //  Globals which should be set before calling this function:
//            //
//            //  int    polyCorners  =  how many corners the polygon has (no repeats)
//            //  float  polyX[]      =  horizontal coordinates of corners
//            //  float  polyY[]      =  vertical coordinates of corners
//            //  float  x, y         =  point to be tested
//            //
//            //  (Globals are used in this example for purposes of speed.  Change as
//            //  desired.)
//            //
//            //  The function will return YES if the point x,y is inside the polygon, or
//            //  NO if it is not.  If the point is exactly on the edge of the polygon,
//            //  then the function may return YES or NO.
//            //
//            //  Note that division by zero is avoided because the division is protected
//            //  by the "if" clause which surrounds it.

//            var polyCorners = poligonoZona.Count;
//            var polyY = poligonoZona.Select(v => v.Lng).ToArray();
//            var polyX = poligonoZona.Select(v => v.Lat).ToArray();
//            var y = ubicacionActualVendedor.Lng;
//            var x = ubicacionActualVendedor.Lat;
//            int i, j = polyCorners - 1;
//            bool oddNodes = false;

//            for (i = 0; i < polyCorners; i++)
//            {
//                if ((polyY[i] < y && polyY[j] >= y || polyY[j] < y && polyY[i] >= y) && (polyX[i] <= x || polyX[j] <= x))
//                {
//                    if (polyX[i] + (y - polyY[i]) / (polyY[j] - polyY[i]) * (polyX[j] - polyX[i]) < x)
//                    {
//                        oddNodes = !oddNodes;
//                    }
//                }
//                j = i;
//            }

//            return oddNodes;
//        }

//        private void DibujarRuta(Brush color, List<Coordenada> vertices)
//        {
//            var poly = vertices.Select(v => new PointLatLng(v.Latitud, v.Longitud));
//            ControladorZona controlZona = new ControladorZona(this.map);
//            controlZona.CrearRuta(poly, color);
//        }

//        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
//        {
//            this.voz.Dispose();
//            this.actualizadorDePosiciones.Dispose();
//            this.locutor.Dispose();
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            this.MostrarTodo();
//            this.modo = ModoVerMarcadores.Todo;
//        }

//        private void Button_Click_1(object sender, RoutedEventArgs e)
//        {
//            this.ObtenerPosiciones();
//        }

//        private void Button_Click_2(object sender, RoutedEventArgs e)
//        {
//            if (voz.State == SynthesizerState.Speaking)
//            {
//                voz.Pause();
//                EtiquetaBotonSilenciar = "Reanudar reporte";
//            }
//            else
//            {
//                this.colaDeMensajes.Clear();
//                voz.Resume();
//                EtiquetaBotonSilenciar = "Silenciar";
//            }
//        }

//    }

//    public enum ModoVerMarcadores
//    {
//        Nada,
//        Todo,
//        Seleccionado
//    }
//}