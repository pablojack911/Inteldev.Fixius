using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Precios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMCambioDePreciosDeVenta:VistaModeloBase<CambioDePreciosDeVenta>
    {
        public VMCambioDePreciosDeVenta(CambioDePreciosDeVenta dto)
            : base(dto)
        {

        }

        public VMCambioDePreciosDeVenta()         
        {

        }
    }
}
