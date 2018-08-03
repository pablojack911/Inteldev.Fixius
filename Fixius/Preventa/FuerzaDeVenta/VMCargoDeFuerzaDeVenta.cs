using Inteldev.Core.Presentacion.VistasModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System.Windows;
using Inteldev.Fixius.Servicios.DTO.Clientes;

namespace Inteldev.Fixius.Preventa.FuerzaDeVenta
{
    public class VMCargoDeFuerzaDeVenta : VistaModeloBase<Servicios.DTO.Clientes.CargosDeFuerzaDeVenta>
    {


        public IPresentadorMinibuscaList<CargosDeFuerzaDeVenta,CargosDeFuerzaDeVenta> PresentadorCargos
        {
            get { return (IPresentadorMinibuscaList<CargosDeFuerzaDeVenta,CargosDeFuerzaDeVenta>)GetValue(PresentadorCargosProperty); }
            set { SetValue(PresentadorCargosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCargos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCargosProperty =
            DependencyProperty.Register("PresentadorCargos", typeof(IPresentadorMinibuscaList<CargosDeFuerzaDeVenta,CargosDeFuerzaDeVenta>), typeof(VMCargoDeFuerzaDeVenta));

        public VMCargoDeFuerzaDeVenta() : base() { }

        public VMCargoDeFuerzaDeVenta(CargosDeFuerzaDeVenta DTO)
            : base(DTO)
        {
            this.SetPresentadorEspecial<CargosDeFuerzaDeVenta>("PresentadorCargos",DTO.CargosHijos);
        }

        

        
    }
}
