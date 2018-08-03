using GMap.NET;
using Inteldev.Core.Presentacion.ClienteServicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MoreLinq;
using System.Windows.Media;
using GMap.NET.WindowsPresentation;

namespace Inteldev.Fixius.Mapas
{
    /// <summary>
    /// Interaction logic for VisualizadorDeZonas.xaml
    /// </summary>
    public partial class VisualizadorDeZonas : Window
    {
        public ObservableCollection<ZonaMapa> Zonas { get; set; }
        public List<System.Windows.Media.SolidColorBrush> solidColorBrushes { get; set; }

        public ControladorZona ControladorDeZonas { get; set; }
        public VisualizadorDeZonas()
        {
            InitializeComponent();

            GeoCoderStatusCode status;
            this.map.Position = GoogleMapGeocoder.ObtenerCordenadasPorDireccion("Mar del Plata, Buenos Aires", out status);

            this.ControladorDeZonas = new ControladorZona(this.map);

            this.Zonas = new ObservableCollection<ZonaMapa>();
            this.CargarZonas();

            this.DataContext = this;

            this.listaDeZonas.SelectionChanged += listaDeZonas_SelectionChanged;

            //this.CargarColores();
        }

        private void CargarZonas()
        {
            var servicioRutaVenta = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioRutaDeVenta>("ServicioRutaDeVenta");
            var lista = servicioRutaVenta.ObtenerLista("", Core.CargarRelaciones.CargarEntidades, "01");
            foreach (var item in lista)
            {
                SolidColorBrush color;
                var vertices = servicioRutaVenta.ObtenerVertices(item.Codigo, item.Empresa.Codigo, item.Division);
                if (item.Codigo.StartsWith("A"))
                    color = Brushes.AliceBlue;
                else
                    if (item.Codigo.EndsWith("R"))
                        color = Brushes.Magenta;
                    else
                        color = Brushes.LightBlue;
                this.Zonas.Add(new ZonaMapa()
                {
                    Codigo = item.Codigo,
                    Empresa = item.Empresa.Codigo,
                    Division = item.Division,
                    Nombre = item.Nombre,
                    ColorFondo = color,
                    Vertices = vertices
                });
            }

        }

        //private void CargarColores()
        //{
        //    //var r = new Random();
        //    //var R = 0;
        //    //var G = 0;
        //    //var B = 0;
        //    //this.solidColorBrushes = new List<SolidColorBrush>();

        //    //for (int i = 0; i < 185; i++)
        //    //{
        //    //    this.solidColorBrushes.Add(new SolidColorBrush(Color.FromArgb((byte)i, (byte)i, (byte)(i + 35), (byte)(i + 70))));
        //    //}

        //    //PropertyInfo[] colorProperties = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);
        //    //foreach (PropertyInfo colorProperty in colorProperties)
        //    //{
        //    //    //could probably omit this check; I think all static properties of Colors are of type Color
        //    //    if (colorProperty.PropertyType == typeof(Color))
        //    //    {
        //    //        Color color = (Color)colorProperty.GetValue(null, null);
        //    //        string colorName = colorProperty.Name;
        //    //        SolidColorBrush brush = new SolidColorBrush(color);

        //    //        ColorItem item = new ColorItem() { Brush = brush, Name = colorName };
        //    //        solidColorBrushList.Add(item);
        //    //    }
        //    //}
        //    //return solidColorBrushList;
        //}

        void listaDeZonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.map.Markers.Clear();
            if (this.listaDeZonas.SelectedItem != null)
            {
                foreach (var zona in listaDeZonas.SelectedItems)
                {
                    var item = zona as ZonaMapa;
                    var tag = item.Codigo + "-" + item.Division + "-" + item.Empresa;

                    if (!this.map.Markers.Any(z => z.Tag.ToString() == tag))
                        this.DibujarPoligono(Brushes.Blue, item.Vertices, tag);
                    if (item.Clientes.Count == 0)
                    {
                        var servicioRutaVenta = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioRutaDeVenta>("ServicioRutaDeVenta");
                        var clientes = servicioRutaVenta.ObtenerClientesPorZona(item.Codigo, item.Empresa, item.Division);
                        var servicioCoordenadaCliente = FabricaClienteServicio.Instancia.CrearCliente<Inteldev.Fixius.Contratos.IServicioCoordenadasClientes>("ServicioCoordenadasClientes");
                        item.Clientes = servicioCoordenadaCliente.ObtenerCoordenadasClientesPorZona(clientes).ToList();
                    }
                    this.VerClientes(item.Clientes);
                }
                //foreach (var marker in this.map.Markers.Where(x => x.GetType() == typeof(GMapPolygon)))
                //{
                //    var listaZonas = listaDeZonas.SelectedItems as List<ZonaMapa>;
                //    if (!listaZonas.Any(z => z.Codigo + "-" + z.Division + "-" + z.Empresa == marker.Tag.ToString()))
                //        this.map.Markers.Remove(marker);
                //}
            }
            this.RefrescarVista();
        }

