using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Organizacion.Localidad
{
    public class VMLocalidad : VistaModeloBase<Inteldev.Core.DTO.Locacion.Localidad>
    {


        public IPresentadorMiniBusca<Inteldev.Core.DTO.Locacion.Provincia> PresentadorProvincia
        {
            get { return (IPresentadorMiniBusca<Inteldev.Core.DTO.Locacion.Provincia>)GetValue(PresentadorProvinciaProperty); }
            set { SetValue(PresentadorProvinciaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProvincia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProvinciaProperty =
            DependencyProperty.Register("PresentadorProvincia", typeof(IPresentadorMiniBusca<Inteldev.Core.DTO.Locacion.Provincia>), typeof(VMLocalidad));

        public VMLocalidad() : base() { }

        public VMLocalidad(Core.DTO.Locacion.Localidad DTO)
            : base(DTO)
        {
            this.SetPresentador<Inteldev.Core.DTO.Locacion.Provincia>("PresentadorProvincia", (p => DTO.Provincia = p), DTO.Provincia);
            this.PresentadorProvincia.cantidadNumeros = 2;
        }
    }
}
