using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Preventa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Clientes.Maestro
{
    public class VMConfiguraCredito:VistaModeloBase<Inteldev.Fixius.Servicios.DTO.Clientes.ConfiguraCredito>
    {
        public VMConfiguraCredito()
        { }
            
        public VMConfiguraCredito(Inteldev.Fixius.Servicios.DTO.Clientes.ConfiguraCredito dto)
            : base(dto)
        {
            if (dto.Vendedor!=null)
                Debug.WriteLine("Vendedor hash:"+dto.Vendedor.GetHashCode().ToString());
            if (dto.Cobrador!= null)
                Debug.WriteLine("Cobrador hash:" + dto.Cobrador.GetHashCode().ToString());

            SetPresentador<Vendedor>("PresentadorVendedor", p => dto.Vendedor = p, dto.Vendedor);
            this.PresentadorVendedor.cantidadNumeros = 2;
            SetPresentador<Cobrador>("PresentadorCobrador", p => dto.Cobrador = p, dto.Cobrador);
            this.PresentadorCobrador.cantidadNumeros = 2;
            SetPresentador<Vendedor>("PresentadorVendedorEspecial", p => dto.VendedorEspecial = p, dto.VendedorEspecial);
            this.PresentadorVendedorEspecial.cantidadNumeros = 2;
            SetPresentador<CondicionDePagoCliente>("PresentadorCondicionDePago", p => dto.CondicionDePago=p, dto.CondicionDePago);
            this.PresentadorCondicionDePago.cantidadNumeros = 2;
            SetPresentador<CondicionDePagoCliente>("PresentadorCondicionDePago2", p => dto.CondicionDePago2 = p, dto.CondicionDePago2);
            this.PresentadorCondicionDePago2.cantidadNumeros = 2;
            
        }
        
        public IPresentadorMiniBusca<Vendedor> PresentadorVendedor
        {
            get { return (IPresentadorMiniBusca<Vendedor>)GetValue(PresentadorVendedorProperty); }
            set { SetValue(PresentadorVendedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorVendedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorVendedorProperty =
            DependencyProperty.Register("PresentadorVendedor", typeof(IPresentadorMiniBusca<Vendedor>), typeof(VMConfiguraCredito));
        
        public IPresentadorMiniBusca<Vendedor> PresentadorVendedorEspecial
        {
            get { return (IPresentadorMiniBusca<Vendedor>)GetValue(PresentadorVendedorEspecialProperty); }
            set { SetValue(PresentadorVendedorEspecialProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorVendedorEspecial.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorVendedorEspecialProperty =
           DependencyProperty.Register("PresentadorVendedorEspecial", typeof(IPresentadorMiniBusca<Vendedor>), typeof(VMConfiguraCredito));
        
        public IPresentadorMiniBusca<Cobrador> PresentadorCobrador
        {
            get { return (IPresentadorMiniBusca<Cobrador>)GetValue(PresentadorCobradorProperty); }
            set { SetValue(PresentadorCobradorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCobrador.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCobradorProperty =
            DependencyProperty.Register("PresentadorCobrador", typeof(IPresentadorMiniBusca<Cobrador>), typeof(VMConfiguraCredito));

        public IPresentadorMiniBusca<CondicionDePagoCliente> PresentadorCondicionDePago
        {
            get { return (IPresentadorMiniBusca<CondicionDePagoCliente>)GetValue(PresentadorCondicionDePagoProperty); }
            set { SetValue(PresentadorCondicionDePagoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCondicionDePago.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCondicionDePagoProperty =
            DependencyProperty.Register("PresentadorCondicionDePago", typeof(IPresentadorMiniBusca<CondicionDePagoCliente>), typeof(VMConfiguraCredito));

        public IPresentadorMiniBusca<CondicionDePagoCliente> PresentadorCondicionDePago2
        {
            get { return (IPresentadorMiniBusca<CondicionDePagoCliente>)GetValue(PresentadorCondicionDePago2Property); }
            set { SetValue(PresentadorCondicionDePago2Property, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCondicionDePago.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCondicionDePago2Property =
            DependencyProperty.Register("PresentadorCondicionDePago2", typeof(IPresentadorMiniBusca<CondicionDePagoCliente>), typeof(VMConfiguraCredito));

    }
}
