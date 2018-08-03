using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Vistas.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.VistasModelo
{
    public class VMOperarioDeVenta : VistaModeloBase<Servicios.DTO.Clientes.OperariosDePreventa>
    {


        public IPresentadorGrilla<OperariosDePreventa, CargosDeFuerzaDeVenta, VistaCargos> PresentadorCargos
        {
            get { return (IPresentadorGrilla<OperariosDePreventa, CargosDeFuerzaDeVenta, VistaCargos>)GetValue(PresentadorCargosProperty); }
            set { SetValue(PresentadorCargosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCargos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCargosProperty =
            DependencyProperty.Register("PresentadorCargos", typeof(IPresentadorGrilla<OperariosDePreventa, CargosDeFuerzaDeVenta, VistaCargos>), typeof(VMOperarioDeVenta));

        public VMOperarioDeVenta() : base() { }

        public VMOperarioDeVenta(OperariosDePreventa DTO)
            : base(DTO)
        {
            this.SetPresentador<CargosDeFuerzaDeVenta>("PresentadorCargos",DTO.Cargos);
        }
        
    }
}
