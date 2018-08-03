using Inteldev.Core.DTO;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Articulos.Tablas.Envase
{
    public class PresentadorArticuloEnvase : PresentadorGrilla<Servicios.DTO.Articulos.Envase, Servicios.DTO.Articulos.ArticuloEnvase, ItemArticuloEnvase>
    {
        public IPresentadorMiniBusca<Articulo> PresentadorArticulo
        {
            get { return (IPresentadorMiniBusca<Articulo>)GetValue(PresentadorArticuloProperty); }
            set { SetValue(PresentadorArticuloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Articulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorArticuloProperty =
            DependencyProperty.Register("PresentadorArticulo", typeof(IPresentadorMiniBusca<Articulo>), typeof(PresentadorArticuloEnvase));



        public PresentadorArticuloEnvase(ArticuloEnvase DTO)
            : base(DTO)
        {
            this.SetearPresentadores();
        }

        private void SetearPresentadores()
        {
            this.PresentadorArticulo = FabricaPresentadores.Instancia.Resolver<PresentadorMiniBusca<Articulo>>();
            this.PresentadorArticulo.cantidadNumeros = 13;
            this.PresentadorArticulo.ActualizarDto = p => this.Objeto.Articulo = p;
            this.PresentadorArticulo.Entidad = this.Objeto.Articulo;
            //this.PresentadorArticulo.ObtenerParametros = () =>
            //    {
            //        var lista = new List<ParametrosMiniBusca>();
            //        lista.Add(new ParametrosMiniBusca() { Nombre = "EsEnvase", TipoObjeto = typeof(bool).ToString(), Valor = true });
            //        return lista;
            //    };
        }

        public override void CrearVentana()
        {
            this.SetearPresentadores();
            base.CrearVentana();
        }

        public override bool PuedeAceptar()
        {
            return PresentadorArticulo.Entidad != null;
        }
    }
}
