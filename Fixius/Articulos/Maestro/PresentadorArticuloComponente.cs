using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Maestro
{
    public class PresentadorArticuloComponente : PresentadorGrilla<Articulo, ArticuloCompuesto, ItemArticuloComponente>
    {

        public IPresentadorMiniBusca<Articulo> PresentadorArticulo
        {
            get { return (IPresentadorMiniBusca<Articulo>)GetValue(PresentadorArticuloProperty); }
            set { SetValue(PresentadorArticuloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorArticulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorArticuloProperty =
            DependencyProperty.Register("PresentadorArticulo", typeof(IPresentadorMiniBusca<Articulo>), typeof(PresentadorArticuloComponente));



        public PresentadorArticuloComponente(ArticuloCompuesto DTO)
            : base(DTO)
        {
            this.SetPresentadores();
        }

        private void SetPresentadores()
        {
            this.PresentadorArticulo = FabricaPresentadores.Instancia.Resolver<IPresentadorMiniBusca<Articulo>>();
            this.PresentadorArticulo.cantidadNumeros = 13;
            this.PresentadorArticulo.ActualizarDto = p => this.Objeto.ArticuloComponente = p;
            this.PresentadorArticulo.Entidad = this.Objeto.ArticuloComponente;
        }

        public override void CrearVentana()
        {
            this.SetPresentadores();
            base.CrearVentana();
        }

        public override bool PuedeAgregar()
        {
            return this.Maestro.ArticulosCompuestos.Count < 2;
        }
    }
}
