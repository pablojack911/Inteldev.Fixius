using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Organizacion.Provincia
{
    public class VMProvincia : VistaModeloBase<Inteldev.Core.DTO.Locacion.Provincia>
    {
        public VMProvincia()
            : base()
        {

        }

        public VMProvincia(Inteldev.Core.DTO.Locacion.Provincia DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }
    }
}
