using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Logistica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Logistica
{
    public class VMZonaLogistica : VistaModeloBase<ZonaLogistica>
    {
        public VMZonaLogistica()
        {

        }
        public VMZonaLogistica(ZonaLogistica DTO)
            : base(DTO)
        {
        }
    }
}
