using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Core.DTO.Organizacion;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMDivisionComercial : VistaModeloBase<Inteldev.Core.DTO.Organizacion.DivicionComercial>
    {

        public IPresentadorMiniBusca<Core.DTO.Organizacion.DivicionComercial> PresentadorMiniBusca
        {
            get { return (IPresentadorMiniBusca<Core.DTO.Organizacion.DivicionComercial>)GetValue(PresentadorMiniBuscaProperty); }
            set { SetValue(PresentadorMiniBuscaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMiniBusca.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaProperty =
            DependencyProperty.Register("PresentadorMiniBusca", typeof(IPresentadorMiniBusca<Core.DTO.Organizacion.DivicionComercial>), typeof(VMDivisionComercial));

        public VMDivisionComercial(Inteldev.Core.DTO.Organizacion.DivicionComercial dto) : base(dto)
        {
            this.SetPresentador<Empresa>("PresentadorMiniBusca",(p=>dto.Empresa=p),dto.Empresa);
        }

        
    }
}
