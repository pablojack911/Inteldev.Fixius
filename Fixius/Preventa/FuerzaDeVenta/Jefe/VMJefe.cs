using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.FuerzaDeVenta.Jefe
{
    public class VMJefe : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Preventa.Jefe>
    {
        public IPresentadorMinibuscaList<Servicios.DTO.Preventa.Jefe, Servicios.DTO.Preventa.Supervisor> PresentadorSupervisores
        {
            get { return (IPresentadorMinibuscaList<Servicios.DTO.Preventa.Jefe, Servicios.DTO.Preventa.Supervisor>)GetValue(PresentadorSupervisoresProperty); }
            set { SetValue(PresentadorSupervisoresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorSupervisores.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorSupervisoresProperty =
            DependencyProperty.Register("PresentadorSupervisores", typeof(IPresentadorMinibuscaList<Servicios.DTO.Preventa.Jefe, Servicios.DTO.Preventa.Supervisor>), typeof(VMJefe));

        public IPresentadorMinibuscaList<Servicios.DTO.Preventa.Jefe, DivisionComercial> PresentadorDivisionComercial
        {
            get { return (IPresentadorMinibuscaList<Servicios.DTO.Preventa.Jefe, DivisionComercial>)GetValue(PresentadorDivisionComercialProperty); }
            set { SetValue(PresentadorDivisionComercialProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDivisionComercial.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDivisionComercialProperty =
            DependencyProperty.Register("PresentadorDivisionComercial", typeof(IPresentadorMinibuscaList<Servicios.DTO.Preventa.Jefe, DivisionComercial>), typeof(VMJefe));

        public VMJefe()
            : base()
        {

        }

        public VMJefe(Inteldev.Fixius.Servicios.DTO.Preventa.Jefe DTO) : base(DTO)
        {
            this.SetPresentadorEspecial<Servicios.DTO.Preventa.Supervisor>("PresentadorSupervisores", DTO.Supervisores);

        }
    }
}
