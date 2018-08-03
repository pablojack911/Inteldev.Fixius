using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Fixius.Deposito
{
    public class VMDeposito : VistaModeloBase<Core.DTO.Stock.Deposito>
    {
        public VMDeposito()
        {

        }

        public VMDeposito(Core.DTO.Stock.Deposito DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = System.Windows.Visibility.Collapsed;
        }
    }
}
