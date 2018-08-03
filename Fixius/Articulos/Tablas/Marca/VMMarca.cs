using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.Marca
{
    public class VMMarca : VistaModeloBase<Servicios.DTO.Articulos.Marca>
    {
        public VMMarca()
            : base()
        {

        }

        public VMMarca(Servicios.DTO.Articulos.Marca DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }
    }
}
