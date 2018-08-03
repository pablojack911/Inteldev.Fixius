using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;

using Inteldev.Fixius.Servicios.DTO.Tesoreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Tesoreria.MovimientoBancario
{
    public class PresentadorItemMovimientoBancario : PresentadorGrilla<Fixius.Servicios.DTO.Tesoreria.CuentaBancaria, Fixius.Servicios.DTO.Tesoreria.MovimientoBancario, ItemMovimientoBancario>
    {


        public IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Tesoreria.ConceptoDeMovimientoBancario> PresentadorMovimientoBancario
        {
            get { return (IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Tesoreria.ConceptoDeMovimientoBancario>)GetValue(PresentadorMovimientoBancarioProperty); }
            set { SetValue(PresentadorMovimientoBancarioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMovimientoBancario.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMovimientoBancarioProperty =
            DependencyProperty.Register("PresentadorMovimientoBancario", typeof(IPresentadorMiniBusca<Inteldev.Fixius.Servicios.DTO.Tesoreria.ConceptoDeMovimientoBancario>), typeof(PresentadorItemMovimientoBancario));



        public PresentadorItemMovimientoBancario(Servicios.DTO.Tesoreria.MovimientoBancario objeto)
            : base(objeto) 
        {
            this.PresentadorMovimientoBancario = FabricaPresentadores._Resolver<IPresentadorMiniBusca<ConceptoDeMovimientoBancario>>();
            this.PresentadorMovimientoBancario.ActualizarDto = (p => this.Objeto.ConceptoMovimientoBancario = p);
            this.PresentadorMovimientoBancario.Entidad = this.Objeto.ConceptoMovimientoBancario;
            this.PresentadorMovimientoBancario.cantidadNumeros = 3;
        }

    }
}
