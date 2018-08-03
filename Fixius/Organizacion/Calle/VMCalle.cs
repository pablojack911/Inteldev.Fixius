using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Organizacion.Calle
{
    public class VMCalle : VistaModeloBase<Inteldev.Core.DTO.Locacion.Calle>
    {


        public IPresentadorMiniBusca<Core.DTO.Locacion.Localidad> PresentadorLocalidad
        {
            get { return (IPresentadorMiniBusca<Core.DTO.Locacion.Localidad>)GetValue(PresentadorLocalidadProperty); }
            set { SetValue(PresentadorLocalidadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorLocalidad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorLocalidadProperty =
            DependencyProperty.Register("PresentadorLocalidad", typeof(IPresentadorMiniBusca<Core.DTO.Locacion.Localidad>), typeof(VMCalle));

        public VMCalle() : base() { }

        public VMCalle(Core.DTO.Locacion.Calle DTO)
            : base(DTO)
        {
            this.SetPresentador<Core.DTO.Locacion.Localidad>("PresentadorLocalidad", (p => DTO.Localidad = p), DTO.Localidad);
            this.PresentadorLocalidad.cantidadNumeros = 4;
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
        }
    }
}
