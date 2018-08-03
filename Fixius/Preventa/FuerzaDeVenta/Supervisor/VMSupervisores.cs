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

namespace Inteldev.Fixius.Preventa.FuerzaDeVenta.Supervisor
{
    public class VMSupervisores : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Preventa.Supervisor>
    {


        public IPresentadorMinibuscaList<Servicios.DTO.Preventa.Supervisor, Servicios.DTO.Preventa.Preventista> PresentadorPreventistas
        {
            get { return (IPresentadorMinibuscaList<Servicios.DTO.Preventa.Supervisor, Servicios.DTO.Preventa.Preventista>)GetValue(PresentadorPreventistasProperty); }
            set { SetValue(PresentadorPreventistasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorPreventistas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorPreventistasProperty =
            DependencyProperty.Register("PresentadorPreventistas", typeof(IPresentadorMinibuscaList<Servicios.DTO.Preventa.Supervisor, Servicios.DTO.Preventa.Preventista>), typeof(VMSupervisores));

        public VMSupervisores() : base() { }

        public VMSupervisores(Servicios.DTO.Preventa.Supervisor DTO)
            : base(DTO)
        {
            this.SetPresentadorEspecial<Servicios.DTO.Preventa.Preventista>("PresentadorPreventistas", DTO.Preventistas);
            this.PresentadorPreventistas.PMB.cantidadNumeros = 2;
        }
    }
}
