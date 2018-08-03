using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Articulos.Tablas.Categoria
{
    public class VMCategoria : VistaModeloBase<Fixius.Servicios.DTO.Articulos.Categoria>
    {
        public VMCategoria()
            : base()
        {

        }

        public VMCategoria(Fixius.Servicios.DTO.Articulos.Categoria DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = System.Windows.Visibility.Collapsed;
        }
    }
}
