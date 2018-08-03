using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace Inteldev.Fixius.Mapas
{
    public class ControladorPuntos
    {
        GMapControl mapcontrol;
        public ControladorPuntos(GMapControl mapcontrol)
        {
            this.mapcontrol = mapcontrol;
        }

        public GMapMarker CrearMarcador(PointLatLng point)
        {
            GMapMarker marcador = new GMapMarker(point);
            return marcador;
        }

        public GMapMarker CrearMarcador(string direccion)
        {
            GMapMarker marcador = null; ;
            GeoCoderStatusCode status;
            var point = GoogleMapGeocoder.ObtenerCordenadasPorDireccion(direccion, out status);

            if (status == GeoCoderStatusCode.G_GEO_SUCCESS)
            {
                marcador = new GMapMarker(point);
            }
            return marcador;
        }

        public void MostrarMarcador(GMapMarker marcador)
        {
            this.mapcontrol.Markers.Add(marcador);
        }
        public void EliminarMarcador(GMapMarker marcador)
        {
            this.mapcontrol.Markers.Remove(marcador);
        }


    }
}
