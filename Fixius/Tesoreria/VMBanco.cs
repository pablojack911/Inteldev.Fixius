using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Tesoreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Tesoreria
{
    public class VMBanco : VistaModeloBase<Banco>
    {
        public VMBanco()
        {

        }

        public VMBanco(Banco DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = System.Windows.Visibility.Collapsed;
        }
    }
}
