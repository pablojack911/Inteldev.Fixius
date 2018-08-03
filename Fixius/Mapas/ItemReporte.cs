using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Mapas
{
    public class ItemReporte
    {
        public string Cliente { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Tiempo { get; set; }
        public TipoReporte Tipo { get; set; }
        public string CodigoVendedor { get; set; }
        public ItemReporte(string codigo)
        {
            this.CodigoVendedor = codigo;
        }
    }

    public enum TipoReporte : int
    {
        EN_VIAJE = 0,
        CHECK_IN = 1,
        CHECK_OUT = 2
    }
}
