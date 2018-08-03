using Fixius;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;

namespace Inteldev.Fixius.Mapas
{
    /// <summary>
    /// Interaction logic for ActualizarCoordenadasClientes.xaml
    /// </summary>
    public partial class ActualizarCoordenadasClientes : Window, INotifyPropertyChanged
    {
        public List<CoordenadaCliente> Coordenadas { get; set; }
        public System.Timers.Timer timer { get; set; }

        IServicioCoordenadasClientes servicioCoordenadaCliente;



        public ObservableCollection<CoordenadaCliente> ListaClientesConCoordenadasInvalidas
        {
            get { return (ObservableCollection<CoordenadaCliente>)GetValue(ListaClientesConCoordenadasInvalidasProperty); }
            set { SetValue(ListaClientesConCoordenadasInvalidasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListaClientesConCoordenadasInvalidas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListaClientesConCoordenadasInvalidasProperty =
            DependencyProperty.Register("ListaClientesConCoordenadasInvalidas", typeof(ObservableCollection<CoordenadaCliente>), typeof(ActualizarCoordenadasClientes));

        public Visibility EstaActualizando
        {
            get { return (Visibility)GetValue(EstaActualizandoProperty); }
            set { SetValue(EstaActualizandoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EstaActualizando.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EstaActualizandoProperty =
            DependencyProperty.Register("EstaActualizando", typeof(Visibility), typeof(ActualizarCoordenadasClientes), new PropertyMetadata(Visibility.Collapsed));

        //public TimeSpan TiempoRestante
        //{
        //    get { return (TimeSpan)GetValue(TiempoRestanteProperty); }
        //    set { SetValue(TiempoRestanteProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for TiempoRestante.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TiempoRestanteProperty =
        //    DependencyProperty.Register("TiempoRestante", typeof(TimeSpan), typeof(ActualizarCoordenadasClientes));

        public int Progreso
        {
            get { return (int)GetValue(ProgresoProperty); }
            set { SetValue(ProgresoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Progreso.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgresoProperty =
            DependencyProperty.Register("Progreso", typeof(int), typeof(ActualizarCoordenadasClientes), new PropertyMetadata(0));

        public int TotalClientes
        {
            get { return (int)GetValue(TotalClientesProperty); }
            set { SetValue(TotalClientesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalClientes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalClientesProperty =
            DependencyProperty.Register("TotalClientes", typeof(int), typeof(ActualizarCoordenadasClientes), new PropertyMetadata(0));


        public ActualizarCoordenadasClientes(List<CoordenadaCliente> coordenadas)
        {
            InitializeComponent();

            this.DataContext = this;

            this.servicioCoordenadaCliente = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioCoordenadasClientes>("ServicioCoordenadasClientes");

            this.Coordenadas = coordenadas;
            this.TotalClientes = this.Coordenadas.Count;
            this.Progreso = 0;

            this.ListaClientesConCoordenadasInvalidas = new ObservableCollection<CoordenadaCliente>();



            //timer.Elapsed += (d, s) => Actualizar2();
            //this.TiempoRestante = TimeSpan.FromSeconds(this.TotalClientes);
        }

        private void Actualizar2()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < this.TotalClientes; i++)
                {
                    this.servicioCoordenadaCliente.Grabar(this.Coordenadas[this.Progreso], null, "01");
                    this.Progreso = i;
                }
                //lock (this.Coordenadas)
                //{
                //    while (this.Progreso <= this.TotalClientes)
                //    {
                //        if (Monitor.Wait(this.Coordenadas, 1000)) // Wait for one second at most
                //        {
                //            Monitor.PulseAll(this.Coordenadas); // Signal boss we are done
                //        }
                //        else
                //        {
                //            this.servicioCoordenadaCliente.Grabar(this.Coordenadas[this.Progreso], null, "01");
                //            this.Progreso++;
                //            Monitor.PulseAll(this.Coordenadas); // Signal boss we are done
                //        }
                //    }
                //}


                //lock (this.Coordenadas)
                //{
                //    for (int i = 0; i < this.TotalClientes; i++)
                //    {
                //        Monitor.Wait(this.Coordenadas);
                //        this.servicioCoordenadaCliente.Grabar(this.Coordenadas[i], null, "01");
                //        this.Progreso = i;
                //        Monitor.PulseAll(this.Coordenadas);
                //Dispatcher.CurrentDispatcher.Thread.Resume();
                //    }
                //}
            }), DispatcherPriority.Background, null);

        }

        private void Actualizar()
        {
            for (int i = 0; i < this.TotalClientes; i++)
            {
                this.Progreso = i;
                this.InvalidateVisual();
                try
                {
                    var requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}", Uri.EscapeDataString(this.Coordenadas[this.Progreso].Domicilio + "," + " Mar del Plata"));
                    double lat = 0;
                    double lng = 0;
                    //34.6000° S, 58.3833° W ARGENTINA
                    var request = WebRequest.Create(requestUri);
                    try
                    {
                        var response = request.GetResponse();
                        var xdoc = XDocument.Load(response.GetResponseStream());

                        switch (xdoc.Element("GeocodeResponse").Element("status").Value)
                        {
                            case "OK":
                                var result = xdoc.Element("GeocodeResponse").Element("result");
                                var locationElement = result.Element("geometry").Element("location");
                                lat = double.Parse(locationElement.Element("lat").Value, CultureInfo.InvariantCulture);
                                lng = double.Parse(locationElement.Element("lng").Value, CultureInfo.InvariantCulture);
                                this.Coordenadas[this.Progreso].Latitud = lat;
                                this.Coordenadas[this.Progreso].Longitud = lng;
                                this.servicioCoordenadaCliente.Grabar(this.Coordenadas[this.Progreso], null, "01");
                                break;
                            default:
                                throw new Exception("Limite de consultas alcanzado o error al convertir el domicilio en Coordenadas.");
                                break;
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                        //Mensajes.Error("Error al traducir dirección a Coordenadas.\n\n" + exc.Message);
                    }
                }
                catch (Exception ex)
                {
                    Mensajes.Error("Otro tipo de error en Actualización" + ex.Message);
                    this.timer.Dispose();
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //this.timer.Dispose();
        }

        private void btnActualizarTodos_Click(object sender, RoutedEventArgs e)
        {
            //timer = new System.Timers.Timer(1);
            //timer.Elapsed += (d, s) => Actualizar();
            //this.timer.Start();
            this.btnActualizarTodos.Visibility = System.Windows.Visibility.Collapsed;
            this.EstaActualizando = System.Windows.Visibility.Visible;
            this.Actualizar();
            //this.Actualizar2();
        }

        private void btnMostrarCoordenadasMalas_Click(object sender, RoutedEventArgs e)
        {
            this.ListaClientesConCoordenadasInvalidas = new ObservableCollection<CoordenadaCliente>(this.servicioCoordenadaCliente.ObtenerCoordenadasClientesInvalidas());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
