using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMRubro : VistaModeloBase<Servicios.DTO.Articulos.Rubro>
    {




        public IPresentadorMiniBusca<CondicionDePago> PresentadorCondicionDePago
        {
            get { return (IPresentadorMiniBusca<CondicionDePago>)GetValue(PresentadorCondicionDePagoProperty); }
            set { SetValue(PresentadorCondicionDePagoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCondicionDePago.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCondicionDePagoProperty =
            DependencyProperty.Register("PresentadorCondicionDePago", typeof(IPresentadorMiniBusca<CondicionDePago>), typeof(VMRubro));

        

        

        public VMRubro()
            : base()
        {

        }

        public VMRubro(Rubro DTO) : base(DTO)
        {
            this.SetPresentador<CondicionDePago>("PresentadorCondicionDePago",(p=>DTO.CondicionDePago=p),DTO.CondicionDePago);
        }
    }
}
