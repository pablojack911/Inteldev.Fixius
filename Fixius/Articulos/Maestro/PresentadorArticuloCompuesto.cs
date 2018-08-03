using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Fixius.Servicios.DTO;
using System.Windows;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Servicios.DTO.Articulos;

namespace Inteldev.Fixius.Articulos.Maestro
{
    public class PresentadorArticuloCompuesto :
        PresentadorGrilla<Inteldev.Fixius.Servicios.DTO.Articulos.Articulo,
                          Inteldev.Fixius.Servicios.DTO.Articulos.ArticuloCompuesto,
                          ItemArticuloCompuesto>, IPresentadorArticulosCompuestos
    {


        public IPresentadorMiniBusca<Articulo> presentadorMiniBusca
        {
            get { return (IPresentadorMiniBusca<Articulo>)GetValue(presentadorMiniBuscaProperty); }
            set { SetValue(presentadorMiniBuscaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for presentadorMiniBusca.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty presentadorMiniBuscaProperty =
            DependencyProperty.Register("presentadorMiniBusca", typeof(IPresentadorMiniBusca<Articulo>), typeof(PresentadorArticuloCompuesto));

        public PresentadorArticuloCompuesto(IPresentadorMiniBusca<Articulo> presentador, ArticuloCompuesto objeto)
            : base(objeto)
        {
            this.presentadorMiniBusca = presentador;
            this.presentadorMiniBusca.ActualizarDto = p => this.Objeto.ArticuloComponente = p;
            this.presentadorMiniBusca.cantidadNumeros = 13;
            this.presentadorMiniBusca.Entidad = objeto.ArticuloComponente;
        }

        public override bool PuedeAgregar()
        {
            return this.Maestro.ArticulosCompuestos.Count < 2;
        }
    }
}
