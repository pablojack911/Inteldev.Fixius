using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMRamo : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Clientes.Ramo>
    {

        public IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.Canal> PresentadorCanal
        {
            get { return (IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.Canal>)GetValue(PresentadorCanalProperty); }
            set { SetValue(PresentadorCanalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCanal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCanalProperty =
            DependencyProperty.Register("PresentadorCanal", typeof(IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Clientes.Canal>), typeof(VMRamo));

        public VMRamo() : base() { }

        public VMRamo(Ramo DTO)
            : base(DTO)
        {
            this.SetPresentador<Canal>("PresentadorCanal",p=>DTO.Canal=p,DTO.Canal);
        }

    }
}
