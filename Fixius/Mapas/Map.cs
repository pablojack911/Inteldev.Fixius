
namespace Inteldev.Fixius.Mapas
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using GMap.NET.WindowsPresentation;
    using System.Globalization;
    // using System.Windows.Forms;
    using System.Windows;
    using System;
    using GMap.NET.MapProviders;
    using GMap.NET;
    using Inteldev.Core.Presentacion;

    /// <summary>
    /// the custom map f GMapControl 
    /// </summary>
    public class Map : GMapControl
    {
        public Map()
            : base()
        {
            this.CenterCrossPen = null;
            this.ShowCenter = false;
            //this.MapProvider = BingMapProvider.Instance;
            this.MapProvider = GoogleMapProvider.Instance;
            this.MaxZoom = 20;
            this.MinZoom = 1;
            this.Zoom = 15;
            //agregado por Pocho
            this.TouchEnabled = true;
            this.CanDragMap = true;
            //34.6000° S, 58.3833° W ARGENTINA
            //this.MapPoint = new Point(-34.6000, -58.3833);
            //this.Position = new PointLatLng(-34.6000, -58.3833);
            this.DragButton = System.Windows.Input.MouseButton.Left;
            this.IgnoreMarkerOnMouseWheel = true;
            this.MouseDoubleClick += Map_MouseDoubleClick;
        }

        void Map_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //var r = (GMapControl)sender;
            //if (r != null && r.Position != null)
            //{
            //    Mensajes.Aviso("Posición:\n\nLat: " + r.Position.Lat + " - Lng: " + r.Position.Lng);
            //    e.Handled = true;
            //}
        }

    }
}
