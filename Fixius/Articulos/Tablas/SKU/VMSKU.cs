using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Articulos.Tablas.SKU
{
    public class VMSKU : VistaModeloBase<Fixius.Servicios.DTO.Articulos.SKU>
    {
        public VMSKU()
            : base()
        {

        }

        public VMSKU(Fixius.Servicios.DTO.Articulos.SKU DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = System.Windows.Visibility.Collapsed;
        }
    }
}
