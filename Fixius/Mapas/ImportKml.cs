using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.DTO.Usuarios;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace Inteldev.Fixius.Mapas
{
    public class ImportKml : DependencyObject
    {

        public Dictionary<string, List<Coordenada>> Zonas { get; set; }

        public List<CoordenadaCliente> Clientes
        {
            get { return (List<CoordenadaCliente>)GetValue(ClientesProperty); }
            set { SetValue(ClientesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clientes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientesProperty =
            DependencyProperty.Register("Clientes", typeof(List<CoordenadaCliente>), typeof(ImportKml));

        public void ImportartKml()
        {
            this.Zonas = new Dictionary<string, List<Coordenada>>();

            XNamespace ns = "http://www.opengis.net/kml/2.2";

            var doc = XDocument.Load("H:\\doc.kml");
            var d = doc.Root.Elements().Elements(ns + "Folder").Elements(ns + "Folder").Elements(ns + "Folder").Elements(ns + "Folder").Elements(ns + "Placemark");

            var query = doc.Root
               .Element(ns + "Document")
               .Elements(ns + "Folder")
               .Elements(ns + "Folder")
               .Elements(ns + "Folder")
               .Elements(ns + "Folder")
               .Elements(ns + "Placemark")
               .Select(x => new
               {
                   Name = x.Element(ns + "name").Value,
                   Description = x.Element(ns + "description") == null ? "" : x.Element(ns + "description").Value,
                   Coordinates = x.Element(ns + "Point") == null ? "" : x.Element(ns + "Point").Value,
                   Polygono = x.Element(ns + "Polygon") == null ? "" : x.Element(ns + "Polygon").Value.Substring(1).Trim()

                   // etc
               });


            this.Clientes = new List<CoordenadaCliente>();


            foreach (var item in query)
            {
                if (item.Polygono.Length == 0)
                    this.CargarClientes(item);
                else
                    this.cargaZona(item);


            }
            //for (int i = 0; i < this.Clientes.Count; i = i + 100)
            //{
            //    var servicioCoordenadaCliente = FabricaClienteServicio.Instancia.CrearCliente<IServicioCoordenadasClientes>("ServicioCoordenadasClientes");
            //    if (servicioCoordenadaCliente != null)
            //    {
            //        var gc = servicioCoordenadaCliente.GrabarLista(this.Clientes.Skip(i).Take(100).ToList(), new Usuario() { Nombre = "POCHO" }, "01");
            //        //MessageBox.Show(gc.getMensaje());
            //    }
            //}

            var servicioRuta = FabricaClienteServicio.Instancia.CrearCliente<IServicioRutaDeVenta>("ServicioRutaDeVenta");
            if (servicioRuta != null)
            {
                foreach (var item in this.Zonas)
                {
                    servicioRuta.GrabarVertices(item.Key, item.Value);
                }
            }


        }

        private void cargaZona(dynamic item)
        {
            var verti = item.Polygono.Split(' ');
            var codigo = ((string)item.Name).Split('-').LastOrDefault().Trim().Replace(" ", string.Empty).PadLeft(4, '0');
            //var vertices = new List<Tuple<double, double>>();
            if (codigo == "A600")
                codigo = "A600";
            var vertices = new List<Coordenada>();
            double lat;
            double lng;

            foreach (var vx in verti)
            {

                var coord = vx.Split(',');
                lng = double.Parse(coord[0], CultureInfo.InvariantCulture);
                lat = double.Parse(coord[1], CultureInfo.InvariantCulture);

                var coordenada = new Coordenada() { Latitud = lat, Longitud = lng };
                vertices.Add(coordenada);
                //vertices.Add(new Tuple<double, double>(lat, lng));
            }

            if (!this.Zonas.Keys.Any(p => p == codigo))
                this.Zonas.Add(codigo, vertices);
        }

        void CargarClientes(dynamic item)
        {
            string codigo = ((string)item.Name).Split('(', ')').ElementAtOrDefault(1); ;

            if (codigo == null)
                codigo = ((string)item.Description).Split('(', ')').ElementAtOrDefault(1);


            var coor = item.Coordinates.Split(',');
            double lat = 0;
            double lng = 0;
            try
            {
                if (coor.Length == 3)
                {
                    lng = double.Parse(coor[0], CultureInfo.InvariantCulture);
                    lat = double.Parse(coor[1], CultureInfo.InvariantCulture);
                }
            }
            catch (Exception exc)
            {

            }


            var cliente = new CoordenadaCliente()
            {
                Codigo = codigo ?? "",
                Nombre = item.Description,
                //Domicilio = item.Name,
                Latitud = lat,
                Longitud = lng

            };


            this.Clientes.Add(cliente);
            if (cliente.Codigo.Trim() != string.Empty)
            {
                cliente.Codigo = cliente.Codigo.PadLeft(5, '0');
                this.Clientes.Add(cliente);
            }
        }

    }
}
