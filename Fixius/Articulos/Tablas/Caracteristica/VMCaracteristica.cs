using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Articulos.Tablas.Caracteristica
{
    public class VMCaracteristica : VistaModeloBase<Servicios.DTO.Articulos.Caracteristica>
    {

        public VMCaracteristica()
            : base()
        {

        }

        public VMCaracteristica(Servicios.DTO.Articulos.Caracteristica DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = System.Windows.Visibility.Collapsed;
        }
    }
}
