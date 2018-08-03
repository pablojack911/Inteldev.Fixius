using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Articulos;
using Inteldev.Fixius.Articulos.Maestro;
using Inteldev.Fixius.Contratos;
using Inteldev.Fixius.Presentadores;
using Inteldev.Fixius.Servicios.DTO.Articulos;
using Inteldev.Fixius.Servicios.DTO.Contabilidad;
using Inteldev.Fixius.Servicios.DTO.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Fixius.Articuos.Maestro
{
    public class VMArticulo : VistaModeloBase<Articulo>
    {
        #region DP's


        public IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor> PresentadorProveedor
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor>)GetValue(PresentadorProveedorProperty); }
            set { SetValue(PresentadorProveedorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorProveedor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorProveedorProperty =
            DependencyProperty.Register("PresentadorProveedor", typeof(IPresentadorMiniBusca<Servicios.DTO.Proveedores.Proveedor>), typeof(VMArticulo));


        public IPresentadorMiniBusca<Marca> PresentadorMarca
        {
            get { return (IPresentadorMiniBusca<Marca>)GetValue(PresentadorMarcaProperty); }
            set { SetValue(PresentadorMarcaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorMarca.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorMarcaProperty =
            DependencyProperty.Register("PresentadorMarca", typeof(IPresentadorMiniBusca<Marca>), typeof(VMArticulo));


        public IPresentadorMiniBusca<Empaque> PresentadorEmpaque
        {
            get { return (IPresentadorMiniBusca<Empaque>)GetValue(PresentadorEmpaqueProperty); }
            set { SetValue(PresentadorEmpaqueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEmpaque.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEmpaqueProperty =
            DependencyProperty.Register("PresentadorEmpaque", typeof(IPresentadorMiniBusca<Empaque>), typeof(VMArticulo));


        public IPresentadorMiniBusca<Caracteristica> PresentadorCaracteristica
        {
            get { return (IPresentadorMiniBusca<Caracteristica>)GetValue(PresentadorCaracteristicaProperty); }
            set { SetValue(PresentadorCaracteristicaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCaracteristica.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCaracteristicaProperty =
            DependencyProperty.Register("PresentadorCaracteristica", typeof(IPresentadorMiniBusca<Caracteristica>), typeof(VMArticulo));


        //public IPresentadorArticulosCompuestos PresentadorArticuloCompuesto
        //{
        //    get { return (IPresentadorArticulosCompuestos)GetValue(PresentadorArticuloCompuestoProperty); }
        //    set { SetValue(PresentadorArticuloCompuestoProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorArticuloCompuesto.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorArticuloCompuestoProperty =
        //    DependencyProperty.Register("PresentadorArticuloCompuesto", typeof(IPresentadorArticulosCompuestos), typeof(VMArticulo));



        public IPresentadorGrilla<Articulo, ArticuloCompuesto, ItemArticuloComponente> PresentadorArticulosCompuestos
        {
            get { return (IPresentadorGrilla<Articulo, ArticuloCompuesto, ItemArticuloComponente>)GetValue(PresentadorArticulosCompuestosProperty); }
            set { SetValue(PresentadorArticulosCompuestosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorArticulosCompuestos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorArticulosCompuestosProperty =
            DependencyProperty.Register("PresentadorArticulosCompuestos", typeof(IPresentadorGrilla<Articulo, ArticuloCompuesto, ItemArticuloComponente>), typeof(VMArticulo));


        public IPresentadorMinibuscaList<Articulo, GrupoArticulo> PresentadorGrupo
        {
            get { return (IPresentadorMinibuscaList<Articulo, GrupoArticulo>)GetValue(PresentadorGrupoProperty); }
            set { SetValue(PresentadorGrupoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorGrupo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorGrupoProperty =
            DependencyProperty.Register("PresentadorGrupo", typeof(IPresentadorMinibuscaList<Articulo, GrupoArticulo>), typeof(VMArticulo));


        public IPresentadorMiniBusca<Envase> PresentadorEnvase
        {
            get { return (IPresentadorMiniBusca<Envase>)GetValue(PresentadorEnvaseProperty); }
            set { SetValue(PresentadorEnvaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorEnvase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorEnvaseProperty =
            DependencyProperty.Register("PresentadorEnvase", typeof(IPresentadorMiniBusca<Envase>), typeof(VMArticulo));


        public IPresentadorGrilla<Articulo, CodigoDun, ItemDUN> PresentadorCodigoDUN
        {
            get { return (IPresentadorGrilla<Articulo, CodigoDun, ItemDUN>)GetValue(PresentadorCodigoDUNProperty); }
            set { SetValue(PresentadorCodigoDUNProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCodigoDUN.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCodigoDUNProperty =
            DependencyProperty.Register("PresentadorCodigoDUN", typeof(IPresentadorGrilla<Articulo, CodigoDun, ItemDUN>), typeof(VMArticulo));


        public IPresentadorGrilla<Articulo, CodigoEan, ItemEAN> PresentadorCodigoEAN
        {
            get { return (IPresentadorGrilla<Articulo, CodigoEan, ItemEAN>)GetValue(PresentadorCodigoEANProperty); }
            set { SetValue(PresentadorCodigoEANProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorCodigoEAN.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorCodigoEANProperty =
            DependencyProperty.Register("PresentadorCodigoEAN", typeof(IPresentadorGrilla<Articulo, CodigoEan, ItemEAN>), typeof(VMArticulo));


        public IPresentadorGrilla<Articulo, ObservacionArticulo, Inteldev.Core.Presentacion.Controles.ItemObservacion> PresentadorObservacion
        {
            get { return (IPresentadorGrilla<Articulo, ObservacionArticulo, Inteldev.Core.Presentacion.Controles.ItemObservacion>)GetValue(PresentadorObservacionProperty); }
            set { SetValue(PresentadorObservacionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorObservacion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorObservacionProperty =
            DependencyProperty.Register("PresentadorObservacion", typeof(IPresentadorGrilla<Articulo, ObservacionArticulo, Inteldev.Core.Presentacion.Controles.ItemObservacion>), typeof(VMArticulo));


        //public IPresentadorMiniBusca<TasasDeIva> PresentadorTasasIVA
        //{
        //    get { return (IPresentadorMiniBusca<TasasDeIva>)GetValue(PresentadorTasasIVAProperty); }
        //    set { SetValue(PresentadorTasasIVAProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorTasasIVA.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorTasasIVAProperty =
        //    DependencyProperty.Register("PresentadorTasasIVA", typeof(IPresentadorMiniBusca<TasasDeIva>), typeof(VMArticulo));


        public IPresentadorBusquedaArticulo PresentadorBusquedaArticulo
        {
            get { return (IPresentadorBusquedaArticulo)GetValue(PresentadorBusquedaArticuloProperty); }
            set { SetValue(PresentadorBusquedaArticuloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorBusquedaArticulo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorBusquedaArticuloProperty =
            DependencyProperty.Register("PresentadorBusquedaArticulo", typeof(IPresentadorBusquedaArticulo), typeof(VMArticulo));


        public IPresentadorMiniBusca<Servicios.DTO.Articulos.Linea> PresentadorLinea
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Articulos.Linea>)GetValue(PresentadorLineaProperty); }
            set { SetValue(PresentadorLineaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorLinea.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorLineaProperty =
            DependencyProperty.Register("PresentadorLinea", typeof(IPresentadorMiniBusca<Servicios.DTO.Articulos.Linea>), typeof(VMArticulo));


        public IPresentadorMiniBusca<Servicios.DTO.Articulos.Rubro> PresentadorRubro
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Articulos.Rubro>)GetValue(PresentadorRubroProperty); }
            set { SetValue(PresentadorRubroProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorRubro.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorRubroProperty =
            DependencyProperty.Register("PresentadorRubro", typeof(IPresentadorMiniBusca<Servicios.DTO.Articulos.Rubro>), typeof(VMArticulo));


        public IPresentadorMiniBusca<Servicios.DTO.Articulos.Clase> PresentadorClase
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Articulos.Clase>)GetValue(PresentadorClaseProperty); }
            set { SetValue(PresentadorClaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorClase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorClaseProperty =
            DependencyProperty.Register("PresentadorClase", typeof(IPresentadorMiniBusca<Servicios.DTO.Articulos.Clase>), typeof(VMArticulo));


        public IPresentadorMiniBusca<Servicios.DTO.Articulos.Division> PresentadorDivision
        {
            get { return (IPresentadorMiniBusca<Servicios.DTO.Articulos.Division>)GetValue(PresentadorDivisionProperty); }
            set { SetValue(PresentadorDivisionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorDivision.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorDivisionProperty =
            DependencyProperty.Register("PresentadorDivision", typeof(IPresentadorMiniBusca<Servicios.DTO.Articulos.Division>), typeof(VMArticulo));


        public IPresentadorMiniBusca<Articulo> PresentadorArticuloEnvase
        {
            get { return (IPresentadorMiniBusca<Articulo>)GetValue(PresentadorArticuloEnvaseProperty); }
            set { SetValue(PresentadorArticuloEnvaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorArticuloEnvase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorArticuloEnvaseProperty =
            DependencyProperty.Register("PresentadorArticuloEnvase", typeof(IPresentadorMiniBusca<Articulo>), typeof(VMArticulo));


        public IPresentadorMiniBusca<SKU> PresentadorSKU
        {
            get { return (IPresentadorMiniBusca<SKU>)GetValue(PresentadorSKUProperty); }
            set { SetValue(PresentadorSKUProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorSKU.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorSKUProperty =
            DependencyProperty.Register("PresentadorSKU", typeof(IPresentadorMiniBusca<SKU>), typeof(VMArticulo));



        public Decimal ValorTasaDeIva
        {
            get { return (Decimal)GetValue(ValorTasaDeIvaProperty); }
            set { SetValue(ValorTasaDeIvaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValorTasaDeIva.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValorTasaDeIvaProperty =
            DependencyProperty.Register("ValorTasaDeIva", typeof(Decimal), typeof(VMArticulo));



        public string DesdeCodigo
        {
            get { return (string)GetValue(DesdeCodigoProperty); }
            set { SetValue(DesdeCodigoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DesdeCodigo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesdeCodigoProperty =
            DependencyProperty.Register("DesdeCodigo", typeof(string), typeof(VMArticulo));



        public Visibility BotonCodigoVisible
        {
            get { return (Visibility)GetValue(BotonCodigoVisibleProperty); }
            set { SetValue(BotonCodigoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BotonCodigoVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BotonCodigoVisibleProperty =
            DependencyProperty.Register("BotonCodigoVisible", typeof(Visibility), typeof(VMArticulo), new PropertyMetadata(Visibility.Collapsed));



        #endregion

        public ICommand CmdBuscar { get; set; }

        public VMArticulo() : base() { }

        public VMArticulo(Articulo DTO)
            : base(DTO)
        {
            this.DesdeCodigo = DTO.Codigo;
            this.CmdBuscar = new RelayCommand(p => this.ObtenerCodigoDisponible(p.ToString()), p => this.PuedeBuscarCodigoDisponible(p));
            if (DTO.Id == 0)
                this.BotonCodigoVisible = Visibility.Visible;
            //    this.CodigoVisible = Visibility.Collapsed;
            //if (DTO.Codigo != null || DTO.Codigo != "")
            //    this.BotonCodigoVisible = Visibility.Collapsed;
            this.SetPresentador<Servicios.DTO.Proveedores.Proveedor>("PresentadorProveedor", (p => DTO.Proveedor = p), DTO.Proveedor);
            this.PresentadorProveedor.cantidadNumeros = 5;
            this.SetPresentador<Marca>("PresentadorMarca", (p => DTO.Marca = p), DTO.Marca);
            this.PresentadorMarca.cantidadNumeros = 3;
            this.SetPresentador<Empaque>("PresentadorEmpaque", (p => DTO.Empaque = p), DTO.Empaque);
            this.PresentadorEmpaque.cantidadNumeros = 3;
            this.SetPresentador<Caracteristica>("PresentadorCaracteristica", (p => DTO.Caracteristica = p), DTO.Caracteristica);
            this.PresentadorCaracteristica.cantidadNumeros = 3;
            //this.SetPresentador<ArticuloCompuesto>("PresentadorArticuloCompuesto", DTO.ArticulosCompuestos);
            this.SetPresentador<ArticuloCompuesto>("PresentadorArticulosCompuestos", DTO.ArticulosCompuestos);
            this.SetPresentadorEspecial<GrupoArticulo>("PresentadorGrupo", DTO.Grupo);
            this.SetPresentador<Envase>("PresentadorEnvase", (p => DTO.Envase = p), DTO.Envase);
            this.PresentadorEnvase.cantidadNumeros = 2;
            this.SetPresentador<CodigoDun>("PresentadorCodigoDUN", DTO.CodigoDUN);
            this.SetPresentador<CodigoEan>("PresentadorCodigoEAN", DTO.CodigoEAN);
            this.SetPresentador<ObservacionArticulo>("PresentadorObservacion", DTO.Observaciones);
            //this.SetPresentador<TasasDeIva>("PresentadorTasasIVA", (p => DTO.TasasDeIva = p), DTO.TasasDeIva);
            //this.PresentadorTasasIVA.cantidadNumeros = 3;

            if (DTO.DatosOld == null)
                DTO.DatosOld = new DatosOldArticulo();
            this.SetPresentador<SKU>("PresentadorSKU", p => DTO.DatosOld.SKU = p, DTO.DatosOld.SKU);
            this.PresentadorSKU.cantidadNumeros = 2;
            this.SetPresentador<Articulo>("PresentadorArticuloEnvase", p => DTO.DatosOld.ArticuloEnvase = p, DTO.DatosOld.ArticuloEnvase);
            this.PresentadorArticuloEnvase.cantidadNumeros = 13;
            this.SetPresentador<Division>("PresentadorDivision", (p => DTO.DatosOld.Division = p), DTO.DatosOld.Division);
            this.PresentadorDivision.cantidadNumeros = 2;
            this.SetPresentador<Clase>("PresentadorClase", (p => DTO.DatosOld.Clase = p), DTO.DatosOld.Clase);
            this.PresentadorClase.cantidadNumeros = 3;
            this.SetPresentador<Rubro>("PresentadorRubro", (p => DTO.DatosOld.Rubro = p), DTO.DatosOld.Rubro);
            this.PresentadorRubro.cantidadNumeros = 3;
            this.SetPresentador<Linea>("PresentadorLinea", (p => DTO.DatosOld.Linea = p), DTO.DatosOld.Linea);
            this.PresentadorLinea.cantidadNumeros = 3;

            this.PresentadorBusquedaArticulo = (IPresentadorBusquedaArticulo)FabricaPresentadores.Instancia.Resolver(typeof(IPresentadorBusquedaArticulo));

            this.ValorTasaDeIva = this.BuscarValorTasaDeIVA(this.Modelo.TasaDeIva);

            this.Modelo.PropertyChanged += Modelo_PropertyChanged;
        }

        private object ObtenerCodigoDisponible(string desde)
        {

            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioObtenerCodigoDisponible>("ServicioObtenerCodigoDisponibleArticulo");
            if (servicio != null)
                this.Modelo.Codigo = servicio.CodigoDisponible(desde);
            return true;
        }

        void Modelo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TasaDeIva")
                this.ValorTasaDeIva = this.BuscarValorTasaDeIVA(this.Modelo.TasaDeIva);
            if (e.PropertyName == "Codigo")
                this.DesdeCodigo = this.Modelo.Codigo;
        }

        private decimal BuscarValorTasaDeIVA(EnumTasas tasa)
        {
            decimal valor = 0;
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioValorTasasDeIva>("ServicioValorTasasDeIva");
            if (servicio != null)
            {
                valor = servicio.ObtenerValorDeTasaDeIVA(tasa);
            }
            return valor;
        }

    }
}
