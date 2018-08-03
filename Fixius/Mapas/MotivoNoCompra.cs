using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Mapas
{
    public enum MotivoNoCompra : int
    {
        Compra = 0,
        Cerrado = 1,
        NoEstaElResponsable = 2,
        TieneStock = 3,
        NoTienePlata = 4
    }
}
