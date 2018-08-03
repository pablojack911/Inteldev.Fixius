using GMap.NET;
using GMap.NET.WindowsPresentation;
using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Mapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Inteldev.Fixius.Presentadores
{
    public class PresentadorMapa : Inteldev.Core.Presentacion.Presentadores.PresentadorBaseDialogo<Domicilio>
    {

        //Localidad Localidad;
        //Provincia Provincia;

        //IServicioABM<Localidad> servicioLocalidad;

        //Mapas.ControladorPuntos controladorPunto;

        public ICommand CmdVerMapa
        {
            get { return (ICommand)GetValue(CmdVerMapaProperty); }
            set { SetValue(CmdVerMapaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CmdVerMapa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CmdVerMapaProperty =
            DependencyProperty.Register("CmdVerMapa", typeof(ICommand), typeof(PresentadorMapa));

        //public Coordenada Coordenada
        //{
        //    get { return (Coordenada)GetValue(CoordenadaProperty); }
        //    set { SetValue(CoordenadaProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Coordenada.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CoordenadaProperty =
        //    DependencyProperty.Register("Coordenada", typeof(Coordenada), typeof(PresentadorMapa));
        //public string Domicilio
        //{
        //    get { return (string)GetValue(DomicilioProperty); }
        //    set { SetValue(DomicilioProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Domicilio.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DomicilioProperty =
        //    DependencyProperty.Register("Domicilio", typeof(string), typeof(PresentadorMapa));


        public PresentadorMapa()
        {
            //this.servicioLocalidad = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Localidad>>();

            //this.CmdVerMapa = new RelayCommand(p => this.vermapa(p));

            //this.EntidadActual = domicilio;

            //if (domicilio.Coordenada != null)
            //    this.Coordenada = domicilio.Coordenada;
            //else
            //    this.Coordenada = new Coordenada() { Latitud = 0, Longitud = 0 };
            //if (localidad == null)
            //    this.Localidad = servicioLocalidad.ObtenerPorCodigo("7600", Core.CargarRelaciones.NoCargarNada, "01");
            //else
            //    this.Localidad = localidad;
            //this.Provincia = provincia;
        }


        private bool vermapa(object p)
        {
            //string domicilio = string.Empty;
            //domicilio = this.EntidadActual.ToString();
            //if (this.EntidadActual.Calle != null) //NULLREFERENCEEXCEPTION SI NO EXISTE UNA CALLE (?) Entidad sin setear...
            //    domicilio = string.Format("{0} {1},{2},{3} ", EntidadActual.Calle.Nombre.Trim(), EntidadActual.Numero, Localidad == null ? "Mar del Plata" : Localidad.Nombre, Provincia == null ? "Buenos Aires" : Provincia.Nombre);
            var domicilio = (Domicilio)p;
            //this.Domicilio = this.EntidadActual + ", " + this.Localidad + ", " + this.Provincia;
            //var mapa = new Mapas.Mapa() { Width = 300, Height = 300 };

            //this.controladorPunto = new Mapas.ControladorPuntos(mapa.map);
            //            var marcador = this.controladorPunto.CrearMarcador("florencio sanchez 3097, mar del plata");
            //GMapMarker marcador = null;
            //if (this.EntidadActual.Coordenada == null)
            //{
            //    marcador = this.controladorPunto.CrearMarcador(this.Domicilio);
            //}
            //else
            //{
            //    marcador = this.controladorPunto.CrearMarcador(new PointLatLng(EntidadActual.Coordenada.Latitud, EntidadActual.Coordenada.Longitud));
            //}
            //if (marcador != null)
            //{
            //    this.Coordenada.Latitud = marcador.Position.Lat;
            //    this.Coordenada.Longitud = marcador.Position.Lng;
            //    this.EntidadActual.Coordenada = this.Coordenada;

            //    marcador.Shape = Mapas.FabricaIconoMarcador.Circulo(10, Brushes.Red);
            //    this.controladorPunto.MostrarMarcador(marcador);
            //}
            //mapa.map.ZoomAndCenterMarkers(null);

            //this.Vista = mapa;
            //var ventana = new BaseVentanaDialogo();
            //ventana.VistaPrincipal.Content = this.Vista;
            //this.Ventana = ventana;
            //mapa.DataContext = this;

            //this.Ejecutar();
            GeoCoderStatusCode status;
            var coordenada = GoogleMapGeocoder.ObtenerCordenadasPorDireccion(domicilio.ToString() + ", Mar del Plata", out status);
            if (status == GeoCoderStatusCode.G_GEO_SUCCESS)
            {
                //domicilio.Latitud = coordenada.Lat;
                //domicilio.Longitud = coordenada.Lng;
            }
            return true;
        }



    }
}
