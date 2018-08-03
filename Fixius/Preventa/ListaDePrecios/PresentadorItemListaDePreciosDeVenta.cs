using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Fixius.Articulos;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Precios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.ListaDePrecios
{
    public class PresentadorItemListaDePreciosDeVenta : PresentadorGrilla<ListaDePreciosDeVenta, ItemListaDePreciosDeVenta, ItemDetalleListaDePrecioDeVenta>
    {
        public PresentadorItemListaDePreciosDeVenta(ItemListaDePreciosDeVenta objeto)
            : base(objeto)
        {
            var servicioMarca = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Marca>>();
            this.Marcas = new ObservableCollection<Marca>(servicioMarca.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (objeto.Marca != null)
                objeto.Marca = this.Marcas.FirstOrDefault(m => m.Codigo == objeto.Marca.Codigo);
            this.Articulos = new List<Articulo>();
            this.PresentadorBusquedaArticulo = FabricaPresentadores._Resolver<IPresentadorBusquedaArticulo>();
            this.PresentadorMiniBuscaArticulo = FabricaPresentadores._Resolver<IPresentadorMinibuscaList<PresentadorItemListaDePreciosDeVenta, Articulo>>();
            this.PresentadorMiniBuscaArticulo.PMD.DTO = this.Articulos;
            this.PresentadorMiniBuscaArticulo.PMB.cantidadNumeros = 13;
        }

        public PresentadorItemListaDePreciosDeVenta()
            : base(new ItemListaDePreciosDeVenta())
        {
        }

        public ObservableCollection<Marca> Marcas
        {
            get { return (ObservableCollection<Marca>)GetValue(MarcasProperty); }
            set { SetValue(MarcasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Marcas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarcasProperty =
            DependencyProperty.Register("Marcas", typeof(ObservableCollection<Marca>), typeof(PresentadorItemListaDePreciosDeVenta));


        public IPresentadorBusquedaArticulo PresentadorBusquedaArticulo
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorBusquedaArticuloProperty); }
            set { SetValue(PresentadorBusquedaArticuloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorBusquedaArticulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorBusquedaArticuloProperty =
            DependencyProperty.Register("PresentadorBusquedaArticulo", typeof(IPresentadorBusquedaArticulo), typeof(PresentadorItemListaDePreciosDeVenta));




        public List<Articulo> Articulos
        {
            get { return (List<Articulo>)GetValue(ArticulosProperty); }
            set { SetValue(ArticulosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Articulos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArticulosProperty =
            DependencyProperty.Register("Articulos", typeof(List<Articulo>), typeof(PresentadorItemListaDePreciosDeVenta));


        public IPresentadorMinibuscaList<PresentadorItemListaDePreciosDeVenta, Articulo> PresentadorMiniBuscaArticulo
        {
            get { return (IPresentadorMinibuscaList<PresentadorItemListaDePreciosDeVenta, Articulo>)GetValue(PresentadorMiniBuscaArticuloProperty); }
            set { SetValue(PresentadorMiniBuscaArticuloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMiniBuscaArticulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaArticuloProperty =
            DependencyProperty.Register("PresentadorMiniBuscaArticulo", typeof(IPresentadorMinibuscaList<PresentadorItemListaDePreciosDeVenta, Articulo>), typeof(PresentadorItemListaDePreciosDeVenta));


        public override void Inicializar()
        {
            base.Inicializar();

        }

        public override bool Aceptar()
        {
            bool ok;
            if (Articulos.Count == 0)
                ok = base.Aceptar();
            else
            {
                this.Cancelar();
                this.Articulos.ForEach(a => Detalle.Add(new ItemListaDePreciosDeVenta() { Articulo = a }));
                this.Articulos.Clear();
                this.PresentadorMiniBuscaArticulo.PMD.Detalle.Clear();
                ok = true;
            }
            return ok;
        }


    }
}