        private void VerClientes(List<Servicios.DTO.Preventa.CoordenadaCliente> coordenadasClientes)
        {
            foreach (var item in coordenadasClientes)
            {
                if (item.Latitud != 0 && item.Longitud != 0)
                {
                    if (!this.map.Markers.Any(z => z.Tag.ToString() == item.Codigo))
                    {
                        var marcador = new GMapMarker(new PointLatLng(item.Latitud, item.Longitud));
                        Pin pin = new Pines.PinFucsia();
                        pin.Etiqueta = item.Codigo.ToString();
                        marcador.Shape = pin;
                        marcador.Tag = item.Codigo;
                        //marcador.Shape.IsHitTestVisible = false;
                        marcador.Offset = new Point(-pin.Width / 2, -pin.Height);
                        pin.ToolTip = "Código: " + item.Codigo + "\n" + item.Nombre + "\n" + item.Domicilio;
                        this.map.Markers.Add(marcador);
                    }
                }
            }
        }
        private void DibujarPoligono(SolidColorBrush solidColorBrush, List<Core.DTO.Locacion.Coordenada> vertices, string tag)
        {
            var poly = vertices.Select(v => new PointLatLng(v.Latitud, v.Longitud));
            this.ControladorDeZonas.CrearPoligono(poly, solidColorBrush, tag);
        }

        private void RefrescarVista()
        {
            var zoomActual = this.map.Zoom;
            this.map.Zoom = 20;
            this.map.Zoom = zoomActual;
        }

        private void btnchkVerAutoservicio_Click(object sender, RoutedEventArgs e)
        {
            this.listaDeZonas.SelectedIndex = -1;
            this.Zonas.ForEach(z =>
            {
                if (z.Codigo.StartsWith("A"))
                    z.Visible = true;
                else
                    z.Visible = false;
            });
            this.VerZonas(this.Zonas.Where(z => z.Codigo.StartsWith("A")).ToList());
        }
        private void btnchkVerTODO_Click(object sender, RoutedEventArgs e)
        {
            this.listaDeZonas.SelectedIndex = -1;
            this.Zonas.ForEach(z => z.Visible = true);
            this.map.Markers.Clear();
            //this.VerZonas(this.Zonas.ToList());
        }

        private void btnchkVerRefrigerado_Click(object sender, RoutedEventArgs e)
        {
            this.listaDeZonas.SelectedIndex = -1;
            this.Zonas.ForEach(z =>
            {
                if (z.Codigo.EndsWith("R"))
                    z.Visible = true;
                else
                    z.Visible = false;
            });
            this.VerZonas(this.Zonas.Where(z => z.Codigo.EndsWith("R")).ToList());
        }

        private void btnchkVerTrade_Click(object sender, RoutedEventArgs e)
        {
            this.listaDeZonas.SelectedIndex = -1;
            this.Zonas.ForEach(z =>
            {
                if (!z.Codigo.StartsWith("A") && !z.Codigo.EndsWith("R"))
                    z.Visible = true;
                else
                    z.Visible = false;
            });
            this.VerZonas(this.Zonas.Where(z => !z.Codigo.StartsWith("A") && !z.Codigo.EndsWith("R")).ToList());
        }

        private void VerZonas(List<ZonaMapa> zonas)
        {
            this.map.Markers.Clear();
            Random r = new Random();

            for (int i = 0; i < zonas.Count; i++)
            {
                var alpha = r.Next(0, byte.MaxValue + 1);
                var red = r.Next(0, byte.MaxValue + 1);
                var green = r.Next(0, byte.MaxValue + 1);
                var blue = r.Next(0, byte.MaxValue + 1);
                var brush = new SolidColorBrush(Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue));
                this.DibujarPoligono(brush, zonas[i].Vertices, zonas[i].Codigo + "-" + zonas[i].Division + "-" + zonas[i].Empresa);
            }
            this.RefrescarVista();
        }
    }
}
