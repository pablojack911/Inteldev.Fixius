using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas
{
    public class VMEmpaque : VistaModeloBase<Servicios.DTO.Articulos.Empaque>
    {
        public VMEmpaque()
            : base()
        {

        }

        public VMEmpaque(Servicios.DTO.Articulos.Empaque DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }
    }
}
