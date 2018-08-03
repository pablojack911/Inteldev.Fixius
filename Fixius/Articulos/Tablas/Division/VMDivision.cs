using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.Division
{
    public class VMDivision : VistaModeloBase<Servicios.DTO.Articulos.Division>
    {
        public VMDivision()
            : base()
        {

        }

        public VMDivision(Servicios.DTO.Articulos.Division DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }
    }
}
