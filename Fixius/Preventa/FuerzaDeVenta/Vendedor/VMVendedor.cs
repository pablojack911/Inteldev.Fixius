using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Preventa.Vendedor
{
    public class VMVendedor : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Preventa.Vendedor>
    {
        public VMVendedor()
            : base()
        {

        }

        public VMVendedor(Inteldev.Fixius.Servicios.DTO.Preventa.Vendedor dto)
            : base(dto)
        {

        }
    }
}
