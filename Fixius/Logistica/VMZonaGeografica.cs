using Inteldev.Core.DTO.Locacion;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Logistica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Fixius.Logistica
{
    public class VMZonaGeografica : VistaModeloBase<Servicios.DTO.Logistica.ZonaGeografica>
    {
        #region DP's

        public IPresentadorMiniBusca<Provincia> PresentadorProvincia
        {
            get { return (IPresentadorMiniBusca<Provincia>)GetValue(PresentadorProvinciaProperty); }
            set { SetValue(PresentadorProvinciaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProvincia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProvinciaProperty =
            DependencyProperty.Register("PresentadorProvincia", typeof(IPresentadorMiniBusca<Provincia>), typeof(VMZonaGeografica));

        public IPresentadorMiniBusca<Localidad> PresentadorLocalidad
        {
            get { return (IPresentadorMiniBusca<Localidad>)GetValue(PresentadorLocalidadProperty); }
            set { SetValue(PresentadorLocalidadProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorLocalidad.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorLocalidadProperty =
            DependencyProperty.Register("PresentadorLocalidad", typeof(IPresentadorMiniBusca<Localidad>), typeof(VMZonaGeografica));
        #endregion

        public VMZonaGeografica() : base() { }

        public VMZonaGeografica(ZonaGeografica DTO)
            : base(DTO)
        {
            if (DTO.Id == 0)
                this.CodigoVisible = Visibility.Collapsed;
            this.SetPresentador<Provincia>("PresentadorProvincia", (p => DTO.Provincia = p), DTO.Provincia);
            this.PresentadorProvincia.cantidadNumeros = 2;
            this.SetPresentador<Localidad>("PresentadorLocalidad", (p => DTO.Localidad = p), DTO.Localidad);
            this.PresentadorLocalidad.cantidadNumeros = 4;

        }



    }
}
