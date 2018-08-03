using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.TasasDeIva
{
    public class VMTasasDeIva : VistaModeloBase<Fixius.Servicios.DTO.Contabilidad.TasasDeIva>
    {
        public VMTasasDeIva()
            : base()
        {

        }

        public VMTasasDeIva(Fixius.Servicios.DTO.Contabilidad.TasasDeIva DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }
    }
}
