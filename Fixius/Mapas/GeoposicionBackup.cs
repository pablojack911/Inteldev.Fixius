using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Mapas
{
    class GeoposicionBackup
    {
        //#region Props

        //    public ObservableCollection<Elemento> elementos { get; set; }

        //    Timer timer;
        //    ModoVerMarcadores modo;
        //    SpeechSynthesizer voz = new SpeechSynthesizer();

        //    #region Prop Dp's

        //    public string Status
        //    {
        //        get { return (string)GetValue(StatusProperty); }
        //        set { SetValue(StatusProperty, value); }
        //    }

        //    // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        //    public static readonly DependencyProperty StatusProperty =
        //        DependencyProperty.Register("Status", typeof(string), typeof(Geolocalizacion), new PropertyMetadata(string.Empty));

        //    public string EtiquetaBotonSilenciar
        //    {
        //        get { return (string)GetValue(EtiquetaBotonSilenciarProperty); }
        //        set { SetValue(EtiquetaBotonSilenciarProperty, value); }
        //    }

        //    // Using a DependencyProperty as the backing store for EtiquetaBotonSilenciar.  This enables animation, styling, binding, etc...
        //    public static readonly DependencyProperty EtiquetaBotonSilenciarProperty =
        //        DependencyProperty.Register("EtiquetaBotonSilenciar", typeof(string), typeof(Geolocalizacion), new PropertyMetadata("Silenciar"));


        //    #endregion

        //    #region Servicios
        //    IServicioABM<Preventista> servicioperventista;
        //    IServicioCoordenadasClientes servicioruta;
        //    IServicioRutaDeVenta servicioRutaVenta;

        //    ServiceSoapClient serviceMobile = new ServiceSoapClient();
        //    #endregion

        //    #region Commands
        //    public ICommand CmdVerClientesPorRuta { get; set; }
        //    public ICommand CmdDibujarZona { get; set; }
        //    public ICommand CmdVerCaminoPreventista { get; set; }
        //    #endregion

        //    #endregion

        //    public Geolocalizacion()
        //    {
        //        InitializeComponent();
        //        this.servicioperventista = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Inteldev.Fixius.Servicios.DTO.Preventa.Preventista>>();
        //        this.servicioruta = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioCoordenadasClientes>("ServicioCoordenadasClientes");
        //        this.servicioRutaVenta = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioRutaDeVenta>("ServicioRutaDeVenta");

        //        this.CmdVerClientesPorRuta = new RelayCommand(e => this.VerClientesDeLaRuta(e));
        //        this.CmdDibujarZona = new RelayCommand(e => this.VerZonasDelPreventista(e));
        //        this.CmdVerCaminoPreventista = new RelayCommand(e => this.VerCaminoDelPreventista(e));

        //        this.modo = ModoVerMarcadores.Nada;
        //        this.elementos = new ObservableCollection<Elemento>();
        //        this.DataContext = this;

        //        GeoCoderStatusCode status;
        //        map.Position = GoogleMapGeocoder.ObtenerCordenadasPorDireccion("Mar del Plata, Buenos Aires", out status);

        //        this.listaDeElementos.SelectionChanged += listaDeElementos_SelectionChanged;

        //        timer = new Timer(60000);
        //        timer.Elapsed += (d, s) => ObtenerPosiciones();
        //        voz.SpeakCompleted += voz_SpeakCompleted;
        //        ObtenerPosiciones();

        //    }

        //    private object VerCaminoDelPreventista(object e)
        //    {
        //        var preventista = (Elemento)e;
        //        var fecha = DateTime.Today;
        //        var pinesPrev = new List<Coordenada>();
        //        try
        //        {
        //            //this.Status = "Obteniendo Posisiones...";
        //            var dt = serviceMobile.ObtenerPosicionesDelPreventista(preventista.Codigo, fecha);

        //            foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
        //            {
        //                //Func<object, dynamic, object> nonull = (p, def) => p == null ? def : p;
        //                //var elemento = new Elemento()
        //                //{
        //                //    Codigo = item.Field<string>("usuario"),
        //                //    Estado = (Estado)nonull(item.Field<object>("estado"), Estado.OK),
        //                //    Fecha = item.Field<object>("fecha").ToString(),
        //                //    CoordenadaActual = new PointLatLng(
        //                //                           double.Parse(item.Field<object>("latitud").ToString()),
        //                //                           double.Parse(item.Field<object>("longitud").ToString())
        //                //                           ),
        //                //    Visitados = (int)nonull(item.Field<object>("visitados"), 0),
        //                //    Compradores = (int)nonull(item.Field<object>("compradores"), 0),
        //                //    Bultos = (int)nonull(item.Field<object>("bultos"), 0)
        //                //    //Pesos = decimal.Parse(nonull(item.Field<object>("pesos").ToString(),"0").ToString())

        //                //};
        //                var coordenada = new Coordenada()
        //                {
        //                    Latitud = double.Parse(item.Field<object>("latitud").ToString()),
        //                    Longitud = double.Parse(item.Field<object>("longitud").ToString())
        //                };
        //                if (coordenada.Latitud != 0 && coordenada.Longitud != 0)
        //                    pinesPrev.Add(coordenada);
        //            }
        //            this.DibujarRuta(Brushes.Red, pinesPrev);
        //            foreach (var item in pinesPrev)
        //            {
        //                if (item.Latitud != 0 && item.Longitud != 0)
        //                {
        //                    var marcador = new GMapMarker(new PointLatLng(item.Longitud, item.Latitud));

        //                    var pin = new PinAzul();

        //                    pin.Tag = preventista;
        //                    marcador.Shape = pin;
        //                    marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
        //                    this.map.Markers.Add(marcador);
        //                }
        //            }

        //            //switch (this.modo)
        //            //{
        //            //    case ModoVerMarcadores.Nada:
        //            //        break;
        //            //    case ModoVerMarcadores.Todo:
        //            //        this.MostrarTodo();
        //            //        break;
        //            //    case ModoVerMarcadores.Seleccionado:
        //            //        this.MostrarMarcador(this.listaDeElementos.SelectedItem as Elemento, true);
        //            //        break;
        //            //    default:
        //            //        break;
        //            //}

        //            //this.Status = "Ultima actualizacion: " + DateTime.Now.ToString();

        //            this.RefrescarVista();
        //        }
        //        catch (Exception exc)
        //        {
        //            //Mensajes.Error(exc);
        //            //this.Status = "Error al obtener Posiciones...Reintentando";
        //        }
        //        return true;
        //    }
        //    private object VerClientesDeLaRuta(object e)
        //    {
        //        if (e == null)
        //            return false;

        //        var codigoPreventista = e.ToString();

        //        if (codigoPreventista != null && codigoPreventista != string.Empty)
        //        {
        //            var elemento = this.elementos.FirstOrDefault(el => el.Codigo == codigoPreventista);
        //            if (elemento != null)
        //            {
        //                if (elemento.RutaClientes.Count == 0)
        //                {
        //                    var preventista = this.servicioperventista.ObtenerPorCodigo(codigoPreventista, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo);

        //                    var coordenadas = servicioruta.ObtenerCoordenadasPorPreventista(preventista, DateTime.Today, "01");

        //                    elemento.RutaClientes.AddRange(coordenadas);

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
        //        }
        //        return true;
        //    }

        //    private void VerClientes(ICollection<CoordenadaCliente> coordenadas)
        //    {
        //        foreach (var item in coordenadas)
        //        {
        //            if (item.Latitud != 0 && item.Longitud != 0)
        //            {
        //                var marcador = new GMapMarker(new PointLatLng(item.Longitud, item.Latitud));

        //                var pin = new PinAzul();

        //                pin.Tag = item;
        //                pin.Etiqueta = item.Codigo;
        //                marcador.Shape = pin;
        //                marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
        //                this.map.Markers.Add(marcador);
        //            }
        //        }
        //    }

        //    private void DibujarPoligono(Brush color, List<Coordenada> vertices)
        //    {
        //        var poly = vertices.Select(v => new PointLatLng(v.Longitud, v.Latitud));
        //        ControladorZona controlZona = new ControladorZona(this.map);
        //        controlZona.CrearPoligono(poly, color);
        //    }

        //    private void DibujarRuta(Brush color, List<Coordenada> vertices)
        //    {
        //        var poly = vertices.Select(v => new PointLatLng(v.Latitud, v.Longitud));
        //        ControladorZona controlZona = new ControladorZona(this.map);
        //        controlZona.CrearRuta(poly, color);
        //    }

        //    private object VerZonasDelPreventista(object e)
        //    {
        //        var prev = (Elemento)e;
        //        prev.VerZona = true;
        //        try
        //        {
        //            var codigosderutas = this.servicioRutaVenta.ObtenerRutasDelDia(prev.Codigo, DateTime.Today);

        //            foreach (var item in codigosderutas)
        //            {
        //                var vertices = servicioRutaVenta.ObtenerVertices(item.Codigo, "01", "00BEB");
        //                this.DibujarPoligono(Brushes.Blue, vertices);
        //            }
        //            this.RefrescarVista();
        //        }
        //        catch (Exception ex)
        //        {
        //            Mensajes.Aviso(ex.Message);
        //        }
        //        return 0;
        //    }

        //    void ObtenerPosiciones()
        //    {
        //        this.timer.Stop();
        //        App.Current.Dispatcher.Invoke((Action)delegate
        //               {
        //                   try
        //                   {
        //                       this.Status = "Obteniendo Posisiones...";
        //                       var dt = serviceMobile.ObtenerPosicionActualPreventistas();

        //                       foreach (DataRow item in dt.AsEnumerable().Where(p => p.Field<string>("usuario") != ""))
        //                       {
        //                           Func<object, dynamic, object> nonull = (p, def) => p == null ? def : p;
        //                           var elemento = new Elemento()
        //                           {
        //                               Codigo = item.Field<string>("usuario"),
        //                               Estado = (Estado)nonull(item.Field<object>("estado"), Estado.OK),
        //                               Fecha = item.Field<object>("fecha").ToString(),
        //                               CoordenadaActual = new PointLatLng(
        //                                                      double.Parse(item.Field<object>("latitud").ToString()),
        //                                                      double.Parse(item.Field<object>("longitud").ToString())
        //                                                      ),
        //                               Visitados = (int)nonull(item.Field<object>("visitados"), 0),
        //                               Compradores = (int)nonull(item.Field<object>("compradores"), 0),
        //                               Bultos = (int)nonull(item.Field<object>("bultos"), 0)
        //                               //Pesos = decimal.Parse(nonull(item.Field<object>("pesos").ToString(),"0").ToString())

        //                           };
        //                           if (elemento.Codigo != null)
        //                               this.CargarPerventista(elemento);
        //                       }


        //                       switch (this.modo)
        //                       {
        //                           case ModoVerMarcadores.Nada:
        //                               break;
        //                           case ModoVerMarcadores.Todo:
        //                               this.MostrarTodo();
        //                               break;
        //                           case ModoVerMarcadores.Seleccionado:
        //                               this.MostrarMarcador(this.listaDeElementos.SelectedItem as Elemento, true);
        //                               break;
        //                           default:
        //                               break;
        //                       }

        //                       this.Status = "Ultima actualizacion: " + DateTime.Now.ToString();


        //                   }
        //                   catch (Exception exc)
        //                   {
        //                       Mensajes.Error(exc);
        //                       this.Status = "Error al obtener Posiciones...Reintentando";
        //                   }

        //               });

        //        this.timer.Start();
        //    }

        //    void CargarPerventista(Elemento elemento)
        //    {
        //        var ele = elementos.FirstOrDefault(e => e.Codigo == elemento.Codigo);
        //        if (ele == null)
        //        {
        //            var f = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Preventista>>();
        //            var prev = f.ObtenerPorCodigo(elemento.Codigo, Core.CargarRelaciones.NoCargarNada, "01");
        //            if (prev != null)
        //            {
        //                elemento.Foto = prev.Foto;
        //                elemento.Nombre = prev.Nombre;
        //            }
        //            elemento.PropertyChanged += elemento_PropertyChanged;
        //            //cambio la lat y lng para que no sea 0.0
        //            if (elemento.CoordenadaActual.Lat == 0 && elemento.CoordenadaActual.Lng == 0)
        //            {
        //                elemento.CoordenadaActual = new PointLatLng(-38.01898, -57.54409);
        //                elemento.Estado = Estado.GPS_APAGADO;
        //            }
        //            this.elementos.Add(elemento);
        //            //ele = elemento;
        //        }
        //        else
        //        {
        //            ele.Estado = elemento.Estado;
        //            ele.Fecha = elemento.Fecha;
        //            if (elemento.CoordenadaActual.Lat != 0 && elemento.CoordenadaActual.Lng != 0)
        //                ele.CoordenadaActual = elemento.CoordenadaActual;
        //        }

        //        //var fecha = DateTime.Parse(ele.Fecha);
        //        //var timeSpam = DateTime.Now - fecha;
        //        //if (timeSpam.Minutes > 30 || timeSpam.Hours > 1)
        //        //{
        //        //    this.Hablar(String.Format("Atención: {0} no reporta hace {1} minutos", ele.Codigo, timeSpam.Minutes));
        //        //    ele.Estado = Estado.NO_REPORTA;
        //        //}
        //    }

        //    void elemento_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //    {
        //        if (e.PropertyName == "Estado")
        //        {
        //            var elemento = (sender as Elemento);
        //            var estado = elemento.Estado;
        //            switch (estado)
        //            {
        //                case Estado.OK:
        //                    break;
        //                case Estado.GPS_APAGADO:
        //                    this.Hablar(string.Format("¡Atención! {0} apagó el gepeese.", elemento.Codigo));
        //                    break;
        //                case Estado.DATOS_APAGADO:
        //                    this.Hablar(string.Format("Atención!: {0} Deshabilitó paquete de datos.", elemento.Codigo));
        //                    break;
        //                case Estado.FUERA_DE_ZONA:
        //                    this.Hablar(string.Format("Atención!: {0} Está Fuera de Zona.", elemento.Codigo));
        //                    break;
        //                case Estado.DESLOGUEADO:
        //                    this.Hablar(string.Format("Atención!: {0} Se ha deslogueado.", elemento.Codigo));
        //                    break;
        //                case Estado.NO_REPORTA:
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }

        //    void listaDeElementos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        var item = this.listaDeElementos.SelectedItem as Elemento;
        //        item.VerClientes = false;
        //        item.VerZona = false;
        //        SeleccionarMarcador(item);
        //        this.modo = ModoVerMarcadores.Seleccionado;
        //    }


        //    void SeleccionarMarcador(Elemento item)
        //    {
        //        MostrarMarcador(item, true);
        //        map.Position = item.CoordenadaActual;
        //        //map.Zoom = 15;
        //    }
        //    void MostrarMarcador(Elemento item, bool clear = false)
        //    {
        //        if (clear)
        //            map.Markers.Clear();
        //        //{
        //        //    foreach (var m in map.Markers)
        //        //    {
        //        //        if (!(m.Tag != null && m.Tag == "POLIGONO"))
        //        //            map.Markers.Remove(m);
        //        //    }
        //        //}

        //        var marcador = new GMapMarker(item.CoordenadaActual);
        //        var pin = new PinRojo();
        //        pin.Tag = item;
        //        pin.Etiqueta = item.Codigo;
        //        //pin.Menu = new ContextMenu();

        //        var menuItem = new MenuItem();
        //        menuItem.Header = "Ver Clientes de la Ruta";
        //        menuItem.Command = this.CmdVerClientesPorRuta;
        //        menuItem.CommandParameter = item.Codigo;
        //        pin.Menu.Items.Add(menuItem);

        //        var menuMostrarZona = new MenuItem();
        //        menuMostrarZona.Header = "Dibujar Zona";
        //        menuMostrarZona.Command = this.CmdDibujarZona;
        //        menuMostrarZona.CommandParameter = item;
        //        pin.Menu.Items.Add(menuMostrarZona);

        //        var menuMostrarCamino = new MenuItem();
        //        menuMostrarCamino.Header = "Mostrar todos los reportes";
        //        menuMostrarCamino.Command = this.CmdVerCaminoPreventista;
        //        menuMostrarCamino.CommandParameter = item;
        //        pin.Menu.Items.Add(menuMostrarCamino);

        //        pin.Menu.UpdateLayout();
        //        //pin.Menu.Items.Add(new Button()
        //        //{
        //        //    Content = "Ver Clientes de la Ruta",
        //        //    Command = this.CmdVerClientesPorRuta,
        //        //    CommandParameter = item.Codigo
        //        //});
        //        pin.MouseDoubleClick += pin_MouseDoubleClick;

        //        marcador.Shape = pin;
        //        marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
        //        this.map.Markers.Add(marcador);

        //        if (item.VerZona)
        //            this.VerZonasDelPreventista(item);
        //        if (item.VerClientes)
        //            this.VerClientes(item.RutaClientes);
        //        this.RefrescarVista();
        //    }

        //    private void RefrescarVista()
        //    {
        //        var zoomActual = this.map.Zoom;
        //        this.map.Zoom = 20;
        //        this.map.Zoom = zoomActual;
        //    }

        //    void MostrarTodo()
        //    {
        //        map.Markers.Clear();
        //        foreach (var item in elementos)
        //        {
        //            item.VerClientes = false;
        //            item.VerZona = false;
        //            this.MostrarMarcador(item);
        //        }
        //        //this.map.Zoom = 10;
        //    }

        //    void pin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //    {
        //        var elemento = (sender as PinRojo).Tag as Elemento;
        //        MessageBox.Show(elemento.CoordenadaActual.ToString());
        //    }

        //    private void Button_Click(object sender, RoutedEventArgs e)
        //    {
        //        this.MostrarTodo();
        //        this.modo = ModoVerMarcadores.Todo;
        //    }

        //    private void Button_Click_1(object sender, RoutedEventArgs e)
        //    {
        //        this.ObtenerPosiciones();
        //    }

        //    private void Hablar(string mensaje)
        //    {
        //        if (voz.State != SynthesizerState.Speaking)
        //            voz.SpeakAsync(mensaje);
        //        else
        //            colaDeMensajes.Enqueue(mensaje);
        //    }

        //    void voz_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        //    {
        //        if (colaDeMensajes.Count > 0)
        //            try
        //            {
        //                voz.SpeakAsync(colaDeMensajes.Dequeue().ToString());
        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //    }

        //    Queue colaDeMensajes = new Queue();


        //    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //    {
        //        this.voz.Dispose();
        //        this.timer.Dispose();
        //    }

        //    private void Button_Click_2(object sender, RoutedEventArgs e)
        //    {
        //        if (voz.State == SynthesizerState.Speaking)
        //        {
        //            voz.Pause();
        //            EtiquetaBotonSilenciar = "Reanudar reporte";
        //        }
        //        else
        //        {
        //            this.colaDeMensajes.Clear();
        //            voz.Resume();
        //            EtiquetaBotonSilenciar = "Silenciar";
        //        }
        //    }


        //}

        //enum ModoVerMarcadores
        //{
        //    Nada,
        //    Todo,
        //    Seleccionado
        //}

    }
}
