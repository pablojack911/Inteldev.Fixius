using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Tesoreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Tesoreria.ChequeTerceros
{
    public class VMChequeDeTerceros : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Tesoreria.ChequeDeTercero>
    {


        public IPresentadorMiniBusca<Servicios.DTO.Tesoreria.Banco> PresentadorBanco
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Tesoreria.Banco>)GetValue(PresentadorBancoProperty); }
            set { SetValue(PresentadorBancoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorBanco.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorBancoProperty =
            DependencyProperty.Register("PresentadorBanco", typeof(IPresentadorMiniBusca<Servicios.DTO.Tesoreria.Banco>), typeof(VMChequeDeTerceros));


        public IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Empresa>), typeof(VMChequeDeTerceros));

        public IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Sucursal> PresentadorSucursal
        {
            get { return (IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Sucursal>)GetValue(PresentadorSucursalProperty); }
            set { SetValue(PresentadorSucursalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorSucursal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorSucursalProperty =
            DependencyProperty.Register("PresentadorSucursal", typeof(IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Sucursal>), typeof(VMChequeDeTerceros));



        //falta definir el presentador de movimientos

        

        public VMChequeDeTerceros() : base() { }

        public VMChequeDeTerceros(ChequeDeTercero DTO)
            : base(DTO)
        {
            this.SetPresentador<Banco>("PresentadorBanco", (p => DTO.Banco = p), DTO.Banco);
            this.SetPresentador<Empresa>("PresentadorEmpresa",(p=> DTO.Empresa = p),DTO.Empresa);
            this.PresentadorEmpresa.cantidadNumeros = 2;
            this.SetPresentador<Sucursal>("PresentadorSucursal",(p=> DTO.Sucursal = p),DTO.Sucursal);
        }
    }
}
