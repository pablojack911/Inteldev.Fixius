using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMEnvase : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Articulos.Envase>
    {


        public IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Articulos.Articulo> PresentadorArticulo
        {
            get { return (IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Articulos.Articulo>)GetValue(PresentadorArticuloProperty); }
            set { SetValue(PresentadorArticuloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorArticulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorArticuloProperty =
            DependencyProperty.Register("PresentadorArticulo", typeof(IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Articulos.Articulo>), typeof(VMEnvase));
        public VMEnvase() : base() { }
        public VMEnvase(Envase DTO)
            : base(DTO)
        {
            if (DTO.DatosOld == null)
                DTO.DatosOld = new DatosOldEnvases();
            this.SetPresentador<Articulo>("PresentadorArticulo", p => DTO.DatosOld.Articulo = p, DTO.DatosOld.Articulo);
        }
        
    }
}
