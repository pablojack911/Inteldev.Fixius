using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.Clase
{
    public class VMClase : VistaModeloBase<Servicios.DTO.Articulos.Clase>
    {
        public VMClase()
            : base()
        {

        }

        public VMClase(Servicios.DTO.Articulos.Clase DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }
    }
}
