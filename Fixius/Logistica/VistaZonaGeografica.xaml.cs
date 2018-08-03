using GMap.NET;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Fixius.Mapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inteldev.Fixius.Logistica
{
    /// <summary>
    /// Interaction logic for VistaZonaGeografica.xaml
    /// </summary>
    public partial class VistaZonaGeografica : UserControl
    {

        ControladorZona controlzona;

        public VistaZonaGeografica()
        {
            InitializeComponent();
            this.controlzona = new ControladorZona(mapa);
            this.controlzona.FinalizoDibujoDeZona += controlzona_FinalizoDibujoDeZona;
            this.DataContextChanged += VistaZonaGeografica_DataContextChanged;
        }

        void VistaZonaGeografica_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dynamic dc = this.DataContext;
            List<Coordenada> Vertices = dc.Modelo.Vertices;
            if (Vertices.Count == 0)
            {
                GeoCoderStatusCode status;
                var point = GoogleMapGeocoder.ObtenerCordenadasPorDireccion("Mar del Plata, Argentina", out status);
                mapa.Position = point;
            }
            else
            {
                List<PointLatLng> points = new List<PointLatLng>();

                Vertices.ForEach(p => points.Add(new PointLatLng(p.Latitud, p.Longitud)));

                this.controlzona.CrearPoligono(points, Brushes.Red);
                mapa.Position = points.FirstOrDefault();
            }
            this.DataContextChanged -= VistaZonaGeografica_DataContextChanged;
        }

        void controlzona_FinalizoDibujoDeZona(GMap.NET.WindowsPresentation.GMapPolygon Zona)
        {
            dynamic dc = this.DataContext;
            List<Coordenada> Vertices = dc.Modelo.Vertices;
            Zona.Points.ForEach(p => Vertices.Add(new Coordenada() { Latitud = p.Lat, Longitud = p.Lng }));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.controlzona.DibujarZona();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtItemCodigo.Campo.Focus();
        }
    }
}
