using Inteldev.Core.Contratos;
using Inteldev.Core.Presentacion.ClienteServicios;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Controladores;
using Inteldev.Core.Presentacion.VistasModelos;
using Inteldev.Fixius.Servicios.DTO.Clientes;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace Inteldev.Fixius.Tablas.Rubro
{
    public class VMRubro : VistaModeloBase<Servicios.DTO.Articulos.Rubro>
    {




        //public IPresentadorMiniBusca<CondicionDePagoCliente> PresentadorCondicionDePago
        //{
        //    get { return (IPresentadorMiniBusca<CondicionDePagoCliente>)GetValue(PresentadorCondicionDePagoProperty); }
        //    set { SetValue(PresentadorCondicionDePagoProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PresentadorCondicionDePago.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PresentadorCondicionDePagoProperty =
        //    DependencyProperty.Register("PresentadorCondicionDePago", typeof(IPresentadorMiniBusca<CondicionDePagoCliente>), typeof(VMRubro));






        public ObservableCollection<CondicionDePagoCliente> CondicionesDePago
        {
            get { return (ObservableCollection<CondicionDePagoCliente>)GetValue(CondicionesDePagoProperty); }
            set { SetValue(CondicionesDePagoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CondicionesDePago.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CondicionesDePagoProperty =
            DependencyProperty.Register("CondicionesDePago", typeof(ObservableCollection<CondicionDePagoCliente>), typeof(VMRubro));



        public string DesdeCodigo
        {
            get { return (string)GetValue(DesdeCodigoProperty); }
            set { SetValue(DesdeCodigoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DesdeCodigo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesdeCodigoProperty =
            DependencyProperty.Register("DesdeCodigo", typeof(string), typeof(VMRubro));



        public Visibility BotonCodigoVisible
        {
            get { return (Visibility)GetValue(BotonCodigoVisibleProperty); }
            set { SetValue(BotonCodigoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BotonCodigoVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BotonCodigoVisibleProperty =
            DependencyProperty.Register("BotonCodigoVisible", typeof(Visibility), typeof(VMRubro), new PropertyMetadata(Visibility.Collapsed));

        public ICommand CmdBuscar { get; set; }


        public VMRubro()
            : base()
        {

        }

        public VMRubro(Servicios.DTO.Articulos.Rubro DTO)
            : base(DTO)
        {
            //this.SetPresentador<CondicionDePagoCliente>("PresentadorCondicionDePago", (p => DTO.CondicionDePago = p), DTO.CondicionDePago);
            //this.PresentadorCondicionDePago.cantidadNumeros = 2;
            this.DesdeCodigo = DTO.Codigo;
            this.CmdBuscar = new RelayCommand(p => this.ObtenerCodigoDisponible(p.ToString()), p => this.PuedeBuscarCodigoDisponible(p));
            if (DTO.Id == 0)
                this.BotonCodigoVisible = Visibility.Visible;
            var servicioCondicionesDePago = FabricaClienteServicio.Instancia.CrearCliente<IServicioABM<CondicionDePagoCliente>>();
            this.CondicionesDePago = new ObservableCollection<CondicionDePagoCliente>(servicioCondicionesDePago.ObtenerLista(1, Core.CargarRelaciones.NoCargarNada, Sistema.Instancia.EmpresaActual.Codigo));
            if (this.Modelo.CondicionDePago != null)
                this.Modelo.CondicionDePago = this.CondicionesDePago.FirstOrDefault(c => c.Codigo == this.Modelo.CondicionDePago.Codigo);
            this.Modelo.PropertyChanged += Modelo_PropertyChanged;
        }

        void Modelo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Codigo")
                this.DesdeCodigo = this.Modelo.Codigo;
        }

        private object ObtenerCodigoDisponible(string desde)
        {
            var servicio = FabricaClienteServicio.Instancia.CrearCliente<IServicioObtenerCodigoDisponible>("ServicioObtenerCodigoDisponibleRubro");
            if (servicio != null)
                try
                {
                    this.Modelo.Codigo = servicio.CodigoDisponible(desde);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            return true;
        }
    }
}
