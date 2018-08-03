using Inteldev.Core.DTO.Locacion;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Inteldev.Fixius.Mapas
{
    public class ZonaMapa : DependencyObject
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Empresa { get; set; }
        public string Division { get; set; }
        public List<Coordenada> Vertices { get; set; }
        public List<CoordenadaCliente> Clientes { get; set; }
        public SolidColorBrush ColorFondo { get; set; }

        public bool Visible
        {
            get { return (bool)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.Register("Visible", typeof(bool), typeof(ZonaMapa), new PropertyMetadata(true));

        public ZonaMapa()
        {
            this.Vertices = new List<Coordenada>();
            this.Clientes = new List<CoordenadaCliente>();
        }
    }
}
