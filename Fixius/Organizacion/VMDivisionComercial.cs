using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Core.DTO.Organizacion;

namespace Inteldev.Fixius.Organizacion
{
    public class VMDivisionComercial : VistaModeloBase<Inteldev.Core.DTO.Organizacion.DivisionComercial>
    {

        public IPresentadorMinibuscaList<DivisionComercial,Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMinibuscaList<DivisionComercial,Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMinibuscaList<DivisionComercial,Empresa>), typeof(VMDivisionComercial));
        public VMDivisionComercial() : base() { }

        public VMDivisionComercial(Inteldev.Core.DTO.Organizacion.DivisionComercial dto) : base(dto)
        {
            this.SetPresentadorEspecial<Empresa>("PresentadorEmpresa",dto.Empresas);
            this.PresentadorEmpresa.PMB.cantidadNumeros = 2;
        }

        
    }
}
