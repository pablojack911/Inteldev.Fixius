using Inteldev.Core.Contratos;
using Inteldev.Core.DTO.Organizacion;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using Inteldev.Fixius.Servicios.DTO.Financiero;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace Inteldev.Fixius.Articulos.Tablas.Linea
{
    public class VMLinea : VistaModeloBase<Servicios.DTO.Articulos.Linea>
    {
        public ICommand CmdBuscar { get; set; }

        public IPresentadorMiniBusca<ConceptoDeMovimiento> PresentadorConceptoMovimiento
        {
            get { return (IPresentadorMiniBusca<ConceptoDeMovimiento>)GetValue(PresentadorConceptoMovimientoProperty); }
            set { SetValue(PresentadorConceptoMovimientoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentadorConceptoMovimiento.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentadorConceptoMovimientoProperty =
            DependencyProperty.Register("PresentadorConceptoMovimiento", typeof(IPresentadorMiniBusca<ConceptoDeMovimiento>), typeof(VMLinea));



        //public IPresentadorMiniBusca<CondicionDePagoCliente> PresentadorCondicionDePago
        //{
        //    get { return (IPresentadorMiniBusca<CondicionDePagoCliente>)GetValue(PresentadorCondicionDePagoProperty); }
        //    set { SetValue(PresentadorCondicionDePagoProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorCondicionDePago.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorCondicionDePagoProperty =
        //    DependencyProperty.Register("PresentadorCondicionDePago", typeof(IPresentadorMiniBusca<CondicionDePagoCliente>), typeof(VMLinea));


        public ObservableCollection<CondicionDePagoCliente> CondicionesDePago
        {
            get { return (ObservableCollection<CondicionDePagoCliente>)GetValue(CondicionesDePagoProperty); }
            set { SetValue(CondicionesDePagoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CondicionesDePago.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CondicionesDePagoProperty =
            DependencyProperty.Register("CondicionesDePago", typeof(ObservableCollection<CondicionDePagoCliente>), typeof(VMLinea));




        //public IPresentadorMiniBusca<Empresa> PresentadorEmpresa
        //{
        //    get { return (IPresentadorMiniBusca<Empresa>)GetValue(PresentadorEmpresaProperty); }
        //    set { SetValue(PresentadorEmpresaProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorEmpresa.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorEmpresaProperty =
        //    DependencyProperty.Register("PresentadorEmpresa", typeof(IPresentadorMiniBusca<Empresa>), typeof(VMLinea));


        public ObservableCollection<Empresa> Empresas
        {
            get { return (ObservableCollection<Empresa>)GetValue(EmpresasProperty); }
            set { SetValue(EmpresasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Empresas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmpresasProperty =
            DependencyProperty.Register("Empresas", typeof(ObservableCollection<Empresa>), typeof(VMLinea));

        public string DesdeCodigo
        {
            get { return (string)GetValue(DesdeCodigoProperty); }
            set { SetValue(DesdeCodigoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DesdeCodigo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesdeCodigoProperty =
            DependencyProperty.Register("DesdeCodigo", typeof(string), typeof(VMLinea));




        public Visibility BotonCodigoVisible
        {
            get { return (Visibility)GetValue(BotonCodigoVisibleProperty); }
            set { SetValue(BotonCodigoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BotonCodigoVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BotonCodigoVisibleProperty =
            DependencyProperty.Register("BotonCodigoVisible", typeof(Visibility), typeof(VMLinea), new PropertyMetadata(Visibility.Collapsed));



        public VMLinea() : base() { }

        public VMLinea(Servicios.DTO.Articulos.Linea DTO)
            : base(DTO)
        {
            this.DesdeCodigo = DTO.Codigo;
            this.CmdBuscar = new RelayCommand(p => this.ObtenerCodigoDisponible(p.ToString()), p => this.PuedeBuscarCodigoDisponible(p));
            if (DTO.Id == 0)
                this.BotonCodigoVisible = Visibility.Visible;
            //this.SetPresentador<Empresa>("PresentadorEmpresa", (p => DTO.Empresa = p), DTO.Empresa);
            //this.PresentadorEmpresa.cantidadNumeros = 2;
            var servicioEmpresas = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<Empresa>>();
            this.Empresas = new ObservableCollection<Empresa>(servicioEmpresas.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Modelo.Empresa != null)
                this.Modelo.Empresa = this.Empresas.FirstOrDefault(e => e.Codigo == this.Modelo.Empresa.Codigo);
            //this.SetPresentador<CondicionDePagoCliente>("PresentadorCondicionDePago", (p => DTO.CondicionDePago = p), DTO.CondicionDePago);
            //this.PresentadorCondicionDePago.cantidadNumeros = 2;
            var servicioCondicionesDePago = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<CondicionDePagoCliente>>();
            this.CondicionesDePago = new ObservableCollection<CondicionDePagoCliente>(servicioCondicionesDePago.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Modelo.CondicionDePago != null)
                this.Modelo.CondicionDePago = this.CondicionesDePago.FirstOrDefault(c => c.Codigo == this.Modelo.CondicionDePago.Codigo);
            this.SetPresentador<ConceptoDeMovimiento>("PresentadorConceptoMovimiento", (p => DTO.ConceptoDeMovimiento = p), DTO.ConceptoDeMovimiento);
            this.PresentadorConceptoMovimiento.cantidadNumeros = 5;
            this.Modelo.PropertyChanged += Modelo_PropertyChanged;
        }

        void Modelo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Codigo")
                this.DesdeCodigo = this.Modelo.Codigo;
        }



        private object ObtenerCodigoDisponible(string desde)
        {
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioObtenerCodigoDisponible>("ServicioObtenerCodigoDisponibleLinea");
            if (servicio != null)
                this.Modelo.Codigo = servicio.CodigoDisponible(desde);
            return true;
        }

    }
}
