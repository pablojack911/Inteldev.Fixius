using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Articulos;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Precios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Inteldev.Fixius.Preventa.DescuentosPorLista
{
    public class VMItemDescuentoPorLista : VistaModeloBase<ItemDescuentoPorLista>
    {
        public List<Articulo> Articulos { get; set; }

        public IPresentadorBusquedaArticulo PresentadorArea
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorAreaProperty); }
            set { SetValue(PresentadorAreaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorArea.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorAreaProperty =
            DependencyProperty.Register("PresentadorArea", typeof(IPresentadorBusquedaArticulo), typeof(VMItemDescuentoPorLista));


        public IPresentadorMinibuscaList<PresentadorItemDescuentoPorLista, Articulo> PresentadorMiniBuscaArticulo
        {
            get { return (IPresentadorMinibuscaList<PresentadorItemDescuentoPorLista, Articulo>)GetValue(PresentadorMiniBuscaArticuloProperty); }
            set { SetValue(PresentadorMiniBuscaArticuloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMiniBuscaArticulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMiniBuscaArticuloProperty =
            DependencyProperty.Register("PresentadorMiniBuscaArticulo", typeof(IPresentadorMinibuscaList<PresentadorItemDescuentoPorLista, Articulo>), typeof(VMItemDescuentoPorLista));


        public ObservableCollection<Marca> Marcas
        {
            get { return (ObservableCollection<Marca>)GetValue(MarcasProperty); }
            set { SetValue(MarcasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Marcas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarcasProperty =
            DependencyProperty.Register("Marcas", typeof(ObservableCollection<Marca>), typeof(VMItemDescuentoPorLista));


        public IPresentadorListaValores<ItemDescuentoPorLista, Descuento, decimal> PresentadorDescuentos
        {
            get { return (IPresentadorListaValores<ItemDescuentoPorLista, Descuento, decimal>)GetValue(PresentadorDescuentosProperty); }
            set { SetValue(PresentadorDescuentosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDescuentos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDescuentosProperty =
            DependencyProperty.Register("PresentadorDescuentos", typeof(IPresentadorListaValores<ItemDescuentoPorLista, Descuento, decimal>), typeof(VMItemDescuentoPorLista));

        public VMItemDescuentoPorLista(ItemDescuentoPorLista dto)
            : base(dto)
        {
            this.PresentadorArea = FabricaPresentadores._Resolver<IPresentadorBusquedaArticulo>();
            var servicioMarca = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Marca>>();
            this.Marcas = new ObservableCollection<Marca>(servicioMarca.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Modelo.Marca != null)
            {
                this.Modelo.Marca = this.Marcas.FirstOrDefault(m => m.Codigo == this.Modelo.Marca.Codigo);
            }
            this.Articulos = new List<Articulo>();
            this.PresentadorArea = FabricaPresentadores._Resolver<IPresentadorBusquedaArticulo>();
            this.PresentadorMiniBuscaArticulo = FabricaPresentadores._Resolver<IPresentadorMinibuscaList<PresentadorItemDescuentoPorLista, Articulo>>();
            this.PresentadorMiniBuscaArticulo.PMD.DTO = this.Articulos;
            this.PresentadorMiniBuscaArticulo.PMB.cantidadNumeros = 13;
            dynamic p = FabricaPresentadores._Resolver<IPresentadorListaValores<ItemDescuentoPorLista, Descuento, decimal>>();
            this.PresentadorDescuentos = p;
            p.DTO = Modelo.Descuentos;
        }
        public VMItemDescuentoPorLista()
        {
        }
    }
}
