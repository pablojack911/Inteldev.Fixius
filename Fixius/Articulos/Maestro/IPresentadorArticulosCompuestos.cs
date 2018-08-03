using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Articulos.Maestro
{
    public interface IPresentadorArticulosCompuestos
    {
        bool Aceptar();
        void CerrarVentana();
        Inteldev.Core.Presentacion.Presentadores.Interfaces.IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Articulos.Articulo> presentadorMiniBusca { get; set; }
    }
}
