using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.GrupoArticulo
{
    public class VMGrupoArticulo : VistaModeloBase<Fixius.Servicios.DTO.Articulos.GrupoArticulo>
    {
        public VMGrupoArticulo()
            : base()
        {

        }

        public VMGrupoArticulo(Fixius.Servicios.DTO.Articulos.GrupoArticulo DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }
    }
}
