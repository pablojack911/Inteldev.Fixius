using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using Inteldev.Fixius.Servicios.DTO.Stock;
using Inteldev.Fixius.Vistas;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Deposito.IngresoMercaderia
{
    public class VMIngreso : VistaModeloBase<Ingreso>
    {
        public VMIngreso()
            : base()
        {
        }
        public VMIngreso(Ingreso dto)
            : base(dto)
        {
            this.SetPresentador<DocumentoCompra>("PresentadorFacturas", dto.Facturas);
        }

        # region Facturas
        public IPresentadorGrilla<Ingreso,DocumentoCompra,VistaFacturaDeCompra> PresentadorFacturas
        {
            get { return (IPresentadorGrilla<Ingreso,DocumentoCompra,VistaFacturaDeCompra>)GetValue(PresentadorFacturasProperty); }
            set { SetValue(PresentadorFacturasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorFacturas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorFacturasProperty = RegistrarDp<VMIngreso>(p => p.PresentadorFacturas);
        # endregion

        # region Ingreso

        public bool PuedeIngresar
        {
            get { return (bool)GetValue(PuedeIngresarProperty); }
            set { SetValue(PuedeIngresarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PuedeIngresar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PuedeIngresarProperty =
            DependencyProperty.Register("PuedeIngresar", typeof(bool), typeof(VMIngreso));
            
        #endregion

        #region No Ingreso

        

        public IPresentadorGrilla<Ingreso,ItemNoIngresado,VistaItemNoIngresado> PresentadorNoIngresado
        {
            get { return (IPresentadorGrilla<Ingreso,ItemNoIngresado,VistaItemNoIngresado>)GetValue(PresentadorNoIngresadoProperty); }
            set { SetValue(PresentadorNoIngresadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorNoIngresado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorNoIngresadoProperty =
            DependencyProperty.Register("PresentadorNoIngresado", typeof(IPresentadorGrilla<Ingreso,ItemNoIngresado,VistaItemNoIngresado>), typeof(VMIngreso));

        
        #endregion

        #region Cierre


        public int TotalFacturas
        {
            get { return (int)GetValue(TotalFacturasProperty); }
            set { SetValue(TotalFacturasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalFacturas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalFacturasProperty =
            DependencyProperty.Register("TotalFacturas", typeof(int), typeof(VMIngreso));
        
        public decimal TotalNetoFacturas
        {
            get { return (decimal)GetValue(TotalNetoFacturasProperty); }
            set { SetValue(TotalNetoFacturasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalNetoFacturas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalNetoFacturasProperty =
            DependencyProperty.Register("TotalNetoFacturas", typeof(decimal), typeof(VMIngreso));
        
        public decimal TotalBultosFacturas
        {
            get { return ( decimal)GetValue(TotalBultosFacturasProperty); }
            set { SetValue(TotalBultosFacturasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalBultosFacturas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalBultosFacturasProperty =
            DependencyProperty.Register("TotalBultosFacturas", typeof( decimal), typeof(VMIngreso));
        
        public decimal TotalBultosIngresados
        {
            get { return (decimal)GetValue(TotalBultosIngresadosProperty); }
            set { SetValue(TotalBultosIngresadosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalBultosIngresados.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalBultosIngresadosProperty =
            DependencyProperty.Register("TotalBultosIngresados", typeof(decimal), typeof(VMIngreso));
        

        public decimal TotalBultosNoIngresados
        {
            get { return (decimal)GetValue(TotalBultosNoIngresadosProperty); }
            set { SetValue(TotalBultosNoIngresadosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalBultosNoIngresados.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalBultosNoIngresadosProperty =
            DependencyProperty.Register("TotalBultosNoIngresados", typeof(decimal), typeof(VMIngreso));



        public decimal Diferencias
        {
            get { return (decimal)GetValue(DiferenciasProperty); }
            set { SetValue(DiferenciasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diferencias.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiferenciasProperty =
            DependencyProperty.Register("Diferencias", typeof(decimal), typeof(VMIngreso));
        
        #endregion

    }
}
