using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Tesoreria;
using Inteldev.Fixius.Tesoreria.MovimientoBancario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Tesoreria.CuentaBancaria
{
    public class VMCuentaBancaria : VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Tesoreria.CuentaBancaria>
    {


        public IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Tesoreria.Banco> PresentadorBanco
        {
            get { return (IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Tesoreria.Banco>)GetValue(PresentadorBancoProperty); }
            set { SetValue(PresentadorBancoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorBanco.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorBancoProperty =
            DependencyProperty.Register("PresentadorBanco", typeof(IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Tesoreria.Banco>), typeof(VMCuentaBancaria));



        public IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Empresa> PresentadorEmpresa
        {
            get { return (IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Empresa>)GetValue(PresentadorEmpresaProperty); }
            set { SetValue(PresentadorEmpresaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpresaProperty =
            DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Inteldev.Core.DTO.Organizacion.Empresa>), typeof(VMCuentaBancaria));



        public IPresentadorGrilla<Servicios.DTO.Tesoreria.CuentaBancaria, Servicios.DTO.Tesoreria.MovimientoBancario, ItemMovimientoBancario> PresentadorGrilla
        {
            get { return (IPresentadorGrilla<Servicios.DTO.Tesoreria.CuentaBancaria, Servicios.DTO.Tesoreria.MovimientoBancario, ItemMovimientoBancario>)GetValue(PresentadorGrillaProperty); }
            set { SetValue(PresentadorGrillaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorGrilla.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorGrillaProperty =
            DependencyProperty.Register("PresentadorGrilla", typeof(IPresentadorGrilla<Servicios.DTO.Tesoreria.CuentaBancaria, Servicios.DTO.Tesoreria.MovimientoBancario, ItemMovimientoBancario>), typeof(VMCuentaBancaria));


        public VMCuentaBancaria() : base() { }

        public VMCuentaBancaria(Servicios.DTO.Tesoreria.CuentaBancaria DTO)
            : base(DTO)
        {
            this.SetPresentador<Banco>("PresentadorBanco",p=>DTO.Banco=p,DTO.Banco);
            this.SetPresentador<Empresa>("PresentadorEmpresa",p=>DTO.Empresa=p,DTO.Empresa);
            this.PresentadorEmpresa.cantidadNumeros = 2;
            this.SetPresentador<Servicios.DTO.Tesoreria.MovimientoBancario>("PresentadorGrilla", DTO.MovimientosBancarios);
        }

        

        

    }
}
