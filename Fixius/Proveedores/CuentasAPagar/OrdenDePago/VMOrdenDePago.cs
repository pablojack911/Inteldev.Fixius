using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Proveedores.CuenasAPagar.OrdenDePago
{
    public class VMOrdenDePago : VistaModeloBase<Servicios.DTO.Proveedores.OrdenDePago>
    {
        public VMOrdenDePago() : base() { }

        public VMOrdenDePago(Servicios.DTO.Proveedores.OrdenDePago DTO)
            : base(DTO)
        {

        }
    }
}
