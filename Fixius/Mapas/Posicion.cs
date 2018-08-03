using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Mapas
{
    public class Posicion
    {
        public DateTime Fecha { get; set; }
        public PointLatLng Coordenada { get; set; }
        public Estado Estado { get; set; }
        public string Cliente { get; set; }
        public MotivoNoCompra MotivoNoCompra { get; set; }
        public decimal PesosCompra { get; set; }
        public int BultosCompra { get; set; }

    }
}
