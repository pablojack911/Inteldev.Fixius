using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Clientes.Tablas.CondicionDePagoCliente
{
    public class VMCondicionDePagoCliente : VistaModeloBase<Servicios.DTO.Clientes.CondicionDePagoCliente>
    {
        #region Constructores
        public VMCondicionDePagoCliente()
            : base()
        {

        }

        public VMCondicionDePagoCliente(Servicios.DTO.Clientes.CondicionDePagoCliente DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;

        }

        #endregion Constructores





    }
}
